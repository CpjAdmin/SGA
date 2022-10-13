const Toast = Swal.mixin({
    position: 'center'
});

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
        size: 16,
        weight: 'bold'
    }
});

var tabla, dataReportes;
var objTabla = $('#tbl_tabla');

var apiUrl = sessionStorage['Uri'];
var Id_eleccion;
var Id_ronda;
var Login;

//*** Parametros Globales
var Id_papeleta;
var Nombre_papeleta;
var etiquetas_Graf1 = [];
var datos_Graf1 = [];
var etiquetas_Graf2 = [];
var datos_Graf2 = [];

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

//*** #1 - Iniciar 
$(document).ready(function () {

    //*** 1.1 - Acciones Iniciales
    accionesIniciales();
    //*** 1.2 - Cargar Elecciones
    obtenerElecciones();
    //*** 1.3 - Iniciar Tabla
    iniciarDataTable();
    //*** 1.4 - Boton Aceptar del Modal
    $(document).on('click', '#btnGuardar', function () {
        //*** 1.4.1 - Validar los datos seleccionados
        if (validarDatosPantalla() === 1) {
            return;
        }
        //*** 1.4.2 Mensaje Toast de confirmación de carga de resultados
        Toast.fire({
            toast: false,
            type: 'warning',
            title: 'CARGA DE RESULTADOS',
            html: '<h5>Está seguro de su selección ?</h5>',
            allowOutsideClick: false,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {

            if (result.value) {

                //*** 1.4.2.1 - Asignación de Variables Generales
                Id_eleccion = $('#modalEleccion').val();
                Id_ronda = $('#modalRonda').val();
                Login = sessionStorage.getItem('Cedula');

                //*** 1.4.2.2 - Iniciar la carga de la interfaz de resultados 
                IniciarCargaInterfaz(); 

                //*** 1.4.2.3 - Ocultar Modal
                $("#modal_inicial").modal('hide');

            } else {
                return;
            }
        });
    });
    //*** 1.5 - Btn Detalle Resultado
    $(document).on('click', '#btn_detalle_resultado', function (e) {
        e.preventDefault();
        $("#modal_detalle").modal('show');
    });
    //*** 1.6 - Botón Refrescar 
    $(document).on('click', '#btnRefrescar', function () {
        location.reload(true);
    });
    //*** 1.7 - Exportar la pantalla completa a PDF 
    $('#btn_descargar_pdf').click(function () {
        domtoimage.toPng(document.getElementById('content'))
            .then(function (blob) {
                var pdf = new jsPDF('l', 'pt', [$('#content').width(), $('#content').height()]);

                pdf.addImage(blob, 'PNG', 0, 0, $('#content').width(), $('#content').height());
                var pdfNombre = "Resultados_E_" + Id_eleccion + "_R_" + Id_eleccion + "_P_" + Nombre_papeleta + ".pdf";
                pdf.save(pdfNombre);
            });
    });
    //*** 1.8 - Cuando se muestre el Modal Detalle
    $(document).on('shown.bs.modal', '#modal_detalle', function (e) {

        //*** 1.8.1 Carga Resultados
        cargarResultadosDetalle();
    });
    //*** 1.9 - Cuando se muestre el Modal Inicial
    $(document).on('shown.bs.modal', '#modal_inicial', function (e) {
        //*** Tecla Enter
        if ($('#modal_inicial').is(':visible')) {
            $('#modal_inicial').on("keypress", function (e) {
                console.log(e.which);
                if (e.which === 13) {
                    $('#btnGuardar').click();
                }
            });
        }
    });
});
//*** (1.1) #2 - Acciones Iniciales
function accionesIniciales(){
    //***Inactivar botones
    $('#btn_detalle_resultado').attr("disabled", true);
    $('#btn_descargar_pdf').attr("disabled", true);
    $('.btn-papeletas').attr("disabled", true);

    //*** Mostrar Modal
    $("#modal_inicial").modal('show');
}
//*** (1.2) #3 - Carga de Elecciones
function obtenerElecciones() {

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
            //*** 2.1 - Obtener lista de Rondas
            obtenerRondas();
        }
    });
}
//*** (2.1) #4 - Carga de Rondas
function obtenerRondas() {

    Id_eleccion = $('#modalEleccion').val();

    if ($(Id_eleccion !== "0")) {

        DatosER.Id_eleccion = Id_eleccion;

        $.ajax({
            type: 'POST',
            url: apiUrl + '/api/Asamblea/EleccionRonda/ListadoEleccionesR',
            data: JSON.stringify(DatosER),
            contentType: 'application/json; charset=utf-8',
            error: function (xhr, ajaxOptions, thrownError) {
                mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
                alertaError("Carga roles", mensajeError);
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
//*** (1.3) #5 - Iniciar Tabla 
function iniciarDataTable() {

    tabla = objTabla.DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "EXCEL",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5],
                    stripHtml: false
                },
                title: "Resultados Resumen",
                sheetName: 'Resultados Resumen',
                className: 'btn-success mr-2'
            },
            {
                extend: 'pdfHtml5',
                text: "PDF",
                pageSize: 'A4',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5],
                    stripHtml: false
                },
                className: 'btn-danger'
            }]
        , "fnDrawCallback": function () {

            var tbl = objTabla.DataTable();

            if (tbl.data().length === 0)
                tbl.buttons('.buttons-html5').disable();
            else
                tbl.buttons('.buttons-html5').enable();

            //*** Agrupar
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(0, { page: 'current' }).data().each(function (group, i) {
                if (last !== group) {
                    $(rows).eq(i).before(
                        '<tr class="group"><td colspan="10" style="text-align: center;" >' + group + '</td></tr>'
                    );
                    last = group;
                }
            });

        }, "initComplete": function () {
            //*** Quitar thead del Scroll #1
            $('.dataTables_scrollBody thead tr').css({ visibility: 'collapse' });
        },

        "aaSorting": [[0, 'asc'], [5, 'asc']],
        'bDestroy': true,
        "bDeferRender": true,
        "paging": false,
        "scrollY": $(document).height() - 380 + "px", //30vh

        "scrollCollapse": true,
        "scroller": true,
        "bSort": true,
        "autoWidth": true,
        "responsive": true,

        "aoColumns": [
            { "className": "text-center" },                        //*** PAPELETA 
            { "className": "text-center", "bSearchable": false },  //*** CANDIDATO
            { "bSearchable": false },                              //*** NOMBRE 
            {
                "render": function (data) {
                    return "<div><img src='../Images/Candidatos/" + data + "' style='width:50%' /></div>";
                },
                "className": "text-center",
                "bSortable": false,
                "bSearchable": false
            },                                                     //*** FOTO
            {
                "className": "text-center",
                "bSearchable": false
            },                                                     //*** VOTOS
            {
                "className": "text-center text-danger text-bold",
                "bSearchable": false
            }                                                      //*** POSICIÓN
        ]
    });
}
//*** (1.4.1) #6 - Validar Datos Entrada
function validarDatosPantalla() {

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
//*** (1.4.2.2) #7 - Proceso de IniciarCargaInterfaz
function IniciarCargaInterfaz() {
    //*** 7.1 - Lista Papeletas
    obtenerPapeletas();
    //*** 7.2 - Validación de datos
    if (Id_papeleta === undefined) {

        //*** 7.2.1 Mostrar Toast de Carga de Resultados con mensaje de "No hay resultados registrados"
        Toast.fire({
            toast: false,
            type: 'info',
            title: 'CARGA DE RESULTADOS',
            html: '<h5>No hay resultados registrados para la <b>Elección: </b>' + $('#modalEleccion').text() + ' | <b>Ronda : </b>' + $("#modalRonda option:selected").text() + '</h5>',
            allowOutsideClick: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'

        }).then(function (result) {

            if (result.value) {
                //*** Mostrar Modal
                $("#modal_inicial").modal('show');
            }
        });
    } else {
        //*** 7.3 - Si la variable Id_papeleta fue definida, se habilitan los botones
        $('#btn_detalle_resultado').attr("disabled", false);
        $('#btn_descargar_pdf').attr("disabled", false);
        $('.btn-papeletas').attr("disabled", false);

        //*** 7.4 - Iniciar Carga
        iniciarCarga();
    }
}
//*** (7.1) #8 - Cargar Papeletas
function obtenerPapeletas() {

    var DatosPR = {
        "Id_eleccion": Id_eleccion,
        "Id_ronda": Id_ronda
    };
    //*** 8.1 - Cargar lista de papeletas 
    $.ajax({
        type: 'POST',
        async: false,
        url: apiUrl + '/api/Asamblea/PapeletaRonda/ListadoPapeletasR',
        data: JSON.stringify(DatosPR),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga papeletas", mensajeError);
        },
        success: function (data) {

            if (data !== null && data.length > 0) {

                //*** Asignar Papeleta #1
                Id_papeleta = data[0].Id_papeleta;
                Nombre_papeleta = data[0].Id_papeleta + ' - ' + data[0].Nombre;
                //*** Cambiar Título
                $('.card-titulo').html('RESUMEN DE RESULTADOS  ( <b class="text-danger"> ' + data[0].Id_papeleta + ' - ' + data[0].Nombre + ' </b>)');

                var listaPapeletas = [];

                $.each(data, function (index, val) {
                    var myCol = $('<span id="' + val.Id_papeleta + '" class="dropdown-item">' + val.Id_papeleta + ' - ' + val.Nombre + '</span><div class="dropdown-divider"></div>');
                    listaPapeletas.push(myCol);
                });

                $('#listaPapeletas').append(listaPapeletas);

                //*** Btn click Span dinámico
                $('.dropdown-item').click(function (e) {
                    //*** Evitar el PostBack
                    e.preventDefault();
                    //*** Variables
                    var texto = $(this).text();
                    var id = $(this).attr("id");
                    Id_papeleta = id;
                    Nombre_papeleta = texto;
                    //*** Cambiar Título
                    $('.card-titulo').html('RESUMEN DE RESULTADOS  ( <b class="text-danger"> ' + texto + ' </b>)');
                    //*** 8.2 - Iniciar Carga de Resultados
                    iniciarCarga();
                });
            }
        }
    });
}
//*** (7.4 | 8.2 ) # 9 - Iniciar Carga de Graficos
function iniciarCarga() {
    //*** 9.1 - Obtener Resultados
    obtenerResultados();
    //*** 9.2 - Gráficos ( Pie y Barras )
    cargarGraficoBar('ctxGrafico1', 'horizontalBar', datos_Graf1, etiquetas_Graf1);
    cargarGraficoBar('ctxGrafico2', 'pie', datos_Graf2, etiquetas_Graf2);
}
//*** (9.1) #10 - Ejecutar el proceso Ajax de obtenerResultados
function obtenerResultados() {

    var parametros = new Object();

    parametros.Id_eleccion = Id_eleccion;
    parametros.Id_ronda = Id_ronda;
    parametros.Id_papeleta = Id_papeleta;
    parametros.Usuario = Login;

    var data = JSON.stringify({ parametros });

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Resultado/ResultadoResumen',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga de Elecciones", mensajeError);
        },
        success: function (data) {

            if (data === undefined || data === null) {

                Toast.fire({
                    toast: false,
                    type: 'info',
                    title: 'CARGA DE RESULTADOS',
                    html: '<h5>No hay resultados registrados en la base de datos.</h5>',
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
                //*** Llenar arreglos para los gráficos
                //*** Lista de Papeletas
                var totalVotos = 0;
                var votosBlancos = 0;
                //*** 10.1 - Limpiar Graficos
                limpiarGraficos();

                for (var i = 0; i < data.length; i++) {
                    if (data[i].Cedula !== "0") {

                        //*** Votos Totales
                        totalVotos += data[i].Votos;
                        //*** Etiquetas
                        etiquetas_Graf1.push([data[i].Num_posicion] + ' - ' + [data[i].Nombre]);
                        //*** Datos
                        datos_Graf1.push([data[i].Votos]);

                    } else {
                        votosBlancos += data[i].Votos;
                    }
                }
            }
            //*** Valores de Gráfico 2
            etiquetas_Graf2.push('VOTOS EMITIDOS');
            etiquetas_Graf2.push('VOTOS EN BLANCO');
            datos_Graf2.push(totalVotos);
            datos_Graf2.push(votosBlancos);

            //*** Texto de votos Totales
            $('.txt_total_efectivos').html('<i class="fas fa-square text-success mr-2"></i><b>VOTOS EMITIDOS:</b> <b style="font-size: 20px;" class="text-danger ml-2">' + totalVotos + '</b>');
            $('.txt_total_todos').html('<i class="fas fa-square text-success mr-2"></i><b>VOTOS TOTALES:</b> <b style="font-size: 20px;" class="text-danger ml-2">' + (totalVotos + votosBlancos) + '</b>');
        }
    });
}
//*** (10.1) #11 - Limpiar Gráficos
function limpiarGraficos() {
    //*** Limpiar los gráficos
    datos_Graf1.length = 0;
    datos_Graf2.length = 0;
    etiquetas_Graf1.length = 0;
    etiquetas_Graf2.length = 0;
}
//*** (1.8.1) #12 - Cargar Resultados Detalle
function cargarResultadosDetalle() {

    var parametros = new Object();

    parametros.Id_eleccion = Id_eleccion;
    parametros.Id_ronda = Id_ronda;
    parametros.Id_papeleta = 0;
    parametros.Usuario = Login;

    var data = JSON.stringify({ parametros });

    //*** 12.1 - Proceso Ajax de Carga de Resultados
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Resultado/ResultadoResumen',
        data: data,
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': sessionStorage['Cedula'] + ':' + sessionStorage['Token']
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga de Elecciones", mensajeError);
        },
        success: function (data) {

            if (data === undefined || data === null) {

                Toast.fire({
                    toast: false,
                    type: 'info',
                    title: 'CARGA DE RESULTADOS',
                    html: '<h5>No hay resultados registrados en la base de datos.</h5>',
                    allowOutsideClick: true,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Aceptar'

                }).then(function (result) {
                    if (result.value) {
                        //*** Mostrar Modal
                        $("#modal_inicial").modal('show');
                    }
                });

            } else {
                //*** 12.2 -  Agregar Filas a la Tabla
                agregarFilasDT(data);
            }
        }
    });
}
//*** (12.2) #13 - Agregar Filas a la Tabla 
function agregarFilasDT(data) {

    // Limpiar Tabla despues de cualquier cambio 
    objTabla.dataTable().fnClearTable();

    var filas = [];

    for (var i = 0; i < data.length; i++) {

        filas.push([
            data[i].Papeleta,
            data[i].Id_candidato,
            data[i].Nombre,
            data[i].Foto,
            data[i].Votos,
            data[i].Posicion
        ]);
    }

    //*** Agregar Datos
    objTabla.dataTable().fnAddData(filas, false);

    //*** Dibujar la Tabla
    tabla.draw();

}
//*** (9.2) #14 - Cargar Grafico 1
function cargarGraficoBar(id, tipo, datos, etiquetas) {

    //*** Refrescar el Gráfico
    $('#' + id).remove();
    $('.' + id).append('<canvas id="' + id + '" width="400" height="300"></canvas>');

    var ctxGrafico = document.getElementById(id).getContext('2d');
    var myChart = new Chart(ctxGrafico, {
        type: tipo,
        data: {
            labels: etiquetas,
            datasets: [{
                label: 'CANTIDAD DE VOTOS',
                data: datos,
                borderWidth: 2,
                backgroundColor: [
                    'rgba(48, 203, 0, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(48, 203, 0, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ]
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        fontColor: 'black'
                    }
                }]
                , xAxes: [{
                    ticks: {
                        min: 0,
                        stepSize: 1, //*** Aumentos 
                        fontColor: 'black'
                    }
                }]
            }
        }
    });
}



