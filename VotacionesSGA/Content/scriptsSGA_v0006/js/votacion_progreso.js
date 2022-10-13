const Toast = Swal.mixin({
    position: 'center'
});

var tabla, dataReportes;
var objTabla = $('#tbl_tabla');
var refresh_Interval_TablaId;

var apiUrl = sessionStorage['Uri'];
var Segundos;
var Id_eleccion;
var Id_ronda;
var Login;

//*** Objeto - Datos de Elección / Ronda
var DatosER = new Object();

//*** #1 - Iniciar 
$(document).ready(function () {

    //*** 1.1 - Mostrar Modal
    $("#modal_inicial").modal('show');
    //*** 1.2 - Cargar Elecciones
    obtenerElecciones();
    //*** 1.3 - Iniciar Tabla
    iniciarDataTable();
    //*** 1.4 - Botón Aceptar del Modal
    $(document).on('click', '#btnGuardar', function () {

        //*** 1.4.1 Validar Datos del Modal
        if (validarDatosPantalla() === 1) {
            return;
        }
        //*** 1.4.2 Mensaje de confirmación
        Toast.fire({
            toast: false,
            type: 'warning',
            title: 'VOTACIÓN EN PROGRESO',
            html: '<h5>Está seguro de su selección ?</h5>',
            allowOutsideClick: false,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {

            //*** 1.4.2.1 Define valores de Variables Globales y Carga Resultados
            if (result.value) {

                //*** Asignación de Variables Generales
                Segundos = $('#modalSegundos').val() * 1000;
                Id_eleccion = $('#modalEleccion').val();
                Id_ronda = $('#modalRonda').val();
                Login = sessionStorage.getItem('Cedula');

                //*** Cargar Resultados
                cargarResultados();
                //*** Cargar Resultados Recurrente
                refresh_Interval_TablaId = setInterval(function () {
                    cargarResultados();
                }, Segundos);

                //*** Ocultar Modal
                $("#modal_inicial").modal('hide');
            }
        });
    });
    //*** 1.5 - Cuando se muestre el Modal Inicial
    $(document).on('shown.bs.modal', '#modal_inicial', function (e) {
        //*** Enfocar el campo Segundos
        $('#modalSegundos').focus();
        //*** Validar Tecla Enter
        $('#modal_inicial').on("keypress", function (e) {
            if (e.which === 13) {
                $('#btnGuardar').click();
            }
        });
    });
});

//*** (1.2) #2 - Carga de Elecciones
function obtenerElecciones() {

    //*** 2.1 - Cargar lista de Elecciones
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Eleccion/ListadoElecciones',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga de Elecciones", mensajeError);
        },
        success: function (data) {
            //*** Limpiar 
            $('#modalEleccion').empty();
            //*** Cargar
            data.map(function (element) {
                $('#modalEleccion').append('<option value="' + element.Id_eleccion + '">' + element.Nombre + '</option>');
            });
            //*** 2.2 - Cargar Lista de Rondas
            obtenerRondas();
        }
    });
}
//*** (2.2) #3 - Carga de Rondas
function obtenerRondas() {

    Id_eleccion = $('#modalEleccion').val();

    if ($(Id_eleccion !== "0")) {

        DatosER.Id_eleccion = Id_eleccion;
        //*** Estandar - Enviar Variable al API mediante ( var = data )
        var data = JSON.stringify({ DatosER });
        //*** 3.1 - Cargar la lista de Rondas
        $.ajax({
            type: 'POST',
            url: apiUrl + '/api/Asamblea/EleccionRonda/ListaRondasPorEleccion',
            data: data,
            contentType: 'application/json; charset=utf-8',
            error: function (xhr, ajaxOptions, thrownError) {
                mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
                alertaError("Carga de Rondas", mensajeError);
            },
            success: function (data) {
                //*** Limpiar
                $('#modalRonda').empty();
                //*** Cargar
                data.map(function (element) {
                    $('#modalRonda').append('<option value="' + element.Id_ronda + '">' + element.Nombre + '</option>');
                });
            }
        });
    }
}
//*** (1.3) #4 - Iniciar Tabla 
function iniciarDataTable() {

    tabla = objTabla.DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "EXCEL",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
                    stripHtml: true
                },
                title: "Votaciones Progreso",
                sheetName: 'Votaciones Progreso',
                className: 'btn-success mr-2'
            },
            {
                extend: 'pdfHtml5',
                text: "PDF",
                pageSize: 'A4',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
                    stripHtml: true
                },
                className: 'btn-danger'
            }]
        , "fnDrawCallback": function () {

            var tbl = objTabla.DataTable();

            if (tbl.data().length === 0)
                tbl.buttons('.buttons-html5').disable();
            else
                tbl.buttons('.buttons-html5').enable();

        }, "initComplete": function () {
            //*** Quitar thead del Scroll #1
            $('.dataTables_scrollBody thead tr').css({ visibility: 'collapse' });
        },

        'bDestroy': true,
        "ordering": true,    //Ordena según el script origen en SQL Server
        "bDeferRender": true,
        "paging": false,
        "scrollY": $(document).height() - 348 + "px", //30vh

        "scrollCollapse": true,
        "scroller": true,
        "bSort": true,
        "autoWidth": true,
        "responsive": true,
        //*** Opciones: { "className": "text-center", "bSortable": false, "bSearchable": false }
        "aoColumns": [
            { "className": "text-center" },                                          //*** DELEGADO 
            { "className": "text-center" },                                          //*** LOGIN
            { "width": "20%","className": "text-center" },                           //*** NOMBRE 
            { "className": "text-center" },                                          //*** TELEFONO                         
            { "className": "text-center" },                                          //*** PAPELETAS
            { "className": "text-center" },                                          //*** VOTOS
            { "className": "text-center" },                                          //*** FECHA INICIO
            { "className": "text-center" },                                          //*** FECHA FIN
            { "className": "text-center" },                                          //*** Duracion
            { "className": "text-center" },                                          //*** PROGRESO
            { "className": "text-center", "bSortable": false}                        //*** SESIONES
        ]
    });
}
//*** (1.4.1) #5 - Validar Datos de Entrada
function validarDatosPantalla() {

    if ($("#modalSegundos").val() < 5 || $("#modalSegundos").val() > 60) {
        alertaError('CONTROL DE DATOS', 'Seleccione un intervalo de actualización entre 5 y 60 segundos.');
        return 1;
    }

    if ($("#modalEleccion").val() === undefined || $("#modalEleccion").val() === null) {
        alertaError('CONTROL DE DATOS', 'Seleccione una ELECCIÓN de la lista.');
        return 1;
    }

    if ($("#modalRonda").val() === undefined || $("#modalRonda").val() === null) {
        alertaError('CONTROL DE DATOS', 'Selecciones una RONDA de la lista.');
        return 1;
    }

    return 0;
}
//*** (1.6) #6 - Cargar Resultados
function cargarResultados() {

    var parametros = new Object();

    parametros.Id_eleccion = Id_eleccion;
    parametros.Id_ronda = Id_ronda;
    parametros.Usuario = Login;

    var data = JSON.stringify({ parametros });

    //*** AJAX - Cargar resultados en progreso
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Resultado/ResultadoProgreso',
        data: data,
        async: true,
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga de Resultados", mensajeError);
        },
        success: function (data) {
            if (data.length === 0) {

                //*** Detener Invertalo de Refrescamiento
                clearInterval(refresh_Interval_TablaId);

                //*** Mostrar mensaje de Carga de Registros = 0
                Toast.fire({
                    toast: false,
                    type: 'info',
                    title: 'CARGA DE REGISTROS',
                    html: '<h5>No existen registros de votaciones en progreso para <b>Elección: </b>' + $('#modalEleccion').text() + ' | <b>Ronda : </b>' + $("#modalRonda option:selected").text() + '</h5>',
                    allowOutsideClick: true,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Aceptar'

                }).then(function (result) {

                    if (result.value) {
                        //*** Mostrar Modal Inicial
                        $("#modal_inicial").modal('show');
                    }
                });

            } else {

                //*** Total de Delegados Votando
                var totalDelegados = data.length;
                var totalFinalizados = contadorFinalizados(data,'FINALIZADO');
                //*** Texto Totales
                $('.card-titulo').html('<i class="fas fa-square text-success mr-2"></i><b>PROGRESO DE VOTACIONES  ( TOTAL : </b> <b style="font-size: 20px;" class="text-danger ml-2">' + totalDelegados + '</b> |  FINALIZADO: <b style="font-size: 20px;" class="text-danger ml-2">' + totalFinalizados +'</b> )');
                //*** Agregar FIlas a la Tabla
                agregarFilasDT(data);
            }
        }
    });
}
//*** #7 Agregar Filas a la Tabla 
function agregarFilasDT(data) {

    //*** 7.1 - Limpiar Tabla despues de cualquier cambio 
    objTabla.dataTable().fnClearTable();

    var filas = [];

    for (var i = 0; i < data.length; i++) {

        filas.push([
            data[i].Id_delegado,
            data[i].Login,
            data[i].Nombre,
            data[i].Telefono,
            data[i].Papeletas,
            data[i].Votos,
            data[i].F_inicio_voto,
            data[i].F_fin_voto,
            data[i].Duracion,
            spanEstado(data[i].Progreso),
            data[i].Sesion_act
        ]);
    }

    //*** 7.2 - Agregar Datos
    objTabla.dataTable().fnAddData(filas, false);

    //***7.3 - Dibujar la Tabla
    tabla.draw();

    //***7.4 - Crear botones adicionales
    if ($('#btnRefrescar').length === 0) {

        var btnRefrescar = '<button type="button" class="btn  bg-gradient-secondary" id="btnRefrescar" style="margin-left:8px;">Refrescar</button>';
        $('.buttons-pdf').after(btnRefrescar);

        //*** 7.4.1 - Botón Aceptar del Modal
        $(document).on('click', '#btnRefrescar', function () {
            location.reload(true);
        });
    }
}
//*** #8 Función Condicional de Progreso
function spanEstado(progreso) {

    switch (progreso) {
        case 'VOTANDO':
            return '<small class="badge badge-success">' + progreso +'</small>';
        case 'FINALIZADO':
            return '<small class="badge badge-danger">' + progreso + '</small>';
        default:
            return '<small class="badge badge-info">' + progreso + '</small>';
    }
}
//*** #9 Contador de Votos Finalizdos
function contadorFinalizados(array, valor) {
    var contador = 0;
    $.each(array, function (i, v) { if (v.Progreso === valor) contador++; });
    return contador;
}