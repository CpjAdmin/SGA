//*** Permisos Módulo:  'C,U,D,EP,EM,CU'

var apiUrl = sessionStorage['Uri']; //Ya está en principal, BORRAR
var objtabla = $('#tablaUsuarios');
var tabla;
var nombreReporte = 'Lista de Usuarios';
var telefonoEnvioPin;

var DatosU = {
    "Id_usuario": '',
    "Id_delegado": '',
    "Login": '',
    "Clave": '',
    "Nombre": '',
    "F_ult_ingreso": '',
    "User_modifica": '',
    "Terminal": '',
    "Id_rol": '',
    "Nombre_rol": '',
    "Foto": '',
    "Token": '',
    "I_estado": '',
    "Telefono": '',
    "I_delegado": ''
};

var DatosSMS = {
    "Login": '',
    "Indgenera": '',
    "Usuario": '',
    "Tipo": '',
    "TelefonoAux": ''
};

//*** #1 - Inicio
$(document).ready(function () {

    //*** Validar Permisos de Página
    iniciarPagina();
});

//*** #2 - Función de Validación Inicial
function iniciarPagina() {

    //*** 2.1 - Cargar DataTable
    MostrarCargando();
    setTimeout(function () {
        IniciarDataTable(CargarTablaUsuarios);
    }, 800);

    //*** 2.2 - Cargar Roles y Dispositivos
    obtenerRoles();
    obtenerDispositivos();

    $('#frmDispositivos').hide();

    //Verificar Télefono ( Solo Números y de Máximo 8 Dígitos)
    $('input#telefonoE').bind('keyup paste', function (e) {

        this.value = this.value.replace(/[^0-9]/g, '');

    });

    //*** 2.3 - Botón Refrescar
    $(document).on('click', '#btnRefrescar', function () {
        IniciarDataTable(CargarTablaUsuarios);
    });
    //*** Carga Masiva de Usuarios
    $(document).on('click', '#btnCargar', function () {
        //*** Permiso - Carga Masiva de Usuarios
        if (ValidarPermisoCRUD('CU')) {

            $('#Miarchivo').click();

        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }
    });

    $("#cedula").focusout(function () {
        $("#idDelegado").val('');
        $('#chkDelegado').prop('checked', false).change();
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
                url: apiUrl + '/api/login/Usuario/CargarArchivo',
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
                    CargarTablaUsuarios();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
                    alertaError("cargar archivo", mensajeError);
                    CerrarCargando();
                }
            });

            $('#Miarchivo').val(null);
        }
    });

    $('#chkDelegado').change(function () {
        if ($(this).is(":checked")) {
            //CargarTablaCandidatos(pidEleccion, pidEleccionR, pidPapeletaC);

            validarDelegado($("#cedula").val());
        } else {
            $("#idDelegado").val('');
        }
    });
    //*** Enviar PIN Masivo
    $(document).on('click', '#btnMasivo', function () {
        //*** Permiso - Envío de PIN Masivo
        if (ValidarPermisoCRUD('EM')) {

            $('#modal-lg-masivo').modal('show');

        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }
    });

    $(document).on('click', '#btnAgregar', function () {
        //*** Permiso - Agregar Usuario
        if (ValidarPermisoCRUD('C')) {

            $('#modal-lg-usuario').data('tipo', 'guardar');
            $('#modal-lg-usuario').modal('show');
            $("#lblUsuario").text('NUEVO USUARIO');
            $("#cedula").removeAttr('disabled');
            $("#cedula").val('');
            $("#nombre").val('');
            $("#telefono").val('');
            $("#idDelegado").val('');
            $('#chkactivo').prop('checked', true).change();
            $('#chkDelegado').prop('checked', false).change();

        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }
    });

    //*** Editar Usuario
    $(document).on('click', '.tooltipImgM', function () {
        //*** Permiso - Editar Usuario
        if (ValidarPermisoCRUD('U')) {

            //*** Datos a Modificar
            var cedula = $(this).closest("tr").find('td:eq(1)').text();
            var nombre = $(this).closest("tr").find('td:eq(2)').text();
            var rolId = $(this).closest("tr").find('td:eq(3) p').attr('data-idR');
            var rol = $(this).closest("tr").find('td:eq(3)').text();
            var estado = $(this).closest("tr").find('td:eq(4)').text();
            var telefono = $(this).closest("tr").find('td:eq(5)').text();
            var delegado = $(this).closest("tr").find('td:eq(6)').text();
            var id_delegado = $(this).closest("tr").find('td:eq(7)').text();

            //*** Permiso - Roles de Usuario
            if (!ValidarPermisoCRUD('UR')) {
                // Inactivar Input
                $("#selRol").prop('disabled', true);
                // Cargar Input solo con el Rol del Usuario
                $('#selRol').empty();
                $('#selRol').append('<option value="' + rolId + '">' + rol + '</option>');
            }

            //Estas dos variables indican si se debe seleccionar el modulo de acuerdo a la fila seleccionada
            $('#modal-lg-usuario').data('tipo', 'modificar');
            $('#modal-lg-usuario').modal('show');
            $("#lblUsuario").text('MODIFICAR USUARIO');

            $("#cedula").prop('disabled', true);
            $("#cedula").val(cedula);
            $("#nombre").val(nombre);
            $("#telefono").val(telefono);
            $("#selRol").val(rolId).change();
            estado === 'Activo' ? $('#chkactivo').prop('checked', true).change() : $('#chkactivo').prop('checked', false).change();
            delegado === 'SI' ? $('#chkDelegado').prop('checked', true).change() : $('#chkDelegado').prop('checked', false).change();
            $("#idDelegado").val(id_delegado);
        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }

    });
    //*** Eliminar Usuario
    $(document).on('click', '.tooltipImgB', function () {
        //*** Permiso - Eliminar Usuario
        if (ValidarPermisoCRUD('D')) {

            //*** Usuario a Borrar
            var login = $(this).closest("tr").find('td:eq(1)').text();
            Swal.close();
            Swal.fire({
                title: 'Eliminar Datos',
                html: '¿Desea Eliminar el usuario <b>' + login + '</b>?',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: 'Cancelar',
                confirmButtonText: 'Aceptar'
            }).then(function (result) {
                if (result.value) {
                    EliminarDatos(login);
                }
            });

        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }
    });

    //*** Enviar PIN Individual
    $(document).on('click', '.tooltipImgE', function () {
        //*** Permiso - Envío de PIN Individual
        if (ValidarPermisoCRUD('EP')) {

            //*** Usuario envío de Token
            var cedula = $(this).closest("tr").find('td:eq(1)').text();
            var nombre = $(this).closest("tr").find('td:eq(2)').text();
            var telefono = $(this).closest("tr").find('td:eq(5)').text();

            //Muestra Modal de Envío PIN
            $('#modal-Envio').modal('show');

            //Define los valores de los campos del Modal Envío PIN
            if (!$('#ckTelefonoE').is(':checked')) {
                $("#ckTelefonoE").click();
            }

            $("#cedulaE").val(cedula);
            $("#nombreE").val(nombre);
            $("#telefonoE").val(telefono);
            $('#ckCambiaPin').prop('checked', false).change();
            $('#ckEnviaD').prop('checked', false).change();
            $('#frmDispositivos').hide();

        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }
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
                $('#modal-lg-usuario').modal('hide');
                GuardarModificarPantalla($('#modal-lg-usuario').data('tipo'));

            }
        });
    });

    //*** Check de Editar Teléfono de Envío PIN
    $(document).on('click', '#ckTelefonoE', function () {

        if ($('#ckTelefonoE').is(':checked')) {
            $("#telefonoE").attr('disabled', 'disabled');
        } else {
            $("#telefonoE").removeAttr('disabled');
        }
    });

    //Botón Enviar de Envío PIN
    $(document).on('click', '#btnEnviar', function () {

        //*** Si el télefono es menor a 8 Dífigos no puede seguir
        if ($("#telefonoE").val().length < 8) {

            if ($('#ckTelefonoE').is(':checked')) {
                $("#ckTelefonoE").click();
            }

            $("#telefonoE").focus();

        } else {

            //*** Tipo de Envío Individual PIN
            var tipo = $('#ckEnviaD').is(':checked') ? 'D' : 'C';

            //*** Teléfono donde se enviará el PIN
            telefonoEnvioPin = $("#telefonoE").val();

            Swal.close();
            Swal.fire({
                title: 'Guardar Datos',
                text: '¿Desea enviar el pin al usuario?',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: 'Cancelar',
                confirmButtonText: 'Aceptar'
            }).then(function (result) {
                if (result.value) {
                    $('#modal-Envio').modal('hide');
                    EnviarMensajeTexto(tipo);
                }
            });
        }
    });

    $(document).on('click', '#btnEnviarM', function () {

        Swal.close();
        Swal.fire({
            title: 'Guardar Datos',
            text: '¿Desea ejecutar el proceso masivo de envío masivo de pines de acceso?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {
            if (result.value) {
                $('#modal-lg-masivo').modal('hide');
                enviarSMSMasivo();

            }
        });
    });

    $('#ckEnviaD').change(function () {
        if (this.checked) {
            $('#frmDispositivos').show();
        } else {
            $('#frmDispositivos').hide();
        }

    });
}

function validarPermisos() {

    $('#content .fa-lock').remove();

    //*** Validar permisos y mostrar el Candado de Bloqueo si no los posee

    //*** Candado Blanco
    !ValidarPermisoCRUD('C') ? $("#btnAgregar").append('<span class="fa fa-lock pl-2"></span>') : '';
    !ValidarPermisoCRUD('EM') ? $("#btnMasivo").append('<span class="fa fa-lock pl-2"></span>') : '';
    !ValidarPermisoCRUD('CU') ? $("#btnCargar").after('<span class="fa fa-lock pl-2 text-danger"></span>') : '';

    //*** Candado Rojo
    !ValidarPermisoCRUD('U') ? $("#TblUpdate").append('<span class="fa fa-lock text-danger"></span>') : '';
    !ValidarPermisoCRUD('UR') ? $("#lbSelRol").text('Rol de Usuario: ').append('<span class="fa fa-lock text-danger"></span>') : '';
    !ValidarPermisoCRUD('D') ? $("#TblDelete").append('<span class="fa fa-lock text-danger"></span>') : '';
    !ValidarPermisoCRUD('EP') ? $("#TblEnviarPin").append('<span class="fa fa-lock text-danger"></span>') : '';
}

function CargarTablaUsuarios() {
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Usuario/ListadoUsuarios',
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

//*** Función Condicional de Estado
function spanEstado(estado) {

    if (estado === "A") {
        return '<small class="badge badge-success">Activo</small>';
    }
    else {
        return '<small class="badge badge-danger">Inactivo</small>';
    }
}
//*** Iniciar DataTable
function IniciarDataTable(callBackFN) {

    tabla = objtabla.DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "EXCEL",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                },
                title: function () {
                    return nombreReporte;
                },
                sheetName: 'Reporte',
                className: 'btn-success'
            }]
        ,
        "fnDrawCallback": function () {

            var tbl = objtabla.DataTable();

            if (tbl.data().length === 0) {
                tbl.buttons('.buttons-excel').disable();
                tbl.buttons('.btn-add').disable();
            }
            else {
                tbl.buttons('.buttons-excel').enable();
                tbl.buttons('.btn-add').enable();
            }
        },
        "columns": [
            { "width": "5%", "className": "textoCentrado text-bold" },
            null,
            null,
            null,
            { "className": "textoCentrado" },
            { "className": "textoCentrado text-bold" },
            { "width": "10%", "className": "textoCentrado" },
            { "width": "10%", "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "5%", "sortable": false, "className": "textoCentrado" }
        ],

        'bDestroy': true,
        "ordering": true,    //*** FALSE = Ordena según el script origen en SQL Server
        "bDeferRender": true,
        "paging": false,
        "scrollY": $(document).height() - 350 + "px", //30vh
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": true,
        "autoWidth": false,
        "responsive": true

    });

    var btnAgregar = '<button type="button" class="btn bg-gradient-primary btn-add" id="btnAgregar" style="margin-left:8px;">NUEVO USUARIO</button>';
    var btnMasivo = '<button type="button" class="btn  bg-gradient-danger  btn-add" id="btnMasivo" style="margin-left:8px;">ENVIAR PIN MASIVO</button>';
    var btnRefrescar = '<button type="button" class="btn  bg-gradient-secondary  btn-add" id="btnRefrescar" style="margin-left:8px;"><i class="nav-icon fas fa-redo"></i></button>';
    $('.buttons-excel').after(btnMasivo);
    $('.buttons-excel').after(btnAgregar);
    $('#btnMasivo').after(btnRefrescar);


    $('#tablaUsuarios tbody').on('click', 'tr', function () {
        if (!$(this).hasClass('selected')) {
            tabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    //*** Cargar Datos
    callBackFN();

    //*** Validar Permisos
    validarPermisos();

}
//*** Cargar Filas al DataTable
function CargarFilas(array) {

    //*** Limpiar Tabla despues de cualquier cambio 
    objtabla.dataTable().fnClearTable();

    var filas = [];

    array.map(function (element) {
        filas.push([
            element.Id_usuario,
            element.Login,
            element.Nombre,
            "<p style='margin:0px' data-idR='" + element.Id_rol + "'>" + element.Nombre_rol + "</p>",
            spanEstado(element.I_estado),
            element.Telefono,
            element.I_delegado === 'S' ? 'SI' : 'NO',
            element.Id_delegado === 0 ? '' : element.Id_delegado,
            '<img class="tooltipImgM" src="images/Iconos/update.png" style="cursor:pointer" data-toggle="tooltip" title="Modificar" />',
            '<img class="tooltipImgB" src="images/Iconos/delete.png" style="cursor:pointer" data-toggle="tooltip" title="Eliminar" />',
            '<img class="tooltipImgE" src="images/Iconos/send.png" style="cursor:pointer" data-toggle="tooltip" title="Enviar Token" />'
        ]);

    });

    //*** Agregar datos a la tabla
    objtabla.dataTable().fnAddData(filas, true);

    tabla.draw();
}

function obtenerRoles() {
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Usuario/ListadoRoles',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga roles", mensajeError);
        },
        success: function (data) {
            CargarComboRoles(data);
        }
    });
}

function CargarComboRoles(array) {

    //*** Roles Editar Usuario
    $('#selRol').empty();

    array.map(function (element) {
        //*** Roles Editar Usuario
        $('#selRol').append('<option value="' + element.Id_rol + '">' + element.Nombre + '</option>');
        //*** Roles Envío Masivo
        $('#selRolM').append('<option value="' + element.Id_rol + '">' + element.Nombre + '</option>');
    });

    //*** Roles Envío Masivo
    $('#selRolM').append('<option value="0">TODOS</option>');
}

function GuardarDatos() {

    MostrarCargando();
    DatosU.Login = $("#cedula").val();
    DatosU.Nombre = $("#nombre").val();
    DatosU.Telefono = $("#telefono").val();
    DatosU.Id_Rol = $("#selRol").val();
    DatosU.User_modifica = sessionStorage['Cedula'];
    DatosU.I_estado = $('#chkactivo').is(':checked') ? 'A' : 'I';
    DatosU.I_delegado = $('#chkDelegado').is(':checked') ? 'S' : 'N';
    DatosU.Id_delegado = $("#idDelegado").val();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Usuario/InsertarUsuario',
        data: JSON.stringify(DatosU),
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
                alertaExito('Guardar datos', 'Datos Guardados correctamente');
                CargarTablaUsuarios();
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
    DatosU.Login = $("#cedula").val();
    DatosU.Nombre = $("#nombre").val();
    DatosU.Telefono = $("#telefono").val();
    DatosU.Id_Rol = $("#selRol").val();
    DatosU.I_estado = $('#chkactivo').is(':checked') ? 'A' : 'I';
    DatosU.User_modifica = sessionStorage['Cedula'];
    DatosU.I_delegado = $('#chkDelegado').is(':checked') ? 'S' : 'N';
    DatosU.Id_delegado = $("#idDelegado").val();


    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Usuario/ModificarUsuario',
        data: JSON.stringify(DatosU),
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
                alertaExito('Guardar datos', 'Datos Modificados correctamente');
                CargarTablaUsuarios();
                CerrarCargando();
            } else {
                alertaError("Guardar datos", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function EliminarDatos(loginEliminar) {
    MostrarCargando();
    DatosU.Login = loginEliminar;
    DatosU.User_modifica = login;
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Usuario/EliminarUsuario',
        data: JSON.stringify(DatosU),
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
                alertaExito('Eliminar usuario', 'Usuario eliminado correctamente');
                CargarTablaUsuarios();
                CerrarCargando();
            } else {
                alertaError("Eliminar usuario", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function validarDatosPantalla() {

    if ($("#cedula").val().length < 7) {
        alertaError('Datos pantalla', 'La cédula debe ser mayor a 7 carac');
        return 1;
    }

    if ($.trim($("#nombre").val()).length < 5) {
        alertaError('Datos Pantalla', 'El nombre debe ser mayor a 5 caracteres');
        return 1;
    }

    if ($.trim($("#telefono").val()).length < 8) {
        alertaError('Datos Pantalla', 'La descripción abreviada de la pantalla debe ser mayor a 5 caracteres');
        return 1;
    }

    if ($("#selRol").val() === undefined || $("#selRol").val() === null) {
        alertaError('Datos Pantalla', 'Debe seleccionar un rol');
        return 1;
    }

    if ($("#selRol").val().length < 1) {
        alertaError('Datos Pantalla', 'Debe seleccionar un rol');
        return 1;
    }
    return 0;
}

function GuardarModificarPantalla(tipo) {
    if (tipo === 'guardar') {
        GuardarDatos();
    } else {
        ModificarDatos();
    }
}

function EnviarMensajeTexto(tipo) {
    if (tipo === 'D') {
        EnviarDatosD();
    } else {
        EnviarDatos();
    }
}

//*** Envío de PIN Individual 
function EnviarDatos() {

    MostrarCargando();
    DatosSMS.Login = $("#cedulaE").val();
    DatosSMS.Indgenera = $('#ckCambiaPin').is(':checked') ? 'S' : 'N';
    DatosSMS.Usuario = login;
    DatosSMS.Tipo = 'I';
    DatosSMS.TelefonoAux = telefonoEnvioPin;

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Usuario/EnviarSms',
        data: JSON.stringify(DatosSMS),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Envío SMS", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExito('Envío SMS', 'Pin enviado correctamente a ' + telefonoEnvioPin);
                CerrarCargando();
            } else {
                alertaError("Envío SMS", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

//*** Envío de PIN Individual a TABLETA
function EnviarDatosD() {

    MostrarCargando();

    var NombreDispositivo = $("#selDispositivo option:selected").text();

    var parametros = new Object();
    parametros.Login = $("#cedulaE").val();
    parametros.Indgenera = $('#ckCambiaPin').is(':checked') ? 'S' : 'N';
    parametros.Usuario = login;
    parametros.Id_dispositivo = $("#selDispositivo").val();
    parametros.Tipo = 'I';

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Usuario/EnviarSmsD',
        data: JSON.stringify(parametros),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Envío SMS", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExito('Envío SMS', 'Pin enviado correctamente al dispositivo ' + NombreDispositivo);
                CerrarCargando();
            } else {
                alertaError("Envío SMS", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function validarDelegado(login) {
    var api;
    if ($('#modal-lg-usuario').data('tipo') === 'guardar') {
        api = apiUrl + '/api/login/Usuario/BuscarDelegadoI';
    } else {
        api = apiUrl + '/api/login/Usuario/BuscarDelegado';
    }

    MostrarCargando();
    DatosU.Login = login;
    $.ajax({
        type: 'POST',
        url: api,
        data: JSON.stringify(DatosU),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("validar delegado", mensajeError);
            $('#chkDelegado').prop("checked", false);
            $("#idDelegado").val('');
            CerrarCargando();
        },
        success: function (data) {
            if (data === undefined || data === null) {
                alertaError("validar delegado", "No existe delegado asociado a esa cédula. Favor revisar.");
                $('#chkDelegado').prop("checked", false);
                $("#idDelegado").val('');
                CerrarCargando();
            }
            else if (data.Id_delegado === 0) {
                $('#chkDelegado').prop("checked", false);
                $("#idDelegado").val('');
                alertaError("validar delegado", "Este usuario no está registrado como delegado. Favor revisar.");
                CerrarCargando();
            } else {
                $("#idDelegado").val(data.Id_delegado);
                CerrarCargando();
            }
        }
    });

}

function enviarSMSMasivo() {

    MostrarCargando();

    var idrol = $("#selRolM").val();
    var i_delegado = $("input:radio[name='optDelegados']:checked").val();
    var i_estado = $("input:radio[name='optestado']:checked").val();
    var genera = $('#ckCambiaPinM').is(':checked') ? 'S' : 'N';

    $.ajax({
        type: 'GET',
        url: apiUrl + '/api/Login/Usuario/EnvioSmsMasivo?usuario=' + sessionStorage['Cedula'] + '&idrol=' + idrol + '&i_delegado=' + i_delegado + '&i_estado=' + i_estado + '&genera=' + genera,
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Envío Masivo de sms", mensajeError);
            CerrarCargando();
        },
        success: function (data) {

            if (data.Error === 0) {
                alertaExito('Envío Masivo SMS', data.Mensaje);
                CerrarCargando();
            } else {
                alertaError("Envío Masivo SMS", data.Mensaje);
                CerrarCargando();
            }
        }
    });
}

function obtenerDispositivos() {
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/login/Dispositivo/ListaDispositivosActivos',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga dispositivos", mensajeError);
        },
        success: function (data) {
            CargarComboDispositivo(data);
        }
    });
}

function CargarComboDispositivo(array) {
    $('#selDispositivo').empty();
    array.map(function (element) {
        $('#selDispositivo').append('<option value="' + element.Id_dispositivo + '">' + element.Nombre + '</option>');
    });
}
