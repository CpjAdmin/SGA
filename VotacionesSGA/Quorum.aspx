<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Quorum.aspx.cs" Inherits="VotacionesSGA.Quorum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="content/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <!-- Botones DataTable -->
    <link rel="stylesheet" href="Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">
    <style>
        .dataTable td {
            border-top: 1px solid #000000 !important;
            vertical-align: middle !important;
            padding: 1px !important;
        }

        .border-shine {
            border: solid 3px rgba(0, 0, 0, 0.0);
            transition: border-width 0.6s linear;
            animation: 1s shine;
        }

        @keyframes shine {
            0% {
                border: 3px solid rgba(0, 0, 0, 0.3);
            }

            10% {
                border: solid 3px #5A6351
            }

            20% {
                border: 3px solid rgba(0, 0, 0, 0.3);
            }

            30% {
                border: solid 3px #5A6351
            }

            40% {
                border: 3px solid rgba(0, 0, 0, 0.3);
            }

            50% {
                border: solid 3px #5A6351
            }

            60% {
                border: 3px solid rgba(0, 0, 0, 0.3);
            }

            70% {
                border: solid 3px #5A6351
            }

            80% {
                border: 3px solid rgba(0, 0, 0, 0.3);
            }

            90% {
                border: solid 3px #5A6351
            }

            100% {
                border: 3px solid rgba(0, 0, 0, 0.3);
            }
        }

        #card-body-inicial {
            padding-top: 7px;
            padding-bottom: 5px;
        }
        /*MAYOR a 768px*/
        @media (max-width: 768px) {
            #inputIngrso {
                margin-bottom: 10px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="modal fade" id="modal-lg-abri-quorum" style="display: none;" aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="width: 100%;">APERTURA DE QUÓRUM</h4>

                </div>
                <div class="modal-body">
                    <p style="text-align: center; font-size: 1.5rem;">Actualmente no existe un Quórum abierto. ¿Desea abrir el Quórum?</p>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-primary" id="btnAceptar">Aceptar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <section class="content">
        <div class="container-fluid">
            <%--Card--%>
            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-12" style="align-self: center;">
                        <p id="tituloQ" class="text-bold card-titulo"></p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body" style="padding-top: 3px; padding-bottom: 0px;">

                    <%--Fila donde van a estar el boton de ingreso de cedula y cerrar quorum--%>
                    <div class="row">
                        <div class="col">
                            <div class="card ">
                                <div id="card-body-inicial" class="card-body">
                                    <div class="row">
                                        <div class="col-md-4" id="inputIngrso">
                                            <div class="input-group">
                                                <i class="fa fa-2x fa-search-plus"></i>
                                                <input class="form-control" style="box-shadow: 0 0 4px 1px #007bff;" id="cedula" type="number" min="0" max="999999999999999" placeholder="INGRESE LA CÉDULA" title="Dígite la cédula del delegado">
                                            </div>
                                        </div>
                                        <div class="col-md-8 d-flex justify-content-start">
                                            <button type="button" class="btn btn-primary" id="btnRegistrar">Registrar</button>
                                            <button type="button" class="btn bg-gradient-secondary ml-2" id="btnRefrescar"><i class="nav-icon fas fa-redo"></i></button>
                                            <button type="button" class="btn bg-gradient-warning ml-2" id="btnGrafico"><i class="nav-icon fas fa-chart-pie"></i></button>
                                            <button type="button" class="btn btn-danger float-right ml-2" id="btnCerrarQ">Cerrar Quorum</button>
                                        </div>
                                    </div>
                                    <br>
                                    <%--Time Line para ver el total de ingresos vs el total de delegados--%>
                                    <div class="progress-group">
                                        <b style="font-size: large;">INGRESOS || TOTAL EN PADRÓN &nbsp;&nbsp;</b>
                                        <span id="TimeLT" style="font-size: 20px; background-color: #a1e0ea; font-weight: bold; border-radius: 10px;"><b>0</b>&nbsp;|&nbsp; 0</span>
                                        <br />
                                        <div class="progress progress-sm" style="height: 18px;">
                                            <div id="TimeLW" class="progress-bar bg-success progress-bar-striped progress-bar-animated" style="width: 0%"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.card-body -->
                </div>
            </div>
        </div>

        <%--fila donde van a estar los grid de ingresos y salidas--%>
        <div class="row">
            <div class="col-sm-6">
                <div class="card card-info ">
                    <div class="card-header">
                        <h3 class="card-title">INGRESOS</h3>
                        <span id="tingresos" class="badge badge-light float-right" style="font-size: 15px;">0</span>
                    </div>
                    <div id="bIngreso" class="card-body">
                        <div class="table-responsive">
                            <table class="table nowrap table-bordered table-hover tablatipo1" id="tablaingreso" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Cédula</th>
                                        <th>Nombre</th>
                                        <th>Paleta</th>
                                        <th>Fecha Ingreso</th>
                                        <th class="TblDelete"></th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>

                    </div>
                    <!-- /.card-body -->
                </div>
            </div>
            <div class="col-sm-6">
                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title">SALIDAS</h3>
                        <span id="tsalidas" class="badge badge-light float-right" style="font-size: 15px;">0</span>

                    </div>
                    <div id="bSalida" class="card-body">
                        <div class="table-responsive">
                            <table class="table nowrap table-bordered table-hover tablatipo1" id="tablasalida" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Cédula</th>
                                        <th>Nombre</th>
                                        <th>Paleta</th>
                                        <th>Fecha Salida</th>
                                        <th class="TblDelete"></th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                    <!-- /.card-body -->
                </div>
            </div>
        </div>
        <!-- /.card -->
    </section>

    <%--Modal Gráfico--%>
    <div class="modal fade" id="modal_gradico" style="display: none;" aria-hidden="true" data-keyboard="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="lbl_title" style="width: 100%;">ESTADO ACTUAL DE QUORUM</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="">
                            <%--Gráfico--%>
                            <div class="box-body table-responsive">
                                <div class="d-flex flex-row justify-content-end">
                                    <span class="mr-2 txt_total_grafico"></span>
                                </div>
                            </div>
                            <%--Subtitulo Inferior--%>
                            <div class="d-flex flex-row justify-content-end">
                                <span class="mr-2 txt_actualizando"></span>
                            </div>
                        </div>
                    </div>
                    <%--Sección 2--%>
                    <div class="">
                        <div class="card-body ctxGrafico2">
                            <%--Gráfico 2--%>
                            <canvas id="ctxGrafico2" width="400" height="200"></canvas>
                            <%--Subtitulo Inferior--%>
                            <span class="mr-2 txt_total_grafico"></span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="Content/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="Content/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>

    <!-- Botones DataTable -->
    <script src="Content/plugins/datatables-buttons/js/dataTables.buttons.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js"></script>
    <script src="Content/plugins/jszip/jszip.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.flash.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.html5.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.colVis.min.js"></script>
    <%--CharJs--%>
    <script src="Content/plugins/chart.js/Chart.min.js"></script>
    <%--Plugin CharJs--%>
    <script src="Content/plugins/chart-labels/chartjs-plugin-datalabels.min.js"></script>
    <%--Local--%>
    <script src="Content/scriptsSGA_v0006/js/quorum.js"></script>

</asp:Content>
