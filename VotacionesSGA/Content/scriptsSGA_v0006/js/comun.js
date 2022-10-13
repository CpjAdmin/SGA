var apiUrl = "";

//*** Alertas Swal.fire
function alertaExito(titulo, detalle) {

    Swal.close();
    Swal.fire({
        type: 'success',
        title: titulo,
        text: detalle,
        timer: 5000
    });
}

function alertaInfo(titulo, detalle) {
    Swal.close();

    Swal.fire({
        type: 'info',
        html: detalle,
        title: titulo,
        timer: 15000
    });
}

function alertaError(titulo, detalle) {
    Swal.close();

    Swal.fire({
        type: 'error',
        title: titulo,
        text: detalle,
        timer: 15000
    });
}

//*** Aletar Toastr
function alertaErrorMin(detalle) {
    toastr.error(detalle);
}

function alertaInfoMin(detalle) {
    toastr.info(detalle);
}

function alertaExitoMin(detalle) {
    toastr.success(detalle);
}

function alertaExitoMin2(detalle) {
    toastr.info(detalle);
}

//*** Método para salir del Sistema
function alertaSalir(titulo, detalle) {
    Swal.close();
    Swal.fire({
        title: titulo,
        text: detalle,
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Aceptar'
    }).then(function (result) {
        if (result.value) {

            //*** Limpiar sessionStorage
            sessionStorage.clear();
            //*** Redireccionar a Login.aspx
            window.location = "Login.aspx";
        }
    });
}

function MostrarMensaje(mensaje) {
    Swal.fire(mensaje);
}

function MostrarCargando() {
    $('body').append('<div id="loading"> <div id="cargando" class="spinner"></div></div>');
    $('#loading').show();
}

function CerrarCargando() {
    $('#loading').hide();
    $('#loading').remove();
}