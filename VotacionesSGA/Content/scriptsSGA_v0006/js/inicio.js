//********************* Variables Globales
var Id_eleccion;
var Id_ronda;
var Id_delegado;
var Login;
var Uri;

//********************* Constantes
const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false,
    confirmButtonText: 'VOLVER'
});

const Toast = Swal.mixin({
    position: 'center',
    showConfirmButton: false
});

//*** #1 - Inicio
$(document).ready(function () {

    //*** 1.1 Mensaje solo se muestra 2 veces
    MensajeInicio();

    //*** 1.2 InicializarVariables
    InicializarVariables();

    //*** 1.3 MostrarCargando
    MostrarCargando();

    //*** 1.4 Cargar Rondas de Eleccion
    setTimeout(function () {
        //*** 1.4.1 Cargar Rondas de Eleccion
        AjaxCargarElecciones(Uri, GestionarCargaElecciones);

    }, 500);

});
//*** #1.4.2
function GestionarCargaElecciones() {

    //*** 1.4.2 Click en una Ronda
    $('.btn-ronda').on('click', function () {

        //*** 1.4.3 Si la Ronda está Activa
        if ($(this).hasClass('bg-success')) {

            //*** Definir valores de Elección y Ronda
            //Get
            sessionStorage.setItem('Id_eleccion', $(this).attr("idE"));
            sessionStorage.setItem('Id_ronda', $(this).attr("idR"));
            //Set
            Id_eleccion = sessionStorage.getItem('Id_eleccion');
            Id_ronda = sessionStorage.getItem('Id_ronda');

            //**** 1.4.4 Validar Papeleta Actual
            AjaxCargarPapeletaActual_1(Login, Id_eleccion, Id_ronda, Id_delegado);

        } else {

            var nombreEleccion = $(':first-child h3', this).text();
            var nombreRonda = $(':first-child h5', this).text();

            //*** Modal BootstrapFire
            swalWithBootstrapButtons.fire({
                title: nombreEleccion + ' - ' + nombreRonda
                , html: '<h4>La ronda seleccionada ( ' + nombreRonda + ' ) está <b>INACTIVA</b>.</h4>'
                , timer: 6000
                , type: 'info'
            });
        }
    });
}
//*** (1.1) #2 - MensajeInicio
function MensajeInicio() {

    if (parseInt(sessionStorage.getItem('Sesion_act')) <= 2) {

        Toast.fire({
            target: '.content-wrapper',
            toast: false,
            type: 'info',
            title: 'INFORME DE PUESTOS EN ELECCIÓN',
            html: '<h5>' +
                '<i class="fas fa-arrow-circle-right"></i><b> Consejo de Administración</b></br><div class="dropdown-divider"></div>' +
                '- 4 Miembros propietarios.</br></br>' +
                '<i class="fas fa-arrow-circle-right"></i><b> Comité de Vigilancia</b></br><div class="dropdown-divider"></div>' +
                '- 3 Miembros propietarios</br> ' +
                '- 1 Miembro suplente por 1 año.</br></br>' +
                '<i class="fas fa-arrow-circle-right"></i><b> Comité de Educación y Bienestar Social</b></br><div class="dropdown-divider"></div>' +
                '- 2 Miembros propietarios.</br></br>' +
                '<i class="fas fa-arrow-circle-right"></i><b> Comité de Nominaciones</b></br><div class="dropdown-divider"></div>' +
                '- 2 Miembros propietarios.</br>' +
                '- 1 Miembro suplente.</h5></br></br>',
            allowOutsideClick: false,
            timer: 10000
        });
    }

}
//*** (1.2) #3 - InicializarVariables
function InicializarVariables() {

    //*** Definición de Variables Locales
    Id_delegado = sessionStorage.getItem('Id_delegado');
    Login = sessionStorage['Cedula'];
    Uri = sessionStorage['Uri'];
}
//*** (1.4.1) #4 AjaxCargarElecciones
function AjaxCargarElecciones(Uri, CallbackFN) {

    //*** 4.1 - Llamada Ajax ListadoRondas
    $.ajax({
        url: Uri + '/api/Asamblea/EleccionRonda/ListadoRondas',
        method: 'GET',
        async: true,
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {

            //*** 4.1.1 - Si no hay datos mostrar mensaje
            if (data.respuesta === 0) {

                swalWithBootstrapButtons.fire({
                    title: 'SISTEMA DE ELECCIONES'
                    , html: '<h4> No se encontraron Elecciones que cumplan las condiciones requeridas: </h4></br> ' +
                        '<b>1 - Estado ACTIVO.</b></br>' +
                        '<b>2 - Proceso de QUORUM finalizado.</b></b>',
                    type: 'info',
                    confirmButtonText: 'VOLVER'
                });

            } else {

                //*** 4.1.2 - Cargar lista de Rondas de Elección
                var Lista = [];

                $.each(data.ObjRondas, function (index, val) {
                    var myCol = $('<div class="col-md-12 col-sm-12 col-12 "></div>');
                    var myPanel = $('<div style="border: black solid 1px;" class="small-box bg-' + TipoRondaBG(val.I_estado) + ' btn-ronda" idE="' + val.Id_eleccion + '" idR="' + val.Id_ronda + '">' +
                        '<div class="inner"><h3>' + val.Descripcion + '</h3><h5>' + val.Nombre + '</h5></div>' +
                        '<div class="icon text-dark"><i class="fas fa-users"></i></div >' +
                        '<a href="#" style="border-top: black solid 2px;" class="small-box-footer">IR A VOTAR&nbsp;&nbsp;<i class="fas fa-arrow-circle-right"></i></a>');

                    myPanel.appendTo(myCol);
                    Lista.push(myCol);
                });

                //*** Título de Rondas Activas
                var titulo = '<h5 class="mb-2 text-bold text-center" style="background-color: #8abcf1; border-radius: .25rem;">' +
                    '<a style="vertical-align: inherit;">LISTA DE RONDAS EXISTENTES</a>' +
                    '</h5 >';
                //*** Título
                $('#eleccionRondas').before(titulo);
                //*** Elecciones
                $('#eleccionRondas').append(Lista);
                //*** Llamada del método GestionarCargaElecciones() > CallbackFN
                CallbackFN();
            }
        },
        error: AjaxError

    }).done(function () {
        CerrarCargando();
    });
}
//*** (4.1.2) #5 TipoRondaBG
function TipoRondaBG(i_estado) {
    if (i_estado === 'A') {
        return 'success';
    } else {
        return 'danger';
    }
}
//*** (1.4.4) #6 AjaxCargarPapeletaActual #1
function AjaxCargarPapeletaActual_1(pLogin, pId_eleccion, pId_ronda, pId_delegado) {

    //***Cargar Objeto Papeleta Actual
    var parametros = new Object();

    parametros.Usuario = pLogin;
    parametros.Id_eleccion = pId_eleccion;
    parametros.Id_ronda = pId_ronda;
    // Papeleta se define en 0 
    parametros.Id_papeleta = 0;
    parametros.Id_delegado = pId_delegado;

    var data = JSON.stringify({ parametros });

    $.ajax({
        url: Uri + '/api/Asamblea/PapeletaRonda/PapeletaActual',
        method: 'POST',
        data: data,
        async: true,
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {

            if (data !== 'undefined' && data !== '' && data !== null) {

                //***6.1 - Variables de Sesión
                sessionStorage.setItem('Id_papeleta', data.Id_papeleta);
                sessionStorage.setItem('Num_papeletas', data.Num_papeletas);
                sessionStorage.setItem('Num_votos', data.Num_votos);
                sessionStorage.setItem('Id_voto_blanco', data.Id_voto_blanco);
                sessionStorage.setItem('Nombre_papeleta', data.Nombre);
                sessionStorage.setItem('Color_card', data.Color_card);
                sessionStorage.setItem('Color_background', data.Color_background);
                sessionStorage.setItem('Contador_papeletas', data.Contador_papeletas);

                //***6.2  Iniciar Votaciones
                redirect('Votaciones.aspx');

            } else {
                Toast.fire({
                    toast: false,
                    type: 'error',
                    title: 'CARGA DE PAPELETAS',
                    html: '<h5>El usuario no tiene papeletas disponibles para votar. Saliendo del sistema...</h5></br>',
                    allowOutsideClick: true,
                    timer: 5000
                }).then(
                    function () {
                        //*** 6.3 - Redireccionar
                        LimpiarRedireccionar();
                        //*** 6.4 - Cerrar Modal Cargando
                        CerrarCargando();
                    }
                );
            }
        },
        error: AjaxError

    }).done(function () {
        CerrarCargando();
    });
}
//*** #6 Función para Debug de Variables
function DebugVariables() {

    console.log("Id_papeleta:        " + sessionStorage.getItem('Id_papeleta'));
    console.log("Num_papeletas:      " + sessionStorage.getItem('Num_papeletas'));
    console.log("Num_votos:          " + sessionStorage.getItem('Num_votos'));
    console.log("Id_voto_blanco:     " + sessionStorage.getItem('Id_voto_blanco'));
    console.log("Nombre_papeleta:    " + sessionStorage.getItem('Nombre_papeleta'));
    console.log("Color_card:         " + sessionStorage.getItem('Color_card'));
    console.log("Color_background:   " + sessionStorage.getItem('Color_background'));
    console.log("Contador_papeletas: " + sessionStorage.getItem('Contador_papeletas'));
}

//*** #7 - LimpiarRedireccionar
function LimpiarRedireccionar() {

    //*** 7.1 - Limpiar sessionStorage
    sessionStorage.clear();
    //*** 7.2 - Redireccionar
    redirect('Login.aspx');
}