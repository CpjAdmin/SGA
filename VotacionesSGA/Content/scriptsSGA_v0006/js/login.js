//*** WebAPI
//Local:  http://localhost:8086
//HESTIA: http://190.241.180.140:8086

//*** Toast sweetalert2 - https://sweetalert2.github.io
const ToastGlobal = Swal.mixin({
    position: 'center',
    showConfirmButton: false
});

//*** Objeto - Datos de Usuario
var DatosU = new Object();
//*** #1 - Inicio
$(document).ready(function () {

    //*** Limpiar sessionStorage
    sessionStorage.clear();

    // 1.1 Botón Login
    $('#btnlogin').click(function (e) {
        e.preventDefault();
        //*** Validar Usuario
        validar();

    });

    // 1.2 Tecla ENTER
    $('#pin, #username').keypress(function (e) {
        var key = e.which;
        if (key === 13) {
            //*** Validar Usuario
            validar();
        }
    });
});
//*** (#1) #2 - Validar datos del usuario ingresados
function validar() {
    // 2.1 Valida campo de Usuario
    if ($('#username').val().trim() === '') {
        alertaError("LOGIN", 'Debe digitar su USUARIO.');
        return;
    }

    // 2.2 Valida campo de Pin
    if ($('#pin').val().trim() === '' || $('#pin').val().trim().length < 4) {
        alertaError("LOGIN", 'Debe digitar una CLAVE de entre 4 y 6 valores.');
        return;
    }

    // 2.3 Proceso de validación de Usuario
    validarUsuario();
}
//*** (2.3) #3 -  Validar Usuario - Exec #1
function validarUsuario() {
    // Obtener el Navegador
    var browser = get_browser_info();
    // Mostar Modal Cargando
    MostrarCargando();
    // Define el valor de los datos de usuario
    DatosU.Login = $('#username').val().trim().toUpperCase();
    DatosU.Clave = $('#pin').val().trim().toUpperCase();
    DatosU.Navegador = browser.name + " - " + browser.version;

    // 3.1 Llamada Ajax ValidarUsuario
    $.ajax({
        type: 'POST',
        url: 'http://localhost:8086' + '/api/login/Usuario/ValidarUsuario',
        data: JSON.stringify(DatosU),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("VOTACIONES SGA", mensajeError);
            CerrarCargando();
        },
        success: function (data) {

            // Condiciones del Ajax según el resultado "data"
            // console.log(data);

            // 3.1.1 - Ingresar
            if (data.respuesta > 0) {
                //*** Redireccionar
                IngresarSistema(data.ObjUsuario);
                // 3.1.2 - Usuario o Clave incorrecto
            } else if (data.respuesta === -1) {
                alertaError("VOTACIONES SGA", "El usuario o clave son incorrectos.");
                // Cerrar Modal Cargando
                CerrarCargando();
                // 3.1.3 - Usuario Inactivo
            } else if (data.respuesta === -2) {
                alertaError("VOTACIONES SGA", "El usuario está Inactivo.");
                CerrarCargando();
                // 3.1.4 - Voto ya Emitido por el usuario
            } else if (data.respuesta === -3) {
                // Toast con Mensaje de "Voto ya Emitido"
                ToastGlobal.fire({
                    toast: false,
                    type: 'info',
                    title: 'VOTACIONES SGA',
                    html: '<h3>El usuario <a class="text-danger">' + DatosU.Login + '</a> ya emitió su voto.</h3></br>',
                    allowOutsideClick: true,
                    timer: 7000
                });
                CerrarCargando();
                // 3.1.5 - Ocurrio un error no controlado
            } else {
                alertaError("VOTACIONES SGA", "Ocurrio un error no controlado." + data.respuesta);
                CerrarCargando();
            }
        }
    });
}
//*** (3.1.1 ) #4 - Ingresar al Sistema - Exec #2
function IngresarSistema(data) {

    //*** Definición de Variables de Sesión con el objeto DATA retornado por Ajax
    //console.log(data);
    sessionStorage['Id'] = data.Id_usuario;
    sessionStorage['Id_delegado'] = data.Id_delegado;
    sessionStorage['Cedula'] = data.Login;
    sessionStorage['Nombre'] = data.Nombre;
    sessionStorage['Foto'] = data.Foto;
    sessionStorage['Token'] = data.Token;
    sessionStorage['Rol'] = data.Id_rol;
    sessionStorage['F_ult_ingreso'] = data.F_ult_ingreso;
    sessionStorage['Uri'] = 'http://localhost:8086';
    sessionStorage['Pagina_ref'] = data.Pagina_ref;
    sessionStorage['Sesion_act'] = data.Sesion_act;

    // 4.1 Verificar el ROL del Usuario ( Página de Inicio )
    if (sessionStorage.getItem('Pagina_ref') === "") {
        // Toast con Mensaje de "Usuario sin ROL definido"
        ToastGlobal.fire({
            toast: false,
            type: 'info',
            title: 'VOTACIONES SGA',
            html: '<h3>El Rol ( <a class="text-danger bold">' + data.Id_rol + ' - ' + data.Nombre_rol + '</a> ) del usuario <b>' + data.Login + '</b> no tiene una página inicial definida.</h3></br>',
            allowOutsideClick: true,
            timer: 7000
        });
        CerrarCargando();
    } else {
        //*** Redireccionar
        redirect(sessionStorage.getItem('Pagina_ref'));
    }
}
//*** (0) #5 - Mostar Modal Cargando
function MostrarCargando() {
    $('body').append('<div id="loading"><div id="cargando" class="spinner"></div></div>');
    $('#loading').show();
}
//*** (0) #6 - Cerrar Modal Cargando
function CerrarCargando() {
    $('#loading').hide();
    $('#loading').remove();
}
//*** (3) #7 - Función Obtener Navegador
function get_browser_info() {
    var ua = navigator.userAgent, tem, M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return { name: 'IE ', version: tem[1] || '' };
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\bOPR\/(\d+)/);
        if (tem !== null) { return { name: 'Opera', version: tem[1] }; }
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) !== null) { M.splice(1, 1, tem[1]); }
    return {
        name: M[0],
        version: M[1]
    };
}
//*** (GLOBAL) #8 - Redireccionar desde Cualquier Explorador
function redirect(url) {

    var ua = navigator.userAgent.toLowerCase(),
        isIE = ua.indexOf('msie') !== -1,
        version = parseInt(ua.substr(4, 2), 10);

    //*** Internet Explorer 8 y versiones anteriores
    if (isIE && version < 9) {
        var link = document.createElement('a');
        link.href = url;
        document.body.appendChild(link);
        link.click();
    }
    //*** Todos los demás navegadores pueden usar el estándar window.location.href (no pierden HTTP_REFERER como Internet Explorer 8 y versiones inferiores)
    else {
        window.location.href = url;
    }
}
//*** (GLOBAL) #9 - Alerta de Error
function alertaError(titulo, detalle) {
    Swal.close();

    Swal.fire({
        type: 'error',
        title: titulo,
        text: detalle,
        timer: 15000,
        confirmButtonText: 'Aceptar'
    });
}





