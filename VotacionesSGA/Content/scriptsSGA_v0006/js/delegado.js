var apiUrl = sessionStorage['Uri'];
var objtabla = $('#tablaDelegados');
var tabla;
var nombreReporte = 'Lista de Delegados';
var DatosD = {
    "Id_delegado": '',
    "Cedula": '',
    "Num_paleta": 0,
    "Nombre": '',
    "Institucion": '',
    "Centro": '',
    "Lugar_Trabajo": '',
    "Tel_Celular": '',
    "Email": '',
    "Foto": '',
    "I_estado": '',
    "Usuario": '',
    "Terminal": '',
    "I_votar": ''
};

$(document).ready(function () {

    setTimeout(function () {
        CargarTablaDelegados();
    }, 500);

    $(document).on('click', '#btnCargar', function () {
        $('#Miarchivo').click();
    });

    $("#Miarchivo").change(function () {
        MostrarCargando();
        var file = $('#Miarchivo')[0].files[0];
        var ext = file.name.split('.').pop();
        ext = ext.toLowerCase();
        if (file) {

            if (ext !== "txt") {
                alertaError("Cargar archivo", 'Solo se permiten archivos .txt');
                CerrarCargando();
                return;
            }

            if (file.size > 5242880) {
                alertaError("Cargar archivo", 'El tamaño del archivo no debe sobrepasar los 5 MB');
                CerrarCargando();
                return;
            }
            data = new FormData();
            data.append('file', file);
            $.ajax({
                url: apiUrl + '/api/login/Delegado/CargarArchivo',
                type: 'POST',
                headers: {
                    'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
                },
                data: data,
                cache: false,
                processData: false,
                contentType: false,
                success: function (data) {
                    CerrarCargando();
                    alertaInfo('Guardar datos', data);
                    CargarTablaDelegados();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
                    alertaError("cargar archivo", mensajeError);
                    CerrarCargando();
                }
            });


        }
    });


    $(document).on('click', '#btnAgregar', function () {
        $('#modal-lg-Delegado').data('tipo', 'guardar');
        $('#modal-lg-Delegado').modal('show');
        $("#lblDelegado").text('NUEVO DELEGADO');
        $("#id").val('');
        $("#cedula").val('');
        $("#nombre").val('');
        $("#numPaleta").val('');
        $("#centro").val('');
        $("#institucion").val('');
        $("#lugar_trabajo").val('');
        $("#celular").val('');
        $("#email").val('');
        $("#foto").val('');
        $('#chkvotar').prop('checked', true).change();
        $('#chkactivo').prop('checked', true).change();
    });


    $(document).on('click', '.tooltipImgM', function () {

        var id = $(this).closest("tr").find('td:eq(0)').text();
        var cedula = $(this).closest("tr").find('td:eq(1)').text();
        var nombre = $(this).closest("tr").find('td:eq(2)').text();
        var paleta = $(this).closest("tr").find('td:eq(3)').text();
        var centro = $(this).closest("tr").find('td:eq(4)').text();
        var institucion = $(this).closest("tr").find('td:eq(5)').text();
        var lugar = $(this).closest("tr").find('td:eq(6)').text();
        var celular = $(this).closest("tr").find('td:eq(7)').text();
        var email = $(this).closest("tr").find('td:eq(8)').text();
        var foto = $(this).closest("tr").find('td:eq(9)').text();
        var votar = $(this).closest("tr").find('td:eq(10)').text();
        var estado = $(this).closest("tr").find('td:eq(11)').text();

        //Estas dos variables indican si se debe seleccionar el modulo de acuerdo a la fila seleccionada
        $('#modal-lg-Delegado').data('tipo', 'modificar');
        $('#modal-lg-Delegado').modal('show');
        $("#lblDelegado").text('MODIFICAR USUARIO');
        $("#id").val(id);
        $("#cedula").val(cedula);
        $("#nombre").val(nombre);
        $("#numPaleta").val(paleta);
        $("#centro").val(centro);
        $("#institucion").val(institucion);
        $("#lugar_trabajo").val(lugar);
        $("#celular").val(celular);
        $("#email").val(email);
        $("#foto").val(foto);
        votar === 'SI' ? $('#chkvotar').prop('checked', true).change() : $('#chkvotar').prop('checked', false).change();
        estado === 'ACTIVO' ? $('#chkactivo').prop('checked', true).change() : $('#chkactivo').prop('checked', false).change();
    });

    $(document).on('click', '.tooltipImgB', function () {
        //*** Usuario a Borrar
        var id = $(this).closest("tr").find('td:eq(0)').text();
        Swal.close();
        Swal.fire({
            title: 'Eliminar Datos',
            html: '¿Desea Eliminar el delegado <b>' + id + '</b>?',
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

        if (validarDatosDelegados() === 1) {
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
                $('#modal-lg-Delegado').modal('hide');
                GuardarModificarDelegado($('#modal-lg-Delegado').data('tipo'));

            }
        });
    });


    $(document).on('click', '#btnTrasladar', function () {

        
        Swal.close();
        Swal.fire({
            title: 'Guardar Datos',
            text: '¿Desea generar el proceso de creación de usuarios?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                TrasladarUsuarios();
            }
        });
    });

});
function CargarTablaDelegados() {
    MostrarCargando();

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Delegado/ListaDelegados',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Seleccionar Delegados", mensajeError);
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
            element.Id_delegado,
            element.Cedula,
            element.Nombre,
            element.Num_paleta,
            element.Centro,
            element.Institucion,
            element.Lugar_Trabajo,
            element.Tel_Celular,
            element.Email,
            element.Foto,
            spanVotar(element.I_votar),
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
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                },
                title: function () {
                    return nombreReporte;
                },
                sheetName: 'Reporte',
                className: 'btn-success ml-2'
            }]
        ,
        "columns": [
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            {  "className": "textoCentrado" },
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
        "scrollY": $(document).height() - 415 + "px", //30vh
        "scrollX": true
    });
    tabla.draw();

    $('.btn-group').width('35%');
    var btnTrasladar = '<button type="button" class="btn btn-block bg-gradient-primary" id="btnTrasladar"style="margin-top: 0rem;">TRASLADAR USUARIOS</button>';
    var btnAgregar =   '<button type="button" class="btn btn-block bg-gradient-primary" id="btnAgregar" style="margin-right:8px;">NUEVO DELEGADO</button>';
    $('.buttons-excel').before(btnAgregar);
    $('.buttons-excel').before(btnTrasladar);
    


    $('#tablaDelegados tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            // $(this).removeClass('selected');
        }
        else {
            tabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}
//*** Función Condicional de Estado
function spanEstado(valor) {
    var valorAux = valor === 'A' ? 'ACTIVO' : 'INACTIVO';
    switch (valorAux) {
        case 'ACTIVO':
            return '<small class="badge badge-success">' + valorAux + '</small>';
        case 'INACTIVO':
            return '<small class="badge badge-danger">' + valorAux + '</small>';
        default:
            return '<small class="badge badge-info">' + valorAux + '</small>';
    }
}
//*** Función Condicional de Votar
function spanVotar(valor) {
    var valorAux = valor === 'S' ? 'SI' : 'NO';
    switch (valorAux) {
        case 'SI':
            return '<small class="badge badge-success">' + valorAux + '</small>';
        case 'NO':
            return '<small class="badge badge-danger">' + valorAux + '</small>';
        default:
            return '<small class="badge badge-info">' + valorAux + '</small>';
    }
}
function validarDatosDelegados() {

    if ($("#cedula").val().length < 7) {
        alertaError('Datos Delegados', 'La cédula debe ser mayor a 7 caracteres');
        return 1;
    }
    if ($.trim($("#nombre").val()).length < 5) {
        alertaError('Datos Delegados', 'El nombre debe ser mayor a 5 caracteres');
        return 1;
    }
    if ($.trim($("#numPaleta").val()).length <= 0 || $("#numPaleta").val() === '0') {
        alertaError('Datos Delegados', 'Debe indicar un número de paleta');
        return 1;
    }
    if ($.trim($("#centro").val()).length < 3) {
        alertaError('Datos Delegados', 'Debe indicar un centro de trabajo mayor a 3 caracteres');
        return 1;
    }
    if ($.trim($("#institucion").val()).length < 3) {
        alertaError('Datos Delegados', 'Debe indicar una institución de trabajo mayor a 3 caracteres');
        return 1;
    }
    if ($.trim($("#lugar_trabajo").val()).length < 3) {
        alertaError('Datos Delegados', 'Debe indicar un lugar de trabajo mayor a 3 caracteres');
        return 1;
    }
    if ($.trim($("#celular").val()).length < 8) {
        alertaError('Datos Delegados', 'Debe indicar un número de celular valido (8)');
        return 1;
    }
    if (!validateEmail($.trim($("#email").val()))) {
        alertaError('Datos Delegados', 'Debe indicar un email valido');
        return 1;
    }


    
    return 0;
}
function GuardarDatos() {
    MostrarCargando();
    DatosD.Cedula = $("#cedula").val();
    DatosD.Nombre = $("#nombre").val();
    DatosD.Num_paleta = $("#numPaleta").val();
    DatosD.Centro = $("#centro").val();
    DatosD.Institucion = $("#institucion").val();
    DatosD.Lugar_Trabajo = $("#lugar_trabajo").val();
    DatosD.Tel_Celular = $("#celular").val();
    DatosD.Email = $("#email").val();
    DatosD.Foto = $("#foto").val();
    DatosD.I_votar = $('#chkvotar').is(':checked') ? 'S' : 'N';
    DatosD.I_estado = $('#chkactivo').is(':checked') ? 'A' : 'I';
    DatosD.Usuario = sessionStorage['Cedula'];

    

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Delegado/InsertarDelegado',
        data: JSON.stringify(DatosD),
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
            if (data.Error === 0) {
                alertaExito('Guardar datos', 'Datos Guardardos correctamente');
                CargarTablaDelegados();
                CerrarCargando();
            } else if (data.Error === 1) {
                alertaInfo('Guardar datos', data.Mensaje);
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
    DatosD.Id_delegado = $("#id").val();
    DatosD.Cedula = $("#cedula").val();
    DatosD.Nombre = $("#nombre").val();
    DatosD.Num_paleta =  $("#numPaleta").val();
    DatosD.Centro = $("#centro").val();
    DatosD.Institucion = $("#institucion").val();
    DatosD.Lugar_Trabajo = $("#lugar_trabajo").val();
    DatosD.Tel_Celular= $("#celular").val();
    DatosD.Email = $("#email").val();
    DatosD.Foto = $("#foto").val();
    DatosD.I_votar = $('#chkvotar').is(':checked') ? 'S' : 'N';
    DatosD.I_estado = $('#chkactivo').is(':checked') ? 'A' : 'I';
    DatosD.Usuario = sessionStorage['Cedula'];


    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Delegado/ActualizarDelegado',
        data: JSON.stringify(DatosD),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Modificar datos", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExito('Modificar datos', 'Datos Modificados correctamente');
                CargarTablaDelegados();
                CerrarCargando();
            } else if (data.Error === 1) {
                alertaInfo('Modificar datos', data.Mensaje);
                CerrarCargando();
            } else {
                alertaError("Modificar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}
function GuardarModificarDelegado(tipo) {
    if (tipo === 'guardar') {
        GuardarDatos();
    } else {
        ModificarDatos();
    }
}
function TrasladarUsuarios() {
    MostrarCargando();
    DatosD.Usuario = sessionStorage['Cedula'];

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Delegado/TrasladarUsuarios',
        data: JSON.stringify(DatosD),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Generación de usuarios", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExito('Modificar datos', data.Mensaje);
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
    DatosD.Id_delegado = id;
    DatosD.Usuario = sessionStorage['Cedula'];
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Delegado/EliminarDelegado',
        data: JSON.stringify(DatosD),
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
            if (data.Error === 0) {
                alertaExito('Eliminar datos', 'Datos Eliminados correctamente');
                CargarTablaDelegados();
                CerrarCargando();
            } else if (data.Error === 1) {
                alertaInfo('Eliminar datos', data.Mensaje);
                CerrarCargando();
            } else {
                alertaError("Eliminar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}