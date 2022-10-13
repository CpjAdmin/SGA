var pidEleccion = '';
var pNombreEleccion = '';

var pidEleccionR = '';
var PNombreEleccionR = '';

var pidPapeletaC = '';
var apiUrl = sessionStorage['Uri'];
var objtablaE = $('#tablaEleccion');
var tablaE;
var objtablaER = $('#tablaEleccionR');
var tablaER;
var objtablaPR = $('#tablaEleccionPR');
var tablaPR;
var objtablaPC = $('#tablaPapeletaC');
var tablaPC;

var nombreReporte = 'Lista de Elecciones';
var DatosE = {
    "Id_eleccion": '',
    "Nombre": '',
    "Descripcion": '',
    "F_inicio": '',
    "F_final": '',
    "I_estado": '',
    "I_cierre_quorum": '',
    "Usuario": '',
    "Terminal": ''
};

var DatosER = {
    "Id_eleccion": '',
    "Id_ronda": '',
    "Nombre": '',
    "Descripcion": '',
    "F_inicio": '',
    "F_final": '',
    "I_estado": '',
    "Usuario": '',
    "Terminal": ''
};

var DatosPR = {
    "Id_eleccion": '',
    "Id_ronda": '',
    "Id_papeleta": '',
    "Nombre": "",
    "Descripcion": '',
    "Num_votos": 0,
    "Orden": 0,
    "I_estado": '',
    "Usuario": '',
    "Terminal": ''
};

var DatosPC = {
    "Id_eleccion": '',
    "Id_ronda": '',
    "Id_papeleta": '',
    "Id_candidato": '',
    "Num_posicion": 0,
    "Cedula": '',
    "Nombre": '',
    "Foto": '',
    "Descripcion": '',
    "I_estado": '',
    "Usuario": '',
    "Terminal": ''
};

$(document).ready(function () {
    CargarTablaElecciones();
    obtenerPapeletas();
    $('.toggle').width(150);

    $('#chkTipo').change(function () {
        if ($(this).is(":checked")) {
            CargarTablaCandidatos(pidEleccion, pidEleccionR, pidPapeletaC);
        } else {
            CargarTablaCandidatosT(pidEleccion, pidEleccionR, pidPapeletaC);
        }
    });

    $('#dtpRango, #dtpRangoR').daterangepicker({
        timePicker: true,
        timePickerIncrement: 30,
        locale: {
            format: 'DD/MM/YYYY hh:mm A'
        }
    });
    
    //eventos para la tabla elecciones
    $(document).on('click', '#btnAgregar', function () {
        $('#modal-lg-eleccion').data('tipo', 'guardar');
        $('#modal-lg-eleccion').modal('show');
        $("#lblEleccion").text('Nueva Elección');
        $("#id").val('0');
        $("#nombre").val('');
        $("#descripcion").val('');
        $('#chkactivo').prop('checked', true).change();
        $('#chquorum').prop('checked', false).change();

    });

    $(document).on('click', '#tablaEleccion .tooltipImgM', function () {

        var id = $(this).closest("tr").find('td:eq(0)').text();
        var nombre = $(this).closest("tr").find('td:eq(1)').text();
        var descripcion = $(this).closest("tr").find('td:eq(2)').text();
        var inicio = $(this).closest("tr").find('td:eq(3)').text();
        var fin = $(this).closest("tr").find('td:eq(4)').text();
        var estado = $(this).closest("tr").find('td:eq(5)').text();
        var quorum = $(this).closest("tr").find('td:eq(6)').text();

        //Estas dos variables indican si se debe seleccionar el modulo de acuerdo a la fila seleccionada
        $('#modal-lg-eleccion').data('tipo', 'modificar');
        $('#modal-lg-eleccion').modal('show');
        $("#lblEleccion").text('Modificar Elección');

        $("#id").val(id);
        $("#nombre").val(nombre);
        $("#descripcion").val(descripcion);
        $('#dtpRango').data('daterangepicker').setStartDate(inicio);
        $('#dtpRango').data('daterangepicker').setEndDate(fin);
        estado == 'Activo' ? $('#chkactivo').prop('checked', true).change() : $('#chkactivo').prop('checked', false).change();
        quorum == 'Si' ? $('#chkquorum').prop('checked', true).change() : $('#chkquorum').prop('checked', false).change();
    });

    $(document).on('click', '#tablaEleccion .tooltipImgB', function () {
        var id = $(this).closest("tr").find('td:eq(0)').text();
        Swal.close();
        Swal.fire({
            title: 'Eliminar Datos',
            text: '¿Desea Eliminar la elección seleccionada?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                EliminarEleccion(id);
            }
        });
    });

    $(document).on('click', '#btnGuardar', function () {
        //$('#dtpRango').data('daterangepicker').setStartDate('03/01/2014');
        //$('#dtpRango').data('daterangepicker').setEndDate('03/31/2014');
        //var inicio = $('#dtpRango').data('daterangepicker').startDate.format('DD/MM/YYYY hh:mm A');
        //var fin = $('#dtpRango').data('daterangepicker').endDate.format('DD/MM/YYYY hh:mm A');
        //alert(fin.toString());

        if (validarDatosEleccion() == 1) {
            return;
        }

        Swal.close();
        Swal.fire({
            title: 'Guardar Datos',
            text: '¿Desea guardar los datos?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                $('#modal-lg-eleccion').modal('hide');
                GuardarModificarEleccion($('#modal-lg-eleccion').data('tipo'));

            }
        });
    });

    $(document).on('click', '#tablaEleccion .selected', function () {
        var idEleccion = $(this).find('td:eq(0)').text();
        pidEleccion = idEleccion;
        pNombreEleccion = $(this).find('td:eq(1)').text();
        pidEleccionR = '';
        PNombreEleccionR = '';
        pidPapeletaC = '';
        $("#tituloRondaE").text("Rondas para la elección " + idEleccion + " - " + pNombreEleccion);
        //tituloRondaE
        CargarTablaEleccionesR(idEleccion);
    });
    //FIN eventos para la tabla elecciones

    //eventos para la tabla elecciones ronda
    $(document).on('click', '#btnAgregarR', function () {
        if (pidEleccion == '' || pidEleccion == null || pidEleccion== undefined) {
            alertaError("Nueva Ronda", "Debe seleccionar una elección para poder crear una ronda de elección.");
            return;
        }
        $('#modal-lg-eleccionR').data('tipo', 'guardar');
        $('#modal-lg-eleccionR').modal('show');
        $("#lblEleccionR").text('Nueva Ronda de Elección');
        $("#idR").val('0');
        $("#nombreR").val('');
        $("#descripcionR").val('');
        $('#chkactivoR').prop('checked', true).change();
    });

    $(document).on('click', '#tablaEleccionR .tooltipImgM', function () {
        var id = $(this).closest("tr").find('td:eq(0)').text();
        var nombre = $(this).closest("tr").find('td:eq(1)').text();
        var descripcion = $(this).closest("tr").find('td:eq(2)').text();
        var inicio = $(this).closest("tr").find('td:eq(3)').text();
        var fin = $(this).closest("tr").find('td:eq(4)').text();
        var estado = $(this).closest("tr").find('td:eq(5)').text();
        //console.log(inicio);
        //console.log(fin);

        //Estas dos variables indican si se debe seleccionar el modulo de acuerdo a la fila seleccionada
        $('#modal-lg-eleccionR').data('tipo', 'modificar');
        $('#modal-lg-eleccionR').modal('show');
        $("#lblEleccionR").text('Modificar Ronda de Elección');

        $("#idR").val(id);
        $("#nombreR").val(nombre);
        $("#descripcionR").val(descripcion);
        $('#dtpRangoR').data('daterangepicker').setStartDate(inicio);
        $('#dtpRangoR').data('daterangepicker').setEndDate(fin);
        estado == 'Activo' ? $('#chkactivoR').prop('checked', true).change() : $('#chkactivoR').prop('checked', false).change();
    });

    $(document).on('click', '#tablaEleccionR .tooltipImgB', function () {
        var id = $(this).closest("tr").find('td:eq(0)').text();
        Swal.close();
        Swal.fire({
            title: 'Eliminar Datos',
            text: '¿Desea Eliminar la ronda de elección seleccionada?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                EliminarEleccionR(id);
            }
        });
    });

    $(document).on('click', '#btnGuardarR', function () {
        if (validarDatosEleccionR() == 1) {
            return;
        }
        Swal.close();
        Swal.fire({
            title: 'Guardar Datos',
            text: '¿Desea guardar los datos?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                $('#modal-lg-eleccionR').modal('hide');
                GuardarModificarEleccionR($('#modal-lg-eleccionR').data('tipo'));

            }
        });
    });

    $(document).on('click', '#tablaEleccionR .selected', function () {
        var idEleccionR = $(this).find('td:eq(0)').text();
        pidEleccionR = idEleccionR;
        PNombreEleccionR = $(this).find('td:eq(1)').text();

        if (pidEleccionR == 'Ningún dato disponible en esta tabla') {
            pidEleccionR = '';
            PNombreEleccionR = '';
            return;
        }
        pidPapeletaC = '';
        $("#tituloPapeletaR").text("Papeletas para la ronda " + pidEleccionR + " - " + PNombreEleccionR);

        CargarTablaEleccionesPR(pidEleccion, pidEleccionR);
    });
    //fin eventos para la tabla elecciones ronda

    //eventos para la tabla papeletas ronda
    $(document).on('click', '#btnAgregarPR', function () {

        if ((pidEleccion == '' || pidEleccion == null || pidEleccion == undefined) || (pidEleccionR == '' || pidEleccionR == null || pidEleccionR == undefined)) {
            alertaError("Nueva papeleta", "Debe seleccionar una elección y una ronda para poder crear una papeleta.");
            return;
        }
        $('#modal-lg-papeletaR').data('tipo', 'guardar');
        $('#modal-lg-papeletaR').modal('show');
        $("#lblpapeletaR").text('Nueva Papeleta');
        $("#selPapeleta").val(1);
        $("#descripcionPR").val('');
        $("#numVotos").val(0);
        $("#orden").val(0);
        $('#chkactivoPR').prop('checked', true).change();
    });

    $(document).on('click', '#tablaEleccionPR .tooltipImgM', function () {
        var id = $(this).closest("tr").find('td:eq(0)').text();
        var descripcion = $(this).closest("tr").find('td:eq(2)').text();
        var numvotos = $(this).closest("tr").find('td:eq(3)').text();
        var orden = $(this).closest("tr").find('td:eq(4)').text();
        var estado = $(this).closest("tr").find('td:eq(5)').text();

        //Estas dos variables indican si se debe seleccionar el modulo de acuerdo a la fila seleccionada
        $('#modal-lg-papeletaR').data('tipo', 'modificar');
        $('#modal-lg-papeletaR').modal('show');
        $("#lblpapeletaR").text('Modificar Papeleta');
        $("#selPapeleta").val(id);
        $("#descripcionPR").val(descripcion);
        $("#numVotos").val(numvotos);
        $("#orden").val(orden);
        estado == 'Activo' ? $('#chkactivoPR').prop('checked', true).change() : $('#chkactivoPR').prop('checked', false).change();
    });

    $(document).on('click', '#tablaEleccionPR .tooltipImgB', function () {
        var id = $(this).closest("tr").find('td:eq(0)').text();
        Swal.close();
        Swal.fire({
            title: 'Eliminar Datos',
            text: '¿Desea Eliminar la papeleta de la ronda?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                EliminarEleccionPR(id);
            }
        });
    });

    $(document).on('click', '#tablaEleccionPR .tooltipImgR', function () {
        var id = $(this).closest("tr").find('td:eq(0)').text();
        Swal.fire({
            title: 'Regenerar posiciones',
            text: '¿Desea generar las posiciones de los candidatos en la papeleta?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                GenerarPosiciones(pidEleccion, pidEleccionR, id);
            }
        });
    });

    $(document).on('click', '#btnGuardarPR', function () {
        if (validarDatosEleccionPR() == 1) {
            return;
        }
        Swal.close();
        Swal.fire({
            title: 'Guardar Datos',
            text: '¿Desea guardar los datos?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                $('#modal-lg-papeletaR').modal('hide');
                GuardarModificarEleccionPR($('#modal-lg-papeletaR').data('tipo'));
            }
        });
    });
    //fin eventos para la tabla papeletas ronda

    //Eventos para la tabla papeleta candidato
    $(document).on('click', '#tablaEleccionPR .tooltipImgC', function () {
        pidPapeletaC = $(this).closest("tr").find('td:eq(0)').text();
        var descripcion = $(this).closest("tr").find('td:eq(1)').text();
        $("#chkTipo").prop("checked", true);
        $('.toggle').removeClass('btn-danger');
        $('.toggle').removeClass('off');
        $('.toggle').addClass('btn-success');
        //Estas dos variables indican si se debe seleccionar el modulo de acuerdo a la fila seleccionada
        $('#modal-lg-papeletaC').data('tipo', 'modificar');
        $('#modal-lg-papeletaC').modal('show');
        $("#lblpapeletaC").text('Candidatos papeleta ' + descripcion);
        CargarTablaCandidatos(pidEleccion, pidEleccionR, pidPapeletaC);
    });

    $(document).on('click', '#tablaPapeletaC input[type=checkbox]', function () {

        var idCandidato = $(this).closest("tr").find('td:eq(1)').text();
        var cedula = $(this).closest("tr").find('td:eq(2)').text();

        if ($(this).is(":checked")) {
            //$(this).closest('tr').addClass('selected');
            GuardarCandidato(idCandidato, cedula, $(this).closest("tr"), $(this));
        } else {
            //$(this).closest('tr').removeClass('selected');
            EliminarCandidato(idCandidato, cedula, $(this).closest("tr"), $(this));
        }
    });
    //FIN Eventos para la tabla papeleta candidato
});

//cargar combo de papeletas
function obtenerPapeletas() {
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaRonda/ListadoPapeletas',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga papeletas", mensajeError);
        },
        success: function (data) {
            CargarComboPapeletas(data);
        }
    });
}

function CargarComboPapeletas(array) {
    $('#selPapeleta').empty();
    array.map(function (element) {
        $('#selPapeleta').append('<option value="' + element.Id_papeleta + '">' + element.Nombre + '</option>');
    });
}
//FIN cargar combo de papeletas

//FUNCIONES PARA CARGAR EL GRID DE ELECCIONES
function CargarTablaElecciones() {
    CargarFilasR();
    CargarFilasPR();
    pidEleccion = '';
    pNombreEleccion = '';
    pidEleccionR = '';
    PNombreEleccionR = '';
    pidPapeletaC = '';
    $("#tituloRondaE").text("Rondas por Elecciones");
    $("#tituloPapeletaR").text("Papeletas por ronda");

    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Eleccion/ListadoElecciones',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Listado elecciones", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            CargarFilas(data);
            CerrarCargando();
        }
    });
}

function CargarFilas(array) {
    var filas = [];
    array.map(function (element) {
        filas.push([
            element.Id_eleccion,
            element.Nombre,
            element.Descripcion,
            element.F_inicio,
            element.F_final,
            element.I_estado == 'A' ? 'Activo' : 'Inactivo',
            element.I_cierre_quorum == 'S' ? 'Si' : 'No',
            '<img class="tooltipImgM" src="images/Iconos/update.png" style="cursor:pointer" data-toggle="tooltip" title="Modificar" />',
            '<img class="tooltipImgB" src="images/Iconos/delete.png" style="cursor:pointer" data-toggle="tooltip" title="Eliminar" />'
        ]);
    });
    tablaE = objtablaE.DataTable({
        data: filas,
        "columns": [
            null,
           null,
            null,
             null,
            null,
            null,
            null,
            { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" }
        ],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "pageLength": 20,
        "autoWidth": false,
        //"responsive": true,
        "destroy": true,
        "deferRender": true,
        "scrollCollapse": true,
        "scroller": true
    });
    tablaE.draw();

    $('#tablaEleccion tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            // $(this).removeClass('selected');
        }
        else {
            tablaE.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}

function GuardarEleccion() {
    MostrarCargando();
    DatosE.Nombre = $("#nombre").val();
    DatosE.Descripcion = $("#descripcion").val();
    DatosE.F_inicio = $('#dtpRango').data('daterangepicker').startDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosE.F_final = $('#dtpRango').data('daterangepicker').endDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosE.Usuario = sessionStorage['Cedula'];
    DatosE.I_estado = $('#chkactivo').is(':checked') ? 'A' : 'I';
    DatosE.I_cierre_quorum = $('#chkquorum').is(':checked') ? 'S' : 'N';
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Eleccion/InsertarEleccion',
        data: JSON.stringify(DatosE),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Guardar datos', 'Datos Guardados correctamente');
                CargarTablaElecciones();
                CerrarCargando();
            } else {
                alertaError("Guardar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function ModificarEleccion() {


    MostrarCargando();
    DatosE.Id_eleccion = $("#id").val();
    DatosE.Nombre = $("#nombre").val();
    DatosE.Descripcion = $("#descripcion").val();
    DatosE.F_inicio = $('#dtpRango').data('daterangepicker').startDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosE.F_final = $('#dtpRango').data('daterangepicker').endDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosE.Usuario = sessionStorage['Cedula'];
    DatosE.I_estado = $('#chkactivo').is(':checked') ? 'A' : 'I';
    DatosE.I_cierre_quorum = $('#chkquorum').is(':checked') ? 'S' : 'N';
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Eleccion/ActualizarEleccion',
        data: JSON.stringify(DatosE),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Guardar datos', 'Datos Modificados correctamente');
                CargarTablaElecciones();
                CerrarCargando();
            } else {
                alertaError("Guardar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function EliminarEleccion(id) {
    MostrarCargando();
    DatosE.Id_eleccion = id;
    DatosE.Nombre = '';
    DatosE.Descripcion = '';
    DatosE.F_inicio = '';
    DatosE.F_final = '';
    DatosE.User_modifica = '';
    DatosE.I_estado = '';
    DatosE.I_cierre_quorum = '';
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Eleccion/EliminarEleccion',
        data: JSON.stringify(DatosE),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Eliminar datos', 'Datos Eliminar correctamente');
                CargarTablaElecciones();
                CerrarCargando();
            } else {
                alertaError("Eliminar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function validarDatosEleccion() {

    if ($.trim($("#nombre").val()).length < 5) {
        alertaError('Datos Elección', 'El nombre debe ser mayor a 5 caracteres');
        return 1;
    }

    if ($.trim($("#descripcion").val()).length < 10) {
        alertaError('Datos Elección', 'La descripción debe ser mayor a 9 caracteres');
        return 1;
    }

    if ($("#dtpRango").val().length < 1) {
        alertaError('Datos Elección', 'Debe digitar un rango de fechas');
        return 1;
    }
    return 0;
}

function GuardarModificarEleccion(tipo) {
    if (tipo == 'guardar') {
        GuardarEleccion();
    } else {
        ModificarEleccion();
    }
}
//FIN FUNCIONES PARA CARGAR EL GRID DE ELECCIONES

//FUNCIONES PARA CARGAR EL GRID DE RONDAS POR ELECCIONES
function CargarTablaEleccionesR(ideleccion) {
    CargarFilasPR();
    DatosER.Id_eleccion = ideleccion;
    pidEleccionR = '';
    PNombreEleccionR = '';
    pidPapeletaC = '';
    $("#tituloPapeletaR").text("Papeletas por ronda");

    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/EleccionRonda/ListadoEleccionesR',
        data: JSON.stringify(DatosER),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Listado elecciones", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            CargarFilasR(data);
            CerrarCargando();
        }
    });
}

function CargarFilasR(array) {
    var filas = [];
    if (array != null && array != undefined) {
        array.map(function (element) {
            filas.push([
                element.Id_ronda,
                element.Nombre,
                element.Descripcion,
                element.F_inicio,
                element.F_final,
                element.I_estado == 'A' ? 'Activo' : 'Inactivo',
                '<img class="tooltipImgM" src="images/Iconos/update.png" style="cursor:pointer" data-toggle="tooltip" title="Modificar" />',
                '<img class="tooltipImgB" src="images/Iconos/delete.png" style="cursor:pointer" data-toggle="tooltip" title="Eliminar" />'
            ]);
        });
    }
    tablaER = objtablaER.DataTable({
        data: filas,
        "columns": [
            null,
           null,
            null,
             null,
            null,
            null,
            { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" }
        ],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "pageLength": 20,
        "autoWidth": false,
        //"responsive": true,
        "destroy": true,
        "deferRender": true,
        "scrollCollapse": true,
        "scroller": true
    });
    tablaER.draw();

    $('#tablaEleccionR tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            // $(this).removeClass('selected');
        }
        else {
            tablaER.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}

function GuardarEleccionR() {
    MostrarCargando();
    DatosER.Id_eleccion = pidEleccion;
    DatosER.Nombre = $("#nombreR").val();
    DatosER.Descripcion = $("#descripcionR").val();
    DatosER.F_inicio = $('#dtpRangoR').data('daterangepicker').startDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosER.F_final = $('#dtpRangoR').data('daterangepicker').endDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosER.Usuario = sessionStorage['Cedula'];
    DatosER.I_estado = $('#chkactivoR').is(':checked') ? 'A' : 'I';
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/EleccionRonda/InsertarEleccionR',
        data: JSON.stringify(DatosER),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos ronda", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Guardar datos ronda', 'Datos Guardados correctamente');
                CargarTablaEleccionesR(pidEleccion);
                CerrarCargando();
            } else {
                alertaError("Guardar datos ronda", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function ModificarEleccionR() {

    MostrarCargando();
    DatosER.Id_eleccion = pidEleccion;
    DatosER.Id_ronda = $("#idR").val();
    DatosER.Nombre = $("#nombreR").val();
    DatosER.Descripcion = $("#descripcionR").val();
    DatosER.F_inicio = $('#dtpRangoR').data('daterangepicker').startDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosER.F_final = $('#dtpRangoR').data('daterangepicker').endDate.format('DD/MM/YYYY hh:mm A').toString();
    DatosER.Usuario = sessionStorage['Cedula'];
    DatosER.I_estado = $('#chkactivoR').is(':checked') ? 'A' : 'I';

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/EleccionRonda/ActualizarEleccionR',
        data: JSON.stringify(DatosER),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Guardar datos ronda', 'Datos Modificados correctamente');
                CargarTablaEleccionesR(pidEleccion);
                CerrarCargando();
            } else {
                alertaError("Guardar datos ronda", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function EliminarEleccionR(id) {
    MostrarCargando();
    DatosER.Id_eleccion = pidEleccion;
    DatosER.Id_ronda = id;
    DatosER.Nombre = '';
    DatosER.Descripcion = '';
    DatosER.F_inicio = '';
    DatosER.F_final = '';
    DatosER.User_modifica = '';
    DatosER.I_estado = '';
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/EleccionRonda/EliminarEleccionR',
        data: JSON.stringify(DatosER),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Eliminar datos ronda", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Eliminar datos ronda', 'Datos Eliminar correctamente');
                CargarTablaEleccionesR(pidEleccion);
                CerrarCargando();
            } else {
                alertaError("Eliminar datos ronda", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function validarDatosEleccionR() {

    if ($.trim($("#nombreR").val()).length < 5) {
        alertaError('Datos ronda elección', 'El nombre debe ser mayor a 5 caracteres');
        return 1;
    }

    if ($.trim($("#descripcionR").val()).length < 10) {
        alertaError('Datos ronda elección', 'La descripción debe ser mayor a 9 caracteres');
        return 1;
    }

    if ($("#dtpRangoR").val().length < 1) {
        alertaError('Datos ronda elección', 'Debe digitar un rango de fechas');
        return 1;
    }
    return 0;
}

function GuardarModificarEleccionR(tipo) {
    if (tipo == 'guardar') {
        GuardarEleccionR();
    } else {
        ModificarEleccionR();
    }
}
//FIN FUNCIONES  RONDAS POR ELECCIONES

//FUNCIONES PARA PAPELETAS POR RONDA
function CargarTablaEleccionesPR(ideleccion, ideleccionR) {
    DatosPR.Id_eleccion = ideleccion;
    DatosPR.Id_ronda = ideleccionR;
    pidPapeletaC = '';
    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaRonda/ListadoPapeletasR',
        data: JSON.stringify(DatosPR),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Listado papeletas por ronda", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            CargarFilasPR(data);
            CerrarCargando();
        }
    });
}

function CargarFilasPR(array) {
    var filas = [];
    if (array != null && array != undefined) {
        array.map(function (element) {
            filas.push([
                element.Id_papeleta,
                element.Nombre,
                element.Descripcion,
                element.Num_votos,
                element.Orden,
                element.I_estado == 'A' ? 'Activo' : 'Inactivo',
                '<img class="tooltipImgR" src="images/Iconos/reset.png" style="cursor:pointer" data-toggle="tooltip" title="Regenerar posiciones" />',
                '<img class="tooltipImgC" src="images/Iconos/candidatos.png" style="cursor:pointer" data-toggle="tooltip" title="Candidatos" />',
                '<img class="tooltipImgM" src="images/Iconos/update.png" style="cursor:pointer" data-toggle="tooltip" title="Modificar" />',
                '<img class="tooltipImgB" src="images/Iconos/delete.png" style="cursor:pointer" data-toggle="tooltip" title="Eliminar" />'
            ]);
        });
    }
    tablaPR = objtablaPR.DataTable({
        data: filas,
        "columns": [
            null,
            null,
            null,
            null,
            null,
            null,
             { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" }
        ],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "pageLength": 20,
        "autoWidth": false,
        "destroy": true,
        "deferRender": true,
        "scrollCollapse": true,
        "scroller": true
    });
    tablaER.draw();

    $('#tablaEleccionPR tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            // $(this).removeClass('selected');
        }
        else {
            tablaPR.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}

function GuardarEleccionPR() {
    MostrarCargando();
    DatosPR.Id_eleccion = pidEleccion;
    DatosPR.Id_ronda = pidEleccionR;
    DatosPR.Id_papeleta = $("#selPapeleta").val();
    DatosPR.Descripcion = $("#descripcionPR").val();
    DatosPR.Num_votos = $('#numVotos').val();
    DatosPR.Orden = $('#orden').val();
    DatosPR.Usuario = sessionStorage['Cedula'];
    DatosPR.I_estado = $('#chkactivoPR').is(':checked') ? 'A' : 'I';

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaRonda/InsertarPapeletaR',
        data: JSON.stringify(DatosPR),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos papeletas ronda", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Guardar datos papeletas ronda', 'Datos Guardados correctamente');
                CargarTablaEleccionesPR(pidEleccion, pidEleccionR);
                CerrarCargando();
            } else {
                alertaError("Guardar datos papeletas ronda", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function ModificarEleccionPR() {

    MostrarCargando();
    DatosPR.Id_eleccion = pidEleccion;
    DatosPR.Id_ronda = pidEleccionR;
    DatosPR.Id_papeleta = $("#selPapeleta").val();
    DatosPR.Descripcion = $("#descripcionPR").val();
    DatosPR.Num_votos = $('#numVotos').val();
    DatosPR.Orden = $('#orden').val();
    DatosPR.Usuario = sessionStorage['Cedula'];
    DatosPR.I_estado = $('#chkactivoPR').is(':checked') ? 'A' : 'I';

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaRonda/ActualizarPapeletaR',
        data: JSON.stringify(DatosPR),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos ronda papeleta", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Guardar datos ronda papeleta', 'Datos Modificados correctamente');
                CargarTablaEleccionesPR(pidEleccion, pidEleccionR);
                CerrarCargando();
            } else {
                alertaError("Guardar datos ronda papeleta", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function EliminarEleccionPR(id) {
    MostrarCargando();
    DatosPR.Id_eleccion = pidEleccion;
    DatosPR.Id_ronda = pidEleccionR;
    DatosPR.Id_papeleta = id;
    DatosPR.Descripcion = '';
    DatosPR.Num_votos = 0;
    DatosPR.Orden = 0;
    DatosPR.Usuario = '';
    DatosPR.I_estado = '';
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaRonda/EliminarPapeletaR',
        data: JSON.stringify(DatosPR),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Eliminar ronda papeleta", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Eliminar datos ronda papeleta', 'Datos Eliminados correctamente');
                CargarTablaEleccionesPR(pidEleccion, pidEleccionR);
                CerrarCargando();
            } else {
                alertaError("Eliminar datos ronda papeleta", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function validarDatosEleccionPR() {

    if ($.trim($("#descripcionPR").val()).length < 5) {
        alertaError('Datos papelta ronda', 'La descripción debe ser mayor a 9 caracteres');
        return 1;
    }

    if ($("#numVotos").val() <= 0) {
        alertaError('Datos papelta ronda', 'El número de votos debe ser mayor a 0.');
        return 1;
    }
    if ($("#orden").val() <= 0) {
        alertaError('Datos papelta ronda', 'El orden debe ser mayor a 0.');
        return 1;
    }
    return 0;
}

function GuardarModificarEleccionPR(tipo) {
    if (tipo == 'guardar') {
        GuardarEleccionPR();
    } else {
        ModificarEleccionPR();
    }
}
//FIN FUNCIONES PARA PAPELETAS POR RONDA

//FUNCIONES CANDIDATOS POR PAPELETAS
function CargarTablaCandidatos(ideleccion, ideleccionR,idpapeleta) {
    DatosPC.Id_eleccion = ideleccion;
    DatosPC.Id_ronda = ideleccionR;
    DatosPC.Id_papeleta = idpapeleta;
    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaCandidato/ListadoCandidatosS',
        data: JSON.stringify(DatosPC),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Listado candidatos por papeleta", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            CargarFilasCandidatos(data);
            CerrarCargando();
        }
    });
}

function CargarTablaCandidatosT(ideleccion, ideleccionR, idpapeleta) {
    DatosPC.Id_eleccion = ideleccion;
    DatosPC.Id_ronda = ideleccionR;
    DatosPC.Id_papeleta = idpapeleta;
    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaCandidato/ListadoCandidatos',
        data: JSON.stringify(DatosPC),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Listado candidatos por papeleta", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            CargarFilasCandidatos(data);
            CerrarCargando();
        }
    });
}

function CargarFilasCandidatos(array) {
    var filas = [];
    if (array != null && array != undefined) {
        array.map(function (element) {
            filas.push([
                (element.I_estado == null || element.I_estado == 'I') ? '<input type="checkbox" />' : '<input type="checkbox" checked />',
                element.Id_candidato,
                element.Cedula,
                element.Nombre
            ]);
        });
    }

    tablaPC = objtablaPC.DataTable({
        data: filas,
        "columns": [
            { "width": "10%", "sortable": false, "className": "textoCentrado" },
            null,
            null,
            null
        ],
        "drawCallback": function( settings ) {
            $("#tablaPapeletaC input:checkbox:checked").map(function () {
                $(this).closest('tr').addClass('selected');
            });
        },
        "order": [],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "pageLength": 10,
        "autoWidth": false,
        //"responsive": true,
        "destroy": true,
        "deferRender": true,
        "scrollCollapse": true,
        "scroller": true
    });
    tablaPC.draw();
}

function GuardarCandidato(idCandidato, cedula, tr, chk) {

    MostrarCargando();
    DatosPC.Id_eleccion = pidEleccion;
    DatosPC.Id_ronda = pidEleccionR;
    DatosPC.Id_papeleta = pidPapeletaC;
    DatosPC.Id_candidato = idCandidato;
    DatosPC.Descripcion = cedula;
    DatosPC.Num_posicion = null;
    DatosPC.I_estado = 'A';
    DatosPC.Usuario = sessionStorage['Cedula'];

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaCandidato/InsertarPapeletaC',
        data: JSON.stringify(DatosPC),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            chk.prop("checked", false);
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos candidato", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                tr.addClass('selected');
                CerrarCargando();
            } else {
                chk.prop("checked", false);
                alertaError("Guardar datos papeletas ronda", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function EliminarCandidato(idCandidato, cedula, tr,chk) {

    MostrarCargando();
    DatosPC.Id_eleccion = pidEleccion;
    DatosPC.Id_ronda = pidEleccionR;
    DatosPC.Id_papeleta = pidPapeletaC;
    DatosPC.Id_candidato = idCandidato;
    DatosPC.Descripcion = cedula;
    DatosPC.Num_posicion = null;
    DatosPC.I_estado = 'A';
    DatosPC.Usuario = sessionStorage['Cedula'];

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaCandidato/EliminarPapeletaC',
        data: JSON.stringify(DatosPC),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Guardar datos candidato", mensajeError);
            CerrarCargando();
            chk.prop("checked", true);
        },
        success: function (data) {
            if (data.Error == "0") {
                tr.removeClass('selected');
                CerrarCargando();
            } else {
                alertaError("Guardar datos papeletas ronda", data.Mensaje);
                chk.prop("checked", true);
                CerrarCargando();
            }
        }
    });
}

function GenerarPosiciones(ipidEleccion, pidEleccionR, pidPapeletaC) {

    MostrarCargando();
    DatosPC.Id_eleccion = pidEleccion;
    DatosPC.Id_ronda = pidEleccionR;
    DatosPC.Id_papeleta = pidPapeletaC;
    

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/PapeletaCandidato/ActualizarPapeletaPos',
        data: JSON.stringify(DatosPC),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Asignar posiciones", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error == "0") {
                alertaExito('Asignar posiciones', 'Posiciones asignadas correctamente');
                CerrarCargando();
            } else {
                alertaError("Asignar posiciones", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

//FUNCIONES CANDIDATOS POR PAPELETAS




