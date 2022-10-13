var apiUrl = sessionStorage.getItem("Uri");
var paginaInicio = sessionStorage.getItem("Pagina_ref");
var token = sessionStorage.getItem("Token");
var login = sessionStorage.getItem("Cedula");
var nombre = sessionStorage.getItem("Nombre");

var paginaLocal = window.location.pathname.replace('/', '');

var paginaPermisos = '';

//*** Toast sweetalert2 - https://sweetalert2.github.io
const ToastGlobal = Swal.mixin({
    position: 'center',
    showConfirmButton: false
});

//*** GLOBAL - Validar Sesión Inicial de todas las páginas, menos Votaciones.aspx ( Si la página no es válida para el usuario lo redirecciona a LOGIN.ASPX )
if (paginaLocal !== 'Votaciones.aspx') {

    //*** 1.2 - Validar Token
    if (validarSesion()) {

        redirect('Login.aspx');

    } else {
        //*** 1.3 - Validar Permiso de Página
        ValidarPaginaUsuario();
    }
} else {
    //*** 1.4 - Se muestra el Body que fue oculto desde principal.css
    $('body').css('visibility', 'visible');
}

//*** #1 - Inicio
$(document).ready(function () {
    //*** 1.1 - Mostar Log de Variables Globales
    //MostarLog(); 

    //*** 1.2 - Botón Inicio
    $(document).on('click', '#btnInicio', function () {
        redirect(paginaInicio);
    });
});

//*** #2 - Validar Sesión
function validarSesion() {

    //*** Verificar si el TOKEN es valido
    if (token === null || token === undefined || token.length <= 0) {
        return true;
    } else {
        return false;
    }
}
//*** (GLOBAL) #3 - Mostrar Modal - Verificado ( No permite cerrarlo )
function mostrarModal(id, titulo) {
    //*** Titulo
    $('#modalCargando .modal-title').text(titulo);
    //*** Mostrar
    $('#' + id + '').modal({
        backdrop: 'static',
        keyboard: false
    });
}
//*** (GLOBAL) #4 - Ocultar Modal 
function ocultarModal(id) {
    $('#' + id + '').modal('hide');
}
//*** (GLOBAL) #5 - Redireccionar desde Cualquier Explorador
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
//*** (GLOBAL) #6.1 - Mensaje de Error para Ajax
function AjaxError(jqXHR, textStatus, errorThrown) {

    //*** Mensaje Toast con el error
    ToastGlobal.fire({
        toast: false,
        type: 'error',
        title: 'Estado: ' + jqXHR.status,
        html: '<h5>' + AjaxMensajeError(jqXHR, textStatus) + '</h5></br>',
        allowOutsideClick: true
    });

    //*** Redireccionar a la página de Login.aspx ( Posiblemente el TOKEN no es valido )
    setTimeout(function () {
        redirect('Login.aspx');
    }, 6000);
}
//*** (GLOBAL) #6.2 - Mensaje de Error para Ajax
function AjaxMensajeError(jqXHR, textStatus) {

    if (jqXHR.status === 0) {
        return 'No conectado.\n Por favor verifique su conexión de red.';
    } else if (jqXHR.status === 404) {
        return 'La página solicitada no se encuentra. [404]';
    } else if (jqXHR.status === 500) {
        return 'Error de Servidor Interno [500].';
    } else if (textStatus === 'parsererror') {
        return 'El análisis JSON solicitado falló.';
    } else if (textStatus === 'timeout') {
        return 'Error de tiempo de espera.';
    } else if (textStatus === 'abort') {
        return 'Solicitud de Ajax abortada.';
    } else {
        return 'Error: \n' + jqXHR.responseText;
    }
}
//*** (GLOBAL) #7 - Verifica si el caracter digitado es un número
function isNumber(evt) {
    evt = evt ? evt : window.event;
    var charCode = evt.which ? evt.which : evt.keyCode;
    if (charCode > 31 && charCode < 48 || charCode > 57) {
        return false;
    }
    return true;
}
//*** (GLOBAL) #8 - Validar si el Email es valido
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
//*** (GLOBAL) #9 - Validar Página por Usuario
function ValidarPaginaUsuario() {

    var data = new Object();

    data.Usuario = login;
    data.Pagina = paginaLocal;

    //*** 9.1 - Ajax ValidarPaginaUsuario
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Usuario/ValidarPermisosPagina',
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaErrorMin(mensajeError);
        },
        success: function (data) {

            if (data === null) {

                //*** Mensaje de Autorización Denegada
                alert("No está autorizado para ingresar a la página: " + paginaLocal);

                //*** Redirecionar a la pagina de Inicio asignada al Rol ( Se valida que no sea la misma página)
                if (paginaLocal === paginaInicio) {
                    redirect('Login.aspx');
                } else {
                    redirect(paginaInicio);
                }

            } else {

                paginaPermisos = data.Permisos.split(',');
                
                //*** 9.2 - Se muestra el Body que fue oculto desde principal.css
                $('body').css('visibility', 'visible');

                //*** 9.3 - información del usuario
                $('#nombreU').text(nombre);
                $('#iLateral').text(nombre);

                //*** 9.4 - Foto de Perfil
                if (sessionStorage.getItem('Foto') !== 'user.png') {
                    $('.Foto-usuario').attr('src', 'Images/Usuarios/' + sessionStorage['Foto']);
                }

                //*** 9.5 - Botón Salir
                $('#btnSalir').on('click', function () {
                    alertaSalir("Salir del Sistema", "¿Desea Salir del Sistema?");
                });
            }
         }
    });
}
//*** (GLOBAL) #10 - Validar Perimsos CRUD
function ValidarPermisoCRUD(CharId) {

    if (jQuery.inArray(CharId, paginaPermisos) !== -1) {

        return true;
    } else {

        return false;
    }
}

//*** (GLOBAL) #11 - Mensaje de Bloqueo Perimsos CRUD
function mensajeBloqueoToast() {
    //*** Mensaje Toast con el error
    ToastGlobal.fire({
        toast: true,
        type: 'warning',
        title: 'ACCESO NO AUTORIZADO',
        html: '<h5>LA OPCIÓN ESTÁ BLOQUEADA.</h5>',
        timer: 2000
     
    });
}
//*** (GLOBAL) - Variables Globales
function MostarLog() {

    console.log("Api             : " + apiUrl);
    console.log("Login           : " + login);
    console.log("Token           : " + token);
    console.log("Página Inicio   : " + paginaInicio);
    console.log("Página Local    : " + paginaLocal);
    console.log("Página Permisos : " + paginaPermisos);
}
