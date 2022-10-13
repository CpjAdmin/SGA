<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="VotacionesSGA.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTable -->
    <link rel="stylesheet" href="content/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <!-- Botones DataTable -->
    <link rel="stylesheet" href="Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">

    <style>
        .dataTable td {
            border-top: 1px solid #acbad5 !important;
            vertical-align: middle !important;
            padding: 3px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content">
        <div class="container-fluid">
            <%--Card--%>
            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-12">
                        <p class="text-bold card-titulo">REPORTES DEL SISTEMA</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <%--Tabla--%>
                    <div class="table-responsive">
                        <table class="table table-bordered" id="tbl_tabla" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Cod_alterno</th>
                                    <th>Nombre</th>
                                    <th>Archivo_rpt</th>
                                    <th>Ubicacion_ssrs</th>
                                    <th>Descripcion</th>
                                    <th>Parametros</th>
                                    <th>Estado</th>
                                    <th>Modulo</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!-- /.card -->
            <%--modal-Envio--%>
            <div class="modal fade" id="modal-reporte" style="display: none;" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="width: 100%;">ENVÍO DE PIN</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="cedulaE">Cédula del Usuario</label>
                                <input type="text" class="form-control" id="cedulaE" disabled>
                            </div>
                            <div class="form-group">
                                <label for="nombreE">Nombre</label>
                                <input type="text" class="form-control" id="nombreE" disabled>
                            </div>
                            <div class="form-group">
                                <label for="telefonoE">Número Telefónico</label>
                                <input type="text" class="form-control" id="telefonoE" disabled>
                            </div>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="ckCambiaPin">
                                        <label for="ckCambiaPin">Resetear Pin</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="ckEnviaD">
                                        <label for="ckEnviaD">Enviar a Dispositivo</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="frmDispositivos">
                                <label for="selDispositivo">Seleccione un Dispositivo:</label>
                                <select class="form-control" id="selDispositivo">
                                </select>
                            </div>



                        </div>
                        <div class="modal-footer justify-content-between">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <button type="button" class="btn btn-primary" id="btnEnviar">Enviar</button>
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
    <%--Locales--%>
    <script src="Content/scriptsSGA_v0006/js/reportes.min.js"></script>

</asp:Content>
