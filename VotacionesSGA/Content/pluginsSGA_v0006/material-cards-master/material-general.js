//*** PARAMETROS GLOBALES
const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
});

const Toast = Swal.mixin({
    position: 'center',
    showConfirmButton: false
});

//*** Seguridad
var Login;
var Cedula;
var Id_usuario;
var Id_delegado;
var Clave;
var Uri;
//*** Votación
var Id_eleccion;
var Id_ronda;
var Id_papeleta;
//*** Cantidades
var Contador_papeletas;
var Num_papeletas;
var Num_votos;
//*** Detalle votación
var Id_voto_blanco;
var Nombre_papeleta;
var Color_papeleta;
//*** Variables Locales
var Lista_candidatos = [];

//*** #1 - Inicio
$(document).ready(function () {

    //*** Eliminar Botones
    if ($('#btnSalir').length > 0) {
        $('#btnSalir').remove();
    }
    if ($('#btnInicio').length > 0) {
        $('#btnInicio').remove();
    }
    //*** Div ListaVotos para cargar los candidatos
    if (!$('#listaVotos').length > 0) {
        $('.main-header').append('<div id="listaVotos" class="row"></div>');
    }

    //*** 1.1 - InicializarVariables / AlistarFormulario > CargarCandidatos
    InicializarVariables(CargarCandidatos);

});
//*** (1.1) #2 - Funcion InicializarVariables
function InicializarVariables(CallbackFN) {

    //console.log("InicializarVariables - Inicio");

    //*** 2.1 Definición de valores Inicial
    // Contador de Papeletas
    Contador_papeletas = parseInt(sessionStorage.getItem('Contador_papeletas'));
    // Asignación de Variables Generales
    Login = sessionStorage.getItem('Cedula');
    Cedula = sessionStorage.getItem('Cedula');
    Id_usuario = sessionStorage.getItem('Id');
    Id_delegado = sessionStorage.getItem('Id_delegado');
    Clave = sessionStorage.getItem('Clave');
    Uri = sessionStorage.getItem('Uri');
    // Asignación de Variables Votacion
    Id_eleccion = parseInt(sessionStorage.getItem('Id_eleccion'));
    Id_ronda = parseInt(sessionStorage.getItem('Id_ronda'));
    Id_papeleta = parseInt(sessionStorage.getItem('Id_papeleta'));
    Num_papeletas = parseInt(sessionStorage.getItem('Num_papeletas'));
    Num_votos = parseInt(sessionStorage.getItem('Num_votos'));
    // Detalle votación
    Id_voto_blanco = sessionStorage.getItem('Id_voto_blanco');
    Nombre_papeleta = sessionStorage['Nombre_papeleta'];
    Color_papeleta = sessionStorage['Color_background'];

    //*** Debug
    //DebugVariables();

    //*** Mostrar el modal al Alistar el formulario
    mostrarModal('modalCargando', Nombre_papeleta);

    //*** 2.2 Alistar Formulario
    AlistarFormulario(Nombre_papeleta, Num_votos, Color_papeleta);

    //*** 2.3 CallBack de Carga de Candidatos
    CallbackFN();

    //console.log("InicializarVariables - Fin");
}
//*** (2.2) #3 - AlistarFormulario
function AlistarFormulario(Nombre_papeleta, Num_votos, Color_papeleta) {

    //console.log("AlistarFormulario - Inicio");

    //*** Mapeo Titulo Papeleta
    $('#card-titulo').text(Nombre_papeleta);

    //*** Votos Disponibles
    var navItem = $('<li class="nav-item"></li>');
    var navContent = $('<a class="text-danger text-bold">&nbsp;VOTOS DISPONIBLES :</a><span class="circulo" id="VTDisponibles" style="font-size: 1.1em;">' + Num_votos + '</span>');
    navContent.appendTo(navItem);
    navItem.appendTo('#nav-header');

    //*** Color Fondo
    $(".content").css("background-color", Color_papeleta);

    //console.log("AlistarFormulario - Fin");
}
//*** (1.1 - 2.3) #4 - CargarCandidatos
function CargarCandidatos() {

    //console.log("CargarCandidatos - Inicio");

    //*** 4.1 Cargar Candidatos
    AjaxCargarCandidatos(AccionesCargaCandidatos);

    //console.log("CargarCandidatos - Fin");
}
//*** (4.1) #5 - AjaxCargarCandidatos
function AjaxCargarCandidatos(CallbackFN) {

    //console.log("AjaxCargarCandidatos - Inicio");

    var urlAjax = Uri + '/api/candidato/' + Id_eleccion + '/' + Id_ronda + '/' + Id_papeleta;

    $.ajax({
        url: urlAjax,
        method: 'GET',
        async: true,
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {

            if (data.length === 0) {

                swalWithBootstrapButtons.fire({
                    title: 'PAPELETA #' + Id_papeleta,
                    html: '<h4>No existen candidatos asociados a esta papeleta.</h4>',
                    type: 'info',
                    confirmButtonText: 'VOLVER'
                });

            } else {

                //*** Crear Material-Cards
                ListaCandidatos = [];

                $.each(data, function (index, val) {
                    var myCol = $('<div class="col-lg-5 col-md-4 col-sm-6 col-xs-12"></div>');
                    var myPanel = $('<article id="Card-' + val.Cedula + '" class="material-card ' + sessionStorage.getItem('Color_card') + '"><h2><span>' + val.Nombre + '</span>' + votoBlanco(val.Cedula, val.Num_posicion) + '</h2>' +
                        '<div class= "mc-content"><div class="img-container"><img id="img-' + val.Id_candidato + '" class="img-responsive" src="../Images/Candidatos/' + val.Foto + '"></div>' +
                        '<div style="text-align:center;" class="mc-description"><h5><i class="far fa-5x fa-check-circle" style="color:red"></i></h5></div></div > <a class="mc-btn-action" id="' + val.Id_candidato + '"><i class="fa fa-plus fa-2x" style="color:red"></i></a> <div style="text-align:center;" class="mc-footer">' +
                        '</div ></article > ');

                    myPanel.appendTo(myCol);
                    ListaCandidatos.push(myCol);
                });

                $('#containerPanel').append(ListaCandidatos);

                //*** 5.1 CallBack de Acciones para Carga de Candidatos
                CallbackFN();
            }
        },
        error: AjaxError
    });

    //console.log("AjaxCargarCandidatos - Fin");
}
//*** (4.1) #5.1 - AjaxCargarCandidatos
function AccionesCargaCandidatos() {

    //console.log("AccionesCargaCandidatos - Inicio");

    //*** Evento Clic Candidato
    $('.material-card > .mc-btn-action').click(function () {

        //*** Variables de Card
        var candidato = $(this).attr('id');
        var card = $(this).parent('.material-card');
        var icon = $(this).children('i');

        icon.addClass('fa-spin');

        //*** Desmarcar / Marcar Cards
        if (card.hasClass('mc-active')) {

            //*** 4.2 Descarmar Card
            DescarmarSeleccion(candidato, card, icon);
            //*** Actualizar Contador
            $('#VTDisponibles').text(Num_votos);

        } else {
            //*** Valida Votos = 0
            if (Num_votos === 0) {

                swalWithBootstrapButtons.fire({
                    title: 'NO TIENE VOTOS DISPONIBLES'
                    , html: '<h4>Totales <b>( ' + sessionStorage['Num_votos'] + ' )</b> - Disponibles  <b>( ' + Num_votos + ' )</b></h4>'
                    , type: 'info'
                    , confirmButtonText: 'VOLVER'
                    , timer: 5000
                });

            } else {
                //*** 4.3 Marcar Card
                MarcarSeleccion(candidato, card, icon);
                //*** Actualizar Contador
                $('#VTDisponibles').text(Num_votos);
            }
        }
    });

    //*** Enviar el scroll al Inicio al cargarse la papeleta.
    setTimeout(function () {
        $('html, body').animate({ scrollTop: 0 });
    }, 500);
    //***Ocultar el Modal en 2 segundos
    setTimeout(function () {
        ocultarModal('modalCargando');
    }, 2000);

    //***Ocultar el Modal en 6 segundos si está activo ( Para internet lento )
    setTimeout(function () {
        if ($('#modalCargando').hasClass('show')) {
            ocultarModal('modalCargando');
        }
    }, 6000);

    //console.log("AccionesCargaCandidatos - Fin");
}
//*** (4.2) #6 - Desmarcar Seleccion
function DescarmarSeleccion(candidato, card, icon) {

    //console.log("DescarmarSeleccion - Inicio");

    //*** Esconder botón Votar
    if ($('#btnVotar').length) {
        $('#btnVotar').remove();
    }
    //*** Eliminar candidato de Array
    for (var i = 0; i < Lista_candidatos.length; i++) {

        if (Lista_candidatos[i] === candidato) {
            Lista_candidatos.splice(i, 1);
            i--;

            //*** Votos disponibles
            Num_votos++;
        }
    }
    //*** Quita imagen candidato
    $('#listaVotos .column#img-voto-' + candidato).remove();
    //*** Quitar Clase mc-active
    card.removeClass('mc-active');
    //*** Cambio en icono
    window.setTimeout(function () {
        icon.removeClass('fa-minus').removeClass('fa-spin').addClass('fa-plus');
    }, 500);

    //console.log("DescarmarSeleccion - Fin");
}
//*** (4.2) #7 - Marcar Seleccion
function MarcarSeleccion(candidato, card, icon) {

    //console.log("MarcarSeleccion - Inicio");

    var Agregados;

    if (candidato === Id_voto_blanco) {
        Agregados = Num_votos;
    } else {
        Agregados = 1;
    }
    //*** Agregar candidato al Array
    for (var i = 0; i < Agregados; i++) {

        //*** Agrega a Lista Candidatos
        Lista_candidatos.push(candidato);
        //*** Votos disponibles --
        Num_votos--;
        //*** Agrega Imagen candidato
        var imagenSrc = $('div.img-container #img-' + candidato + '').attr('src');

        $('#listaVotos').append('<div class="column" id="img-voto-' + candidato + '">' +
            '<img src="' + imagenSrc + '" style="width:100%">' +
            '</div>');
    }
    //*** Agregar botón Votar
    if (Num_votos === 0) {

        //*** Agregar botón 
        var avisoVotar = $('<div id="avisoVotar" class="container-btn"><span class="circle">' +
            '<i class="fa fa-arrow-down fa-btn"></i></span><span class="pulse"></span>' +
            '</div >');
        var btnVotar = $('<button type="button" id="btnVotar" class="btn btn-block btn-success btn-lg text-bold">VOTAR</button>');

        btnVotar.prepend(avisoVotar);
        btnVotar.appendTo('.card-footer');

        //*** Enviar el scroll al Final cuando los Votos Disponibles es 0.
        $('html, body').animate({ scrollTop: $(document).height() }, 500);

        //*** 7.1 Función Votar
        Votar();
    }

    //*** Agregar Clase mc-active
    card.addClass('mc-active');
    //*** Cambio en icono
    window.setTimeout(function () {
        icon.removeClass('fa-plus').removeClass('fa-spin').addClass('fa-minus');
    }, 500);

    //console.log("MarcarSeleccion - Fin");
}
//*** (7.1) #8 - Función Votar
function Votar() {

    //*** Evento al precionar CLIC en Votar
    $("#btnVotar").on("click", function () {

        //*** Evento swalWithBootstrapButtons
        swalWithBootstrapButtons.fire({
            title: $('#listaVotos').clone().prop('id', 'listaVotos2'),
            html: "<h4>Está seguro de su selección ?</h4>",
            type: 'success',
            showCancelButton: true,
            confirmButtonText: '--- SI ---',
            cancelButtonText: '--- NO ---',
            reverseButtons: true

        }).then(function (result) {

            //*** Al presionar '- SI -'
            if (result.value) {

                //*** 8.1 - Moscar Modal Cargando
                MostrarCargando();

                //*** 8.2 - Registrar el Voto
                AjaxRegistrarVoto(Lista_candidatos.join(','), GestionarRegistrarVoto);

                //*** Al presionar '- NO -'
            } else if (result.dismiss === Swal.DismissReason.cancel) {

                //*** 8.9 -  Mensaje Voto Cancelado
                mensajeVotoCancelado();
            }
        });
    });
}
//*** (8.2) #9 - AjaxRegistrarVoto
function AjaxRegistrarVoto(Lista_candidatos, CallbackFN) {

    var voto = {
        Id_delegado: Id_delegado,
        Id_eleccion: Id_eleccion,
        Id_ronda: Id_ronda,
        Id_papeleta: Id_papeleta,
        Id_voto: Num_papeletas,  // Para Identificar el número de Voto
        Lista_candidatos: Lista_candidatos,
        Usuario: Login
    };

    $.ajax({
        url: Uri + '/api/Voto/RegistrarVoto',
        method: 'POST',
        async: true,
        contentType: "application/json",
        dataType: 'json',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        data: JSON.stringify(voto),
        success: function (data) {

            //*** SI el resultado es TRUE
            if (data) {

                //*** 9.1 - Evento FNCallBack
                CallbackFN();

            } else {
                //*** 9.2 - Mensaje Voto no Guardado
                mensajeVotoNoGuardado();
            }
        },
        error: AjaxError
    });
}
//*** (8.2 | 9.1) #9.1 - GestionarRegistrarVoto
function GestionarRegistrarVoto() {

    //*** Aumenta Contador Global de Papaletas
    sessionStorage.setItem('Contador_papeletas', Contador_papeletas + 1);
    Contador_papeletas = parseInt(sessionStorage.getItem('Contador_papeletas'));

    //*** 9.1.1 - Verificar si es la última papeleta
    if (Contador_papeletas === Num_papeletas) {

        //*** (9.1.2) - Mensaje de Voto Guardado con Exito ( Último Voto )
        mensajeVotoGuardadoConExito();

    } else {

        //*** 9.1.1.1 - Mensaje de Voto guardado
        Toast.fire({
            toast: false,
            type: 'success',
            title: 'VOTO GUARDADO',
            html: '<h5>Cargando la próxima papeleta...</h5></br>',
            allowOutsideClick: false,
            timer: 3000

        }).then(function () {

            //*** 9.1.3 - Validar Papeleta Actual #2
            AjaxCargarPapeletaActual_2(Login, Id_eleccion, Id_ronda, Id_delegado);
          
        });
    }
}
//*** (8.5) #10 - LimpiarRedireccionar
function LimpiarRedireccionar() {

    //*** 10.1 - Limpiar sessionStorage
    sessionStorage.clear();
    //*** 10.2 - Redireccionar
    redirect('Login.aspx');
}
//*** (8.6) #11 - AjaxCargarPapeletaActual_2 #2
function AjaxCargarPapeletaActual_2(pLogin, pId_eleccion, pId_ronda, pId_delegado) {

    //***Cargar Objeto Papeleta Actual
    var parametros = new Object();

    parametros.Usuario = pLogin;
    parametros.Id_eleccion = pId_eleccion;
    parametros.Id_ronda = pId_ronda;
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

                //*** Redefinir valores de las Variables de Sesión 
                sessionStorage.setItem('Id_papeleta', data.Id_papeleta);
                sessionStorage.setItem('Num_papeletas', data.Num_papeletas);
                sessionStorage.setItem('Num_votos', data.Num_votos);
                sessionStorage.setItem('Id_voto_blanco', data.Id_voto_blanco);
                sessionStorage.setItem('Nombre_papeleta', data.Nombre);
                sessionStorage.setItem('Color_card', data.Color_card);
                sessionStorage.setItem('Color_background', data.Color_background);
                sessionStorage.setItem('Contador_papeletas', data.Contador_papeletas);

                //*** 9.1.4 - Recargar con Nueva Papeleta - Recargar Votaciones.aspx
                location.reload();

            } else {
                //*** 9.1.5 Mensaje de Papeletas Disponibles
                mensajeNoPapeletasDisponibles();
            }
        },
        error: AjaxError

    }).done(function () {
        CerrarCargando();
    });
}
//*** (5  ) #12 -  Voto en Blanco
function votoBlanco(cedula, num_posicion) {
    //*** Posición del voto en blanco siempre es 100 a nivel de Base de Datos
    if (cedula !== '0') {
        return "<strong># " + num_posicion + "</strong>";
    } else {
        return "<strong>*</strong>";
    }
}
//*** (2.1) #13 - Función para Debug de Variables
function DebugVariables() {

    //*** Log de Variables
    console.log('Uri               : ' + sessionStorage['Uri']);
    console.log('Id_eleccion       : ' + sessionStorage['Id_eleccion']);
    console.log('Id_ronda          : ' + sessionStorage['Id_ronda']);
    console.log('Id_papeleta       : ' + sessionStorage['Id_papeleta']);
    console.log('Num_papeletas     : ' + sessionStorage['Num_papeletas']);
    console.log('Num_votos         : ' + sessionStorage['Num_votos']);
    console.log('Nombre_papeleta   : ' + sessionStorage['Nombre_papeleta']);
    console.log('Color_card        : ' + sessionStorage['Color_card']);
    console.log('Color_background  : ' + sessionStorage['Color_background']);
    console.log('Id_voto_blanco    : ' + sessionStorage['Id_voto_blanco']);
    console.log('Contador_papeletas: ' + sessionStorage['Contador_papeletas']);
    console.log('Cedula            : ' + sessionStorage['Cedula']);
    console.log('Nombre            : ' + sessionStorage['Nombre']);
    console.log('Id_usuario        : ' + sessionStorage['Id']);
    console.log('Id_delegado       : ' + sessionStorage['Id_delegado']);
    console.log('Token             : ' + sessionStorage['Token']);
    console.log('Rol               : ' + sessionStorage['Rol']);

}
//*** (8.8) #14 - Mensaje de Voto No Guardado 
function mensajeVotoNoGuardado() {

    //*** 14.1 - Mensaje Voto no Guardado
    Toast.fire({
        toast: false,
        type: 'error',
        title: 'VOTO NO GUARDADO',
        html: '<h5>El voto de esta papeleta no fue guardado, se reiniciara su sesión.</h5></br>',
        allowOutsideClick: false,
        timer: 5000

    }).then(function () {

        //*** Redireccionar
        LimpiarRedireccionar();
        //*** Cerrar Modal Cargando
        CerrarCargando();
    });
}
//*** (8.9) #15 - Mensaje de Voto Cancelado 
function mensajeVotoCancelado() {
    //*** 15.1 -  Mensaje Voto Cancelado
    swalWithBootstrapButtons.fire({
        title: 'VOTO CANCELADO'
        , html: '<strong>Modifique su selección para votar nuevamente.</strong>'
        , type: 'info'
        , confirmButtonText: 'VOLVER'
    });
}
//*** (8.7) #16 - Mensaje de NO existen papeletas disponibles para votar
function mensajeNoPapeletasDisponibles() {
    //*** 16.1 -  Mensaje Voto Cancelado
    Toast.fire({
        toast: false,
        type: 'error',
        title: 'CARGA DE PAPELETAS',
        html: '<h5>El usuario no tiene papeletas disponibles para votar. Saliendo del sistema...</h5></br>',
        allowOutsideClick: true,
        timer: 5000
    }).then(
        function () {
            //*** Redireccionar
            LimpiarRedireccionar();
            //*** Cerrar Modal Cargando
            CerrarCargando();
        }
    );
}
//*** (8.3) #17 - Mensaje de Voto Guardado con Exito 
function mensajeVotoGuardadoConExito() {

    //*** 17.1 - Mensaje de Voto Guardado con Exito
    let timerInterval;

    swalWithBootstrapButtons.fire({
        title: sessionStorage['Nombre']
        , html: '<h4>VOTO GUARDADO CON ÉXITO&nbsp;<i class="fa fa-spinner fa-spin"></i>&nbsp;<strong class="text-danger"></strong></h4>'
        , footer: '<a><strong>SU VOTO HA SIDO IMPRESO DIGITALMENTE</strong></br>' +
            ' - Votación finalizada.</br>' +
            ' - Gracias por cumplir su deber de Delegado.</br>' +
            ' - Su apoyo es valioso para COOPECAJA.</a>'
        , imageUrl: 'Images/UI/logo_coopecaja.png'
        , imageHeight: 140
        , type: 'success'
        , timer: 10000  // Mensaje por 10 Segundos
        , allowOutsideClick: false
        , showConfirmButton: false,
        onBeforeOpen: function () {
           
            //***Paso 1
            timerInterval = setInterval(function () {
                //***Paso 2
                swal.getContent().querySelector('strong').textContent = Math.ceil(swal.getTimerLeft() / 1000);

            }, 100);
        },
        //***Paso 3
        onClose: function () {
            clearInterval(timerInterval);
        }
    }).then(function () {

        //*** 17.2 - Cerrar Modal Cargando
        CerrarCargando();
        //*** 17.3 - Limpiar sessionStorage y Redireccionar
        LimpiarRedireccionar();
    });
}