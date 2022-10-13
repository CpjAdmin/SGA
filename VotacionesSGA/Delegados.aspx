<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Delegados.aspx.cs" Inherits="VotacionesSGA.Delegados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="content/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <!-- Botones DataTable -->
    <link rel="stylesheet" href="Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">
    <link rel="stylesheet" href="Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css">

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
                    <div class="col-sm-11" style="align-self: center;">
                        <p class="text-bold card-titulo">DELEGADOS</p>
                    </div>
                    <div class="col-sm-1">
                        <img id="btnCargar" src="Images/Iconos/txt.png" style="cursor: pointer;" title="Cargar Delegados" />
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <%--Tabla--%>
                    <div class="table-responsive">
                        <table class="table table-bordered tablatipo1" id="tablaDelegados" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Cédula</th>
                                    <th>Nombre</th>
                                    <th>Paleta</th>
                                    <th>Centro</th>
                                    <th>Institucion</th>
                                    <th>Lugar Trabajo</th>
                                    <th>Celular</th>
                                    <th>Email</th>
                                    <th>Foto</th>
                                    <th>Votar</th>
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


            <%--modal-lg-Delegado--%>
            <div class="modal fade" id="modal-lg-Delegado" style="display: none;" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="lblDelegado">Modificar Datos</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="id">Id</label>
                                <input type="text" class="form-control" id="id" disabled>
                            </div>
                            <div class="form-group">
                                <label for="cedula">Número de cédula</label>
                                <input type="text" class="form-control" id="cedula" placeholder="Ingrese el número de cédula" maxlength="20">
                            </div>
                            <div class="form-group">
                                <label for="nombre">Nombre completo</label>
                                <input type="text" class="form-control" id="nombre" placeholder="Ingrese el nombre completo" maxlength="70">
                            </div>
                            <div class="form-group">
                                <label for="numPaleta">Número de Paleta</label>
                                <input type="number" class="form-control" id="numPaleta" min="0" onkeypress="return isNumber(event)" onpaste="return false" />
                            </div>
                            <div class="form-group">
                                <label for="centro">Centro de Trabajo</label>
                                <input type="text" class="form-control" id="centro" placeholder="Ingrese el centro de trabajo" maxlength="50">
                            </div>
                            <div class="form-group">
                                <label for="institucion">Institución de Trabajo</label>
                                <input type="text" class="form-control" id="institucion" placeholder="Ingrese la institución de trabajo" maxlength="50">
                            </div>
                            <div class="form-group">
                                <label for="lugar_trabajo">Lugar de Trabajo</label>
                                <input type="text" class="form-control" id="lugar_trabajo" placeholder="Ingrese el lugar de trabajo" maxlength="50">
                            </div>
                            <div class="form-group">
                                <label for="celular">Teléfono Celular</label>
                                <input type="text" class="form-control" id="celular" placeholder="Ingrese el teléfono celular" maxlength="10" onkeypress="return isNumber(event)" onpaste="return false">
                            </div>
                            <div class="form-group">
                                <label for="email">Email</label>
                                <input type="text" class="form-control" id="email" placeholder="Ingrese el email" maxlength="50">
                            </div>
                            <div class="form-group">
                                <label for="foto">Nombre Fotografía</label>
                                <input type="text" class="form-control" id="foto" placeholder="Ingrese el nombre de la fotografía" maxlength="50">
                            </div>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="chkvotar" checked>
                                        <label for="chkvotar">Puede Votar</label>
                                    </div>
                                </div>
                            </div>

                            <br>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="chkactivo" checked>
                                        <label for="chkactivo">Activo</label>
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
        </div>
        <input type="file" id="Miarchivo" name="myfile" style="visibility: hidden;" accept=".txt">
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
    <script src="Content/scriptsSGA_v0006/js/delegado.min.js"></script>

</asp:Content>
