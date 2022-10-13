<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="VotacionesSGA.Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="content/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <!-- Botones DataTable -->
    <link rel="stylesheet" href="Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">
    <link rel="stylesheet" href="Content/scriptsSGA_v0006/css/flipclock.min.css">
    <link rel="stylesheet" type="text/css" href="Content/plugins/datatables-rowreorder/css/rowReorder.bootstrap4.min.css" />
    <style>
        .dataTable td {
            border-top: 1px solid #000000 !important;
            vertical-align: middle !important;
            padding: 1px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="container-fluid">
            <%--Card--%>
            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-12" style="align-self: center;">
                        <div id="btnPapeletas" class="btn-group" style="float: right;">
                            <button type="button" class="btn btn-success btn-sm btn-papeletas">Lista de Papeletas</button>
                            <button type="button" class="btn btn-success dropdown-toggle btn-sm btn-papeletas" data-toggle="dropdown" aria-expanded="false">
                                <span class="sr-only"></span>
                                <div class="dropdown-menu dropdown-menu-right" id="listaPapeletas">
                                </div>
                            </button>
                        </div>
                        <p class="text-bold card-titulo" id="lblGestion">USO DE LA PALABRA</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-8">
                            <div class="card card-primary" style="height: 680px; margin-bottom: 10px;">
                                <div class="card-header">
                                    <h3 class="card-title" style="font-size: xx-large;" id="palabra">ESPERANDO...</h3>
                                    <img src="Images/Iconos/timer.png" style="float: right; cursor: pointer; border-color: GOLD; border-style: outset; margin: 5px;" title="Detener contador" id="reset" />

                                </div>
                                <div class="card-body" style="margin: 0px auto;">

                                    <div style="text-align: center;"></div>

                                    <div class="clock-1"></div>

                                    <div class="trafficlight" style="margin: 0px auto;">
                                        <div class="protector"></div>
                                        <div class="protector"></div>
                                        <div class="protector"></div>
                                        <div id="greenC" class="green"></div>
                                        <div id="yellowC" class="yellow"></div>
                                        <div id="redC" class="red"></div>
                                    </div>
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>

                        <div class="col-md-4">
                            <div class="card card-primary" style="height: 680px; margin-bottom: 10px;">
                                <div class="card-header">
                                    <h3 class="card-title" style="font-size: xx-large;">LISTA DE PALETAS</h3>
                                </div>
                                <div class="card-body">
                                    <div class="form-group" id="frmPaleta">
                                        <label for="numPapeleta">Número de paleta</label>
                                        <input type="text" class="form-control" autocomplete="off" id="numPapeleta" onkeypress="return isNumber(event)" onpaste="return false" value="" style="box-shadow: 0 0 5px 2px #3c6ea5;"/>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <input type="button" class="btn btn-success col-md-5" id="btnIniciarT" value="INICIAR">
                                        <input type="button" class="btn btn-danger float-right col-md-5" id="btnFinT" value="FINALIZAR" disabled="disabled">
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table table-bordered tablatipo1" id="tablaGestion" style="width: 100%;">
                                            <thead>
                                                <tr>
                                                    <th style="display: none;">orden</th>
                                                    <th id="orden">Paleta</th>
                                                    <th>Cédula</th>
                                                    <th>Nombre</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.card -->

            <%--modal-lg-tipo-gestion--%>
            <div class="modal fade" id="modal-lg-tipo-gestion" style="display: none;" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="width: 100%;">TIPO DE GESTIÓN</h4>

                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="selTipo">Seleccione el tipo de gestión:</label>
                                <select class="form-control" id="selTipo">
                                    <option value="D">Delegados</option>
                                    <option value="C">Candidatos</option>
                                </select>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <button type="button" class="btn btn-default" id="btnCancelar">Cancelar</button>
                            <button type="button" class="btn btn-primary" id="btnSeleccionar">Aceptar</button>
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

    <script src="Content/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="Content/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>

    <!-- Botones DataTable -->
    <script src="Content/plugins/datatables-buttons/js/dataTables.buttons.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js"></script>
    <script src="Content/plugins/jszip/jszip.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.flash.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.html5.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.colVis.min.js"></script>
    <%--Ordenamiento de filas--%>
    <script type="text/javascript" src="Content/plugins/datatables-rowreorder/js/dataTables.rowReorder.min.js"></script>
    <%--Locales--%>
    <script src="Content/scriptsSGA_v0006/js/gestion.min.js"></script>
    <script src="Content/scriptsSGA_v0006/js/flipclock.min.js"></script>


</asp:Content>
