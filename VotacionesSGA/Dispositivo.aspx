<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Dispositivo.aspx.cs" Inherits="VotacionesSGA.Dispositivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="content/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <!-- Botones DataTable -->
    <link rel="stylesheet" href="Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">
    <link rel="stylesheet" href="Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css">

    <style>
        .dataTable td {
            border-top: 1px solid #acbad5 !important;
            vertical-align: middle !important;
            padding: 1px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <%--modal-lg-dispositivo--%>
            <div class="modal fade" id="modal-lg-dispositivo" style="display: none;" aria-hidden="true" data-tipo="guardar">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="lblDispositivo" style="width: 100%;">NUEVO DISPOSITIVO</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="id">IdD</label>
                                <input type="text" class="form-control" id="idD" maxlength="5" disabled>
                            </div>
                            <div class="form-group">
                                <label for="nombre">Nombre</label>
                                <input type="text" class="form-control" id="nombreD" placeholder="Ingrese el nombre del dispositivo" maxlength="50">
                            </div>
                            <div class="form-group">
                                <label for="descripcion">Descripción</label>
                                <input type="text" class="form-control" id="descripcionD" placeholder="Ingrese la descripción del dispositivo" maxlength="100">
                            </div>
                            <div class="form-group">
                                <label for="telefono">Teléfono</label>
                                <input type="text" class="form-control" id="telefonoD" placeholder="Ingrese el telefóno asociado" maxlength="8">
                            </div>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="chkactivoD" checked>
                                        <label for="chkactivoD">Activo</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <button type="button" class="btn btn-primary" id="btnGuardar">Guardar</button>
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
                        <p class="text-bold card-titulo">Dispositivos para Votación</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <%--Tabla--%>
                   
                    <div class="table-responsive">
                        <table class="table table-bordered tablatipo1" id="tablaDispositivos" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Nombre</th>
                                    <th>Descripcion</th>
                                    <th>Teléfono</th>
                                    <th>Estado</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!-- /.card -->

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
     <!-- Locales -->
    <script src="Content/scriptsSGA_v0006/js/dispositivo.min.js"></script>

</asp:Content>
