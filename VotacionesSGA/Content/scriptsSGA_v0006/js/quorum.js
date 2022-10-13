
var apiUrl = sessionStorage['Uri'];
var objtablaI = $('#tablaingreso');
var objtablaS = $('#tablasalida');
var TotalD;
var etiquetas_Graf1 = [];
var datos_Graf1 = [];
var TotalIngresos;
var IntervalGrafico;

//*** Registrar etiquetas
Chart.plugins.register(ChartDataLabels);
//*** Cambiar opciones de etiqueta
Chart.helpers.merge(Chart.defaults.global.plugins.datalabels, {
    align: 'end'
    , anchor: 'center'
    , clamp: true
    , color: 'black'
    , labels: {
        title: {
            font: {
                weight: 'bold'
            }
        }
    },
    backgroundColor: function (context) {
        return context.dataset.backgroundColor;
    },
    borderColor: 'darkred',
    borderRadius: 25,
    borderWidth: 2,
    font: {
        size: 35,
        weight: 'bold'
    }
});
//*** #1 - Inicio
$(document).ready(function () {

    //*** 1.1 - Iniciar Modulo 
    IniciarModulo(ValidarQ);
    //*** 1.2 - Total Delegados 
    TotalDFuncion();
    //*** 1.3 - Eventos Click - Registrar
    $(document).on('click', '#btnRegistrar', function () {

        var cedula = $("#cedula").val();

        if (cedula === '0' || $.trim(cedula) === '') {
            return false;
        } else {
            RegistroIS($.trim($("#cedula").val()));
        }
    });
    //*** 1.4 - Tecla Enter
    $('#cedula').on("keypress", function (e) {
        if (e.keyCode === 13) {

            if ($(this).val() === '0' || $.trim($(this).val()) === '') {
                return false;
            } else {
                RegistroIS($.trim($("#cedula").val()));
            }
        }
    });
    //*** 1.5 - Botón Borrar ( Solo Lista de Roles )
    $(document).on('click', '.tooltipImgB', function () {

        //*** Permiso - Borrar registro de Quorum
        if (ValidarPermisoCRUD('D')) {

            var cedula = $(this).closest("tr").find('td:eq(1)').text();

            Swal.close();
            Swal.fire({
                title: 'Eliminar Datos',
                html: '¿Desea Eliminar el usuario <b>' + cedula + '</b>?',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: 'Cancelar',
                confirmButtonText: 'Aceptar'
            }).then(function (result) {
                if (result.value) {
                    EliminarD(cedula);
                }
            });
        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }
    });
    //*** 1.6 - Botón Abrir Quorum ( Solo Lista de Roles )
    $(document).on('click', '#btnAceptar', function () {

        //*** Permiso - Abrir Quorum
        if (ValidarPermisoCRUD('AQ')) {
            AbrirQ();
            $('#modal-lg-abri-quorum').modal('hide');
        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }

    });
    //*** 1.7 - Botón Cerrar Quorum ( Solo Lista de Roles )
    $(document).on('click', '#btnCerrarQ', function () {

        //*** Permiso - Cerrar Quorum
        if (ValidarPermisoCRUD('CQ')) {

            Swal.close();
            Swal.fire({
                title: 'Cerrar Quorum',
                html: '¿Desea cerrar el Quorum?',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: 'Cancelar',
                confirmButtonText: 'Aceptar'
            }).then(function (result) {
                if (result.value) {
                    CerrarQ();
                }
            });
        } else {
            //** Mensaje de Bloqueo
            mensajeBloqueoToast();
        }
    });
    //*** 1.8 - Botón Refrescar
    $(document).on('click', '#btnRefrescar', function () {
        location.reload(true);
    });
    //*** 1.9 - Botón Mostrar Gráfico
    $(document).on('click', '#btnGrafico', function () {

        $("#modal_gradico").modal('show');

        crearGrafico();

        IntervalGrafico = setInterval(function () { crearGrafico(); }, 35000);

    });
    //*** 1.10 - Limpiar Intervalo al ocultar Gráfico
    $('#modal_gradico').on('hidden.bs.modal', function () {
        //*** 10.1 - Limpiar Graficos
        clearInterval(IntervalGrafico);
    });

});
//*** (1.1) #2 - IniciarModulo(callBackFN)
function IniciarModulo(callBackFN) {

    //*** 2.1 - Iniciar DataTable
    iniciarDataTable(objtablaI, 'Reporte de Entradas');
    iniciarDataTable(objtablaS, 'Reporte de Salidas');

    //*** Validar Permisos
    validarPermisos();

    //*** 2.3 - Cargar datos del DataTable
    callBackFN();
}
//*** (2.1) #3 - Iniciar Tablas
function iniciarDataTable(objTabla, nombreReporte) {

    objTabla.DataTable({
        //data: filas,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "EXCEL",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                },
                title: function () {
                    return nombreReporte;
                },
                sheetName: 'Reporte Quorum',
                className: 'btn-success ml-2'
            }],

        "fnDrawCallback": function () {

            var tbl = objTabla.DataTable();

            if (tbl.data().length === 0)
                tbl.buttons('.buttons-excel').disable();
            else
                tbl.buttons('.buttons-excel').enable();
        },

        'bDestroy': true,
        "ordering": false,    //*** FALSE = Ordena según el script origen en SQL Server
        "bDeferRender": true,
        "paging": false,
        "scrollY": $(document).height() - 556 + "px",
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": true,
        "autoWidth": false,
        "responsive": true,

        "columns": [
            { "width": "5%", "sortable": false, "className": "textoCentrado" },
            { "width": "15%", "sortable": false },
            { "width": "44%", "sortable": false },
            { "width": "5%", "sortable": false, "className": "textoCentrado text-danger text-bold" },
            { "width": "26%", "sortable": false },
            { "width": "5%", "sortable": false, "className": "textoCentrado" }
        ]
    });
}
//*** (1.2) #4 - Total Delegados
function TotalDFuncion() {
    //*** 2.1 - Carga Ajax del Total de Delegados
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Delegado/TotalDelegados',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga Total de Delegados", mensajeError);
            console.log(mensajeError);
            TotalD = 0;
        },
        success: function (data) {
            TotalD = data;
            $('#tituloQ').html('QUORUM - TOTAL EN EL PADRÓN &nbsp;&nbsp; <span class="badge badge-danger">' + TotalD + '</span>');
        }
    });
}
//*** (2.3) #5 - Validar Quorum
function ValidarQ() {
    //*** 5.1 - Mostrar Cargando
    MostrarCargando();
    //*** 5.2 - Validar Quorum
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Quorum/ValidarQ',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Validar Quorum", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            //***Validar Quorum ( 1 = No hay Quorum abierto, 2 = Quorum Abierto )
            if (data === 1) {

                $('#modal-lg-abri-quorum').modal({
                    backdrop: 'static',
                    keyboard: false
                }, 'show');

                CerrarCargando();

            } else {

                if (data === 2) {
                    //*** 5.3 - Ejecuta la Carga de Tablas
                    EjecutarCargaTablas();
                    //*** 5.4 - Intervalo de actualización de las Tablas
                    setInterval(function () { CargarTablas('I'); CargarTablas('S'); }, 30000);
                } else {
                    //*** Código devuelto no corresponde ( 1 = No hay Quorum abierto, 2 = Quorum Abierto )
                    alertaError("Validar Quorum", "El código devuelto debe ser 1 = No hay Quorum abierto, 2 = Quorum Abierto. Código :" + data);
                }
            }
        }
    });

}
//*** (1.3) #6 - Registro de Ingreso / Salida
function RegistroIS(id) {

    //*** 6.1 - Muestra Modal Cargando
    MostrarCargando();

    $("#bIngreso").removeClass("border-shine");
    $("#bSalida").removeClass("border-shine");

    var parametros = new Object();

    parametros.cedula = id;
    parametros.usuario = sessionStorage['Cedula'];

    //*** Limpiar textbox de Registro
    $("#cedula").val('');

    //*** 6.2 - Registrar Ingreso / Salida
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Quorum/RegistrarIS',
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

            //*** 1 = Entrada | 2 = Salida | 3 = No Existe
            if (data === 1) {
                alertaExitoMin('ENTRADA REGISTRADA CORRECTAMENTE');
                $("#bIngreso").addClass("border-shine");
                EjecutarCargaTablas();

            } else if (data === 2) {
                alertaExitoMin2('SALIDA REGISTRADA CORRECTAMENTE');
                $("#bSalida").addClass("border-shine");
                EjecutarCargaTablas();

            } else if (data === 3) {
                alertaErrorMin('LA CÉDULA INGRESADA NO EXISTE ' + id);
                CerrarCargando();

            } else {
                alertaErrorMin('ERROR AL REGISTRAR, VERIFIQUE SI EXISTE UN QUORUM ABIERTO.');
                CerrarCargando();
            }
        }
    });
}
//*** (6.2 | 5.3) #7 - Ejecutar Carga de Tablas
function EjecutarCargaTablas() {

    //*** 7.1 - Cargar Tablas Ingreso y Salida
    setTimeout(function () {
        CargarTablas('I');
        CargarTablas('S');
        //*** 7.2 -  Cerrar Cargando
        CerrarCargando();
    }, 1000);

}
//*** (7.2) #8 - Cargar de Tablas
function CargarTablas(tipo) {

    var totalI = 0;
    var totWid = 0;

    //*** 8.1 - Ejecutar Ajax de Carga de Quorum
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Quorum/ListaQuorum',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        data: JSON.stringify(tipo),
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Lista Quorum", mensajeError);
        },
        success: function (data) {

            var tbl;

            if (tipo === 'I') {

                //*** 8.2 - Cargar Filas Ingreso
                CargarFilas(data, objtablaI);

                $('#tablaingreso tbody').on('click', 'tr', function () {
                    if (!$(this).hasClass('selected')) {
                        $('#tablaingreso tr.selected').removeClass('selected');
                        $(this).addClass('selected');
                    }
                });

                tbl = objtablaI.DataTable();
                totalI = tbl.data().length;
                TotalIngresos = totalI;

                $('#tingresos').text(totalI);
                $('#TimeLT').html('&nbsp;<b>' + totalI + '</b>&nbsp;&nbsp;|&nbsp;&nbsp;<b class="text-danger">' + TotalD + '&nbsp;<b>');
                totWid = (totalI * 100 / TotalD).toFixed(0);
                $('#TimeLW').width(totWid + '%');
                $('#TimeLW').html('<b style="font-size: 20px; color: black">' + totWid + ' %</b>');

            } else {
                //*** 8.3 - Cargar Filas Salida
                CargarFilas(data, objtablaS);

                $('#tablasalida tbody').on('click', 'tr', function () {
                    if (!$(this).hasClass('selected')) {
                        $('#tablasalida tr.selected').removeClass('selected');
                        $(this).addClass('selected');
                    }
                });

                tbl = objtablaS.DataTable();
                $('#tsalidas').text(tbl.data().length);
            }
        }
    });
}
//*** (8.2 | 8.3) #9 - Cargar Filas
function CargarFilas(array, objTabla) {

    //*** Limpiar Tabla despues de cualquier cambio 
    objTabla.dataTable().fnClearTable();

    //*** Si existen datos se actualiza la tabla
    if (array.length > 0) {

        var filas = [];
        array.map(function (element) {
            filas.push([
                element.Id_delegado,
                element.Cedula,
                element.Nombre,
                element.Num_paleta,
                element.F_ing_sal,
                '<img class="tooltipImgB" src="images/Iconos/delete.png" style="cursor:pointer" data-toggle="tooltip" title="Eliminar" />'
            ]);
        });

        //*** Agregar datos a la tabla
        objTabla.dataTable().fnAddData(filas, true);

        //*** Dibujar la Tabla ( No funciona por pasar por parametro TablaX)
        //tabla.draw();

    }
}
//*** (1.5) #10 - Borrar Registro de Quorum
function EliminarD(cedula) {

    MostrarCargando();

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Quorum/EliminarD',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        data: JSON.stringify(cedula),
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Eliminar registro de Quorum", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExitoMin('Delegado eliminado Correctamente.');
                EjecutarCargaTablas();
            } else {
                alertaErrorMin(data.Mensaje);
                CerrarCargando();
            }
        }
    });
}
//*** (1.6) #11 - Abrir Quorum
function AbrirQ() {
    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Quorum/AbrirQ',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        data: JSON.stringify(sessionStorage['Cedula']),
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Abrir Quorum", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                EjecutarCargaTablas();
            } else {
                alertaErrorMin(data.Mensaje);
                CerrarCargando();
            }
        }
    });
}
//*** (1.7) #12 - Cerrar Quorum
function CerrarQ() {
    //*** Mostrar Cargando
    MostrarCargando();
    //*** Cerrar Quorum
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Quorum/CerrarQ',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        data: JSON.stringify(sessionStorage['Cedula']),
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Cerrar Quorum", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            if (data.Error === 0) {
                alertaExitoMin('Quorum cerrado Correctamente.');
                EjecutarCargaTablas();
            } else {
                alertaErrorMin(data.Mensaje);
                CerrarCargando();
            }
        }
    });
}
//*** (1.9) #13 Crear Gráfico
function crearGrafico() {

    //*** 13.1 - Limpiar Graficos
    limpiarGraficos();

    //*** Cargar Gráfico
    //*** Valores de Gráfico 2
    etiquetas_Graf1.push('PRESENTES');
    etiquetas_Graf1.push('AUSENTES');

    var presentes = TotalIngresos;
    var ausentes = TotalD - TotalIngresos;

    datos_Graf1.push(presentes);
    datos_Graf1.push(ausentes);

    //*** Texto de votos Totales
    $('#lbl_title').html('<i class="fas fa-square text-success mr-2"></i><b style="font-size: 30px;">ESTADO QUORUM - TOTAL EN PADRÓN:</b> <b style="font-size: 30px;" class="text-danger ml-2">' + TotalD + '</b>');

    //*** 13.2 - Cargar Gráfico
    cargarGraficoBar('ctxGrafico2', 'pie', datos_Graf1, etiquetas_Graf1);
}
//*** (13.2) #14 - Cargar Grafico 1
function cargarGraficoBar(id, tipo, datos, etiquetas) {

    //*** Refrescar el Gráfico
    $('#' + id).remove();
    $('.' + id).append('<canvas id="' + id + '" width="400" height="240"></canvas>');

    var ctxGrafico = document.getElementById(id).getContext('2d');
    var myChart = new Chart(ctxGrafico, {
        type: tipo,
        data: {
            labels: etiquetas,
            datasets: [{
                label: 'CANTIDAD DE REGISTROS',
                data: datos,
                borderWidth: 2,
                backgroundColor: [
                    'rgba(48, 203, 0, 0.2)',
                    'rgba(241, 130, 141,1)'
                ],
                borderColor: [
                    'rgba(48, 203, 0, 1)',
                    'rgba(217, 30, 24, 1)'
                ]
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        fontColor: 'black',
                        fontSize: 20
                    }
                }]
                , xAxes: [{
                    ticks: {
                        min: 0,
                        stepSize: 10, //*** Aumentos 
                        fontColor: 'black',
                        fontSize: 30
                    }
                }]
            }
        }
    });
}
//*** (13.1) #15 - limpiarGraficos
function limpiarGraficos() {
    //*** Limpiar los gráficos
    datos_Graf1.length = 0;
    etiquetas_Graf1.length = 0;
}

function validarPermisos() {

    $('#content .fa-lock').remove();

    //*** Validar permisos y mostrar el Candado de Bloqueo si no los posee

    //*** Candado Blanco
    !ValidarPermisoCRUD('CQ') ? $("#btnCerrarQ").append('<span class="fa fa-lock pl-2"></span>') : '';

    //*** Candado Rojo
    !ValidarPermisoCRUD('AQ') ? $("#btnAceptar").append('<span class="fa fa-lock text-danger pl-2"></span>') : '';
    !ValidarPermisoCRUD('D') ? $(".TblDelete").append('<span class="fa fa-lock text-danger"></span>') : '';
}


