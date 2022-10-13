const Toast = Swal.mixin({
    position: 'center'
});

var tabla, dataReportes;
var objTabla = $('#tbl_tabla');
var refresh_Interval_TablaId;

var apiUrl = sessionStorage['Uri'];
var Login;

//@Variables globales
var auxReporte = null;
var parametrosAux = [];
var reporteEjecutado = null;
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

    //*** Obtener Parametros iniciales
    Login = sessionStorage.getItem('Cedula');
    //*** 1.1 - Cargar Elecciones
    iniciarDataTable(obtenerReportes);
    //*** 1.2 - Llamar Reporte
    $(document).on('click', '.tooltipImgM', function () {

        //*** Datos del Reporte
        var archivoRpt = $(this).closest("tr").find('td:eq(3)').text();
        var path = $(this).closest("tr").find('td:eq(4)').text();
        //*** Abrir Reporte
        window.open('ReportViewer.aspx?urpt=' + Login + "&nrpt=" + archivoRpt + '&irpt=' + path, '_blank');

    });
});
//*** (1.1) #2 - Iniciar Tabla 
function iniciarDataTable(CallbackFN) {

    tabla = objTabla.DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "EXCEL",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8],
                    stripHtml: false
                },
                title: "Lista de Reportes",
                sheetName: 'Reportes',
                className: 'btn-success mr-2'
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

        //"aaSorting": [[0, 'asc'], [5, 'asc']],
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
            { "className": "text-center text-danger text-bold" }, //*** ID REPORTE 
            { "className": "text-center", "width": "8%" },        //*** COD ALTERNO 
            { "width": "12%" },                                   //*** NOMBRE
            { "width": "12%" },                                   //*** RPT
            { "width": "18%" },                                   //*** UBICACION   
            null,                                                 //*** DESCRIPCION
            null,                                                 //*** PARAMETROS
            { "className": "text-center" },                       //*** ESTADO
            { "className": "text-center", "width": "7%" },                       //*** MODULO
            { "className": "text-center", "sortable": false, "width": "5%" }     //*** EJECUTAR
        ]
    });

    //*** Cargar el DataTable con datos
    setTimeout(function () {
        CallbackFN();
    }, 500);
}
//*** #2 Obtener Reportes
function obtenerReportes() {

    var parametros = new Object();

    parametros.Usuario = Login;

    var data = JSON.stringify({ parametros });

    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Reportes/ListaReportes',
        data: data,
        async: true,
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga de Reportes", mensajeError);
        },
        success: function (data) {
            if (data.length === 0) {

                //*** Mostrar mensaje de Carga de Registros = 0
                Toast.fire({
                    toast: false,
                    type: 'info',
                    title: 'CARGA DE REPORTES',
                    html: '<h5>No existen reportes en la base de datos</h5>',
                    allowOutsideClick: true,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Aceptar'

                }).then(function (result) {

                });

            } else {

                //*** Total de Reportes
                var totalReportes = data.length;
                //*** Texto Totales
                $('.card-titulo').html('<i class="fas fa-square text-success mr-2"></i><b>REPORTES DEL SISTEMA  ( TOTAL : </b> <b style="font-size: 20px;" class="text-danger ml-2">' + totalReportes + '</b> )');
                //*** 2.1 - Agregar FIlas a la Tabla
                agregarFilasDT(data);
            }
        }
    });
}
//*** (2.1) #3 Agregar Filas a la Tabla 
function agregarFilasDT(data) {

    //*** 3.1 - Limpiar Tabla despues de cualquier cambio 
    objTabla.dataTable().fnClearTable();

    var filas = [];
    //*** 3.2 - Cargar filas de datos
    for (var i = 0; i < data.length; i++) {

        filas.push([
            data[i].Id_reporte,
            data[i].Cod_alterno,
            data[i].Nombre,
            data[i].Archivo_rpt,
            data[i].Ubicacion_ssrs,
            data[i].Descripcion,
            data[i].Parametros,
            spanEstado(data[i].I_estado),
            data[i].Id_modulo,
            validarEjecutar(data[i].I_estado, data[i].Id_reporte)
        ]);
    }

    //*** 7.2 - Agregar Datos
    objTabla.dataTable().fnAddData(filas, false);

    //***7.3 - Dibujar la Tabla
    tabla.draw();

    //***7.4 - Crear botones adicionales
    if ($('#btnRefrescar').length === 0) {

        var btnRefrescar = '<button type="button" class="btn  bg-gradient-secondary" id="btnRefrescar" style="margin-left:8px;">Refrescar</button>';
        $('.buttons-excel').after(btnRefrescar);

        //*** 7.4.1 - Botón Aceptar del Modal
        $(document).on('click', '#btnRefrescar', function () {
            location.reload(true);
        });
    }
}
//*** (3.2) #4 Función Condicional de Progreso
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
function validarEjecutar(estado, id_reporte) {

    switch (estado) {
        case 'A':
            return '<img id=' + id_reporte + ' class="tooltipImgM dt-fondo-circular-verde" src="images/Iconos/send.png" style="cursor:pointer" data-toggle="tooltip" title="Ejecutar Reporte" />';
        case 'I':
            return '<span class="fa fa-lock text-danger" title="Reporte Bloqueado"></span>';
        default:
            return '';
    }
}

