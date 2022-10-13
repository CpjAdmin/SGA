var apiUrl = sessionStorage['Uri'];
var objtabla = $('#tablaDispositivos');
var tabla;

$(document).ready(function () {
    CargarTablaDispositivos();

    $(document).on('click', '#btnAgregar', function () {
        $('#modal-lg-dispositivo').data('tipo', 'guardar');
        $('#modal-lg-dispositivo').modal('show');
        $("#lblDispositivo").text('NUEVO DISPOSITIVO');
        $("#idR").removeAttr('disabled');
        $("#idD").val('0');
        $("#nombreD").val('');
        $("#descripcionD").val('');
        $("#telefonoD").val('');
        $('#chkactivo').prop('checked', true).change();
    });

    $(document).on('click', '.tooltipImgM', function () {
        //*** Datos a Modificar
        var id = $(this).closest("tr").find('td:eq(0)').text();
        var nombre = $(this).closest("tr").find('td:eq(1)').text();
        var descripcion = $(this).closest("tr").find('td:eq(2)').text();
        var telefono = $(this).closest("tr").find('td:eq(3)').text();
        var estado = $(this).closest("tr").find('td:eq(4)').text();


        //Estas dos variables indican si se debe seleccionar el modulo de acuerdo a la fila seleccionada
        $('#modal-lg-dispositivo').data('tipo', 'modificar');
        $('#modal-lg-dispositivo').modal('show');
        $("#lblDispositivo").text('MODIFICAR DISPOSITIVO');

        $("#idD").prop('disabled', true);
        $("#idD").val(id);
        $("#nombreD").val(nombre);
        $("#descripcionD").val(descripcion);
        $("#telefonoD").val(telefono);
        estado === 'Activo' ? $('#chkactivoD').prop('checked', true).change() : $('#chkactivoD').prop('checked', false).change();
    });

    $(document).on('click', '.tooltipImgB', function () {
        //*** Usuario a Borrar
        var id = $(this).closest("tr").find('td:eq(0)').text();
        Swal.close();
        Swal.fire({
            title: 'Eliminar Datos',
            html: '¿Desea Eliminar el dispositivo <b>' + id + '</b>?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                EliminarDatos(id);
            }
        });
    });


    $(document).on('click', '#btnGuardar', function () {

        if (validarDatosPantalla() === 1) {
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
                $('#modal-lg-dispositivo').modal('hide');
                GuardarModificar($('#modal-lg-dispositivo').data('tipo'));

            }
        });
    });

});
function CargarTablaDispositivos() {
    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Dispositivo/ListaDispositivos',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Seleccionar Usuarios", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            CargarFilas(data);
            CerrarCargando();
        }
    });
}
function spanEstado(estado) {

    if (estado === "A") {
        return '<small class="badge badge-success">Activo</small>';
    }
    else {
        return '<small class="badge badge-danger">Inactivo</small>';
    }
}
function CargarFilas(array) {
    var filas = [];
    array.map(function (element) {
        filas.push([
            element.Id_dispositivo,
            element.Nombre,
            element.Descripcion,
            element.Telefono,
            spanEstado(element.I_estado),
            '<img class="tooltipImgM" src="images/Iconos/update.png" style="cursor:pointer" data-toggle="tooltip" title="Modificar" />',
            '<img class="tooltipImgB" src="images/Iconos/delete.png" style="cursor:pointer" data-toggle="tooltip" title="Eliminar" />'
        ]);
    });

    tabla = objtabla.DataTable({
        data: filas,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "EXCEL",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                },
                title: function () {
                    return 'Reporte_dispositivos';
                },
                sheetName: 'Reporte',
                className: 'btn-success'
            }]
        ,
        "columns": [
            { "width": "5%", "className": "textoCentrado" },
            null,
            null,
            null,
            { "className": "textoCentrado" },
           { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" }
        ],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "pageLength": 22,
        "autoWidth": false,
        "destroy": true,
        "deferRender": true,
        "scrollY": $(document).height() - 390 + "px" //30vh
    });
    tabla.draw();

    $('.btn-group').width('20%');
    var btnAgregar = '<button type="button" class="btn bg-gradient-primary" id="btnAgregar" style="margin-right:8px;">NUEVO DISPOSITIVO</button>';
    $('.buttons-excel').before(btnAgregar);


    $('#tablaDispositivos tbody').on('click', 'tr', function () {
        if (!$(this).hasClass('selected')) {
            tabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}
function GuardarDatos() {
    MostrarCargando();

    var parametros = new Object();

    parametros.Nombre = $("#nombreD").val();
    parametros.Descripcion = $("#descripcionD").val();
    parametros.Telefono = $("#telefonoD").val();
    parametros.I_estado = $('#chkactivoD').is(':checked') ? 'A' : 'I';
    parametros.User_creacion = sessionStorage['Cedula'];

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Dispositivo/InsertarDispositivo',
        data: JSON.stringify(parametros),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaErrorMin(mensajeError);
            CerrarCargando();
        },
        success: function (data) {     
            if (data.Error === 0) {
                alertaExito('Guardar datos', 'Datos Guardados correctamente');
                CargarTablaDispositivos();
                CerrarCargando();
            } else {
                alertaError("Guardar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}
function ModificarDatos() {
    MostrarCargando();

    var parametros = new Object();

    parametros.Id_dispositivo = $("#idD").val();
    parametros.Nombre = $("#nombreD").val();
    parametros.Descripcion = $("#descripcionD").val();
    parametros.Telefono = $("#telefonoD").val();
    parametros.I_estado = $('#chkactivoD').is(':checked') ? 'A' : 'I';
    parametros.User_creacion = sessionStorage['Cedula'];

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Dispositivo/ModificarDispositivo',
        data: JSON.stringify(parametros),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaErrorMin(mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExito('Modificar datos', 'Datos Modificados correctamente');
                CargarTablaDispositivos();
                CerrarCargando();
            } else {
                alertaError("Modificar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}
function EliminarDatos(id) {
    MostrarCargando();

    var parametros = new Object();

    parametros.Id_dispositivo = id;
    parametros.User_creacion = sessionStorage['Cedula'];

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Dispositivo/EliminarDispositivo',
        data: JSON.stringify(parametros),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaErrorMin(mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExito('Eliminar datos', 'Datos Eliminados correctamente');
                CargarTablaDispositivos();
                CerrarCargando();
            } else {
                alertaError("Eliminar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}
function validarDatosPantalla() {
    if ($("#nombreD").val().length < 5) {
        alertaError('Datos Dispositivo', 'El nombre debe ser mayor a 4 caracteres');
        return 1;
    }
    if ($.trim($("#telefonoD").val()).length < 8) {
        alertaError('Datos Dispositivo', 'La descripción abreviada de la pantalla debe ser de 8 caracteres');
        return 1;
    }
    return 0;
}
function GuardarModificar(tipo) {
    if (tipo === 'guardar') {
        GuardarDatos();
    } else {
        ModificarDatos();
    }
}