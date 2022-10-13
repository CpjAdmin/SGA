var apiUrl = sessionStorage['Uri'];
var objtabla = $('#tablaBitacora');
var tabla;
var nombreReporte = 'Lista de procesos';
var DatosP = {
    "Id_proceso": 0,
    "Login": '',
    "Nombre_usuario": '',
    "Pagina": '',
    "Descripcion": '',
    "Navegador": '',
    "Login_name": '',
    "Terminal_id": '',
    "Fecha_ejecucion": ''
};


$(document).ready(function () {

    CargarTablaProcesos();
});


function CargarTablaProcesos() {
    MostrarCargando();
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/BitacoraProc/ListadoProc',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("listado de procesos", mensajeError);
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
    console.log(array);
    array.map(function (element) {
        filas.push([
            element.Id_proceso,
            element.Login,
            element.Nombre_usuario,
            element.Pagina,
            element.Descripcion,
            element.Navegador,
            element.Login_name,
            element.Terminal_id,
            element.Fecha_ejecucion
        ]);
    });

    tabla = objtabla.DataTable({
        data: filas,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "Exportar",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                },
                title: function () {
                    return nombreReporte;
                },
                sheetName: 'Reporte'
            }]
        ,
        "columns": [
            { "width": "5%", "className": "textoCentrado" },
            null,
            null,
            null,
            null,
             null,
            null,
            null,
            null
        ],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "pageLength": 20,
        "autoWidth": false,
        "destroy": true,
        "deferRender": true
        //"aaSorting": []
    });
    tabla.draw();

}