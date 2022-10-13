<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="ResultadosResumen.aspx.cs" Inherits="VotacionesSGA.ResultadosResumen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="content/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <!-- Botones DataTable -->
    <link rel="stylesheet" href="Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">

    <style>
        .dataTable td {
            border-top: 1px solid #acbad5 !important;
            vertical-align: middle !important;
            padding: 1px !important;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content">
        <!-- /.container-fluid -->
        <div class="container-fluid">
            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-12" style="align-self: center;">
                        <p class="text-bold card-titulo">RESUMEN DE RESULTADOS</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <div class="row">
                        <%--Sección 1--%>
                        <div class="col-sm-6">
                            <div class="card">
                                <div class="card-header border-0">
                                    <div class="d-flex justify-content-between">
                                        <h3 class="card-title text-bold">GRÁFICO DE RESULTADOS</h3>
                                        <div class="btn-group">
                                            <button type="button" class="btn  bg-gradient-secondary btn-sm" id="btnRefrescar" style="margin-right:8px;">Refrescar</button>
                                            <button type="button" class="btn btn-success btn-sm btn-papeletas">Lista de Papeletas</button>
                                            <button type="button" class="btn btn-success dropdown-toggle btn-sm btn-papeletas" data-toggle="dropdown" aria-expanded="false">
                                                <span class="sr-only"></span>
                                                <div class="dropdown-menu" id="listaPapeletas">
                                                </div>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body ctxGrafico1">
                                    <%--Gráfico 1--%>
                                    <canvas id="ctxGrafico1" width="400" height="300"></canvas>
                                    <%--Subtitulo Inferior--%>
                                    <div class="d-flex flex-row justify-content-end">
                                        <span class="mr-2 txt_total_efectivos"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--Sección 2--%>
                        <div class="col-sm-6">
                            <div class="card">
                                <div class="card-header border-0">
                                    <div class="d-flex justify-content-between">
                                        <h3 class="card-title text-bold">TOTAL DE VOTOS</h3>
                                        <div>
                                        <button type="button" class="btn btn-danger btn-sm" id="btn_descargar_pdf">Descargar PDF</button>
                                        <button type="button" class="btn btn-primary btn-sm" id="btn_detalle_resultado">Ver Detalle</button>
                                            </div>
                                    </div>
                                </div>
                                <div class="card-body ctxGrafico2">
                                    <%--Gráfico 2--%>
                                    <canvas id="ctxGrafico2" width="400" height="300"></canvas>
                                    <%--Subtitulo Inferior--%>
                                    <div class="d-flex flex-row justify-content-end">
                                        <span class="mr-2 txt_total_todos"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </div>

        <%--Modal Inicial--%>
        <div class="modal fade" id="modal_inicial" style="display: none;" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="lblUsuario" style="width: 100%;">RESUMEN DE RESULTADOS</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="modalEleccion">NOMBRE DE ELECCIÓN:</label>
                            <select class="form-control" id="modalEleccion" onchange="obtenerRondas();">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="modalRonda">NOMBRE DE RONDA:</label>
                            <select class="form-control" id="modalRonda">
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer justify-content-between">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" id="btnGuardar">Aceptar</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <%--Modal Detalle--%>
        <div class="modal fade" id="modal_detalle" style="display: none;" aria-hidden="true" data-keyboard="false">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="lbl_title" style="width: 100%;">DETALLE DE RESULTADOS</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <%--Gráfico--%>
                                <div class="box-body table-responsive">
                                    <table id="tbl_tabla" class="table table-bordered table-hover nowrap" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>PAPELETA</th>
                                                <th>CANDIDATO</th>
                                                <th>NOMBRE</th>
                                                <th>FOTO</th>
                                                <th>VOTOS</th>
                                                <th>POSICIÓN</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbl_body_table">
                                            <%-- Datos por AJAX--%>
                                        </tbody>
                                    </table>
                                </div>
                                <%--Subtitulo Inferior--%>
                                <div class="d-flex flex-row justify-content-end">
                                    <span class="mr-2 txt_actualizando"></span>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </div>

    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

    <!-- DataTable -->
    <script src="Content/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="Content/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/dataTables.buttons.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js"></script>
    <script src="Content/plugins/jszip/jszip.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.flash.min.js"></script>
    <script src="Content/plugins/pdfmake/pdfmake.min.js"></script>
    <script src="Content/plugins/pdfmake/vfs_fonts.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.html5.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.colVis.min.js"></script>
    <%--CharJs--%>
    <script src="Content/plugins/chart.js/Chart.min.js"></script>
    <%--Plugin CharJs--%>
    <script src="Content/plugins/chart-labels/chartjs-plugin-datalabels.min.js"></script>
    <!-- Página -->
    <script src="Content/scriptsSGA_v0006/js/resultado_resumen.min.js"></script>

    <script src="Content/plugins/export-pdf/dom-to-image.min.js"></script>
    <script src="Content/plugins/export-pdf/jspdf.min.js"></script>

</asp:Content>
