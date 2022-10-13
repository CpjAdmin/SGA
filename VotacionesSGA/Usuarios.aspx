<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="VotacionesSGA.Usuarios" %>

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

    <section class="content">
        <div class="container-fluid">
            <%--Card--%>

            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-11">
                        <p class="text-bold card-titulo">USUARIOS DEL SISTEMA</p>
                    </div>
                    <div class="col-sm-1">
                        <img id="btnCargar" src="Images/Iconos/txt.png" style="cursor: pointer;" title="cargar usuarios" />
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <%--Tabla--%>
                    <div class="table-responsive">
                        <table class="table table-bordered tablatipo1" id="tablaUsuarios" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>CÉDULA</th>
                                    <th>NOMBRE</th>
                                    <th>ROL</th>
                                    <th>ESTADO</th>
                                    <th>TELÉFONO</th>
                                    <th>ES DELEGADO</th>
                                    <th>ID DELEGADO</th>
                                    <th id="TblUpdate"></th>
                                    <th id="TblDelete"></th>
                                    <th id="TblEnviarPin"></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!-- /.card -->

            <%--modal-Envio--%>
            <div class="modal fade" id="modal-Envio" style="display: none;" aria-hidden="true">
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
                            <div class="form-group row">
                                <div class="col-lg-9">
                                    <label for="telefonoE">Número Telefónico</label>
                                    <input type="text" class="form-control" id="telefonoE" maxlength="8" title="Télefono de Envío ( Requiere 8 Números )" disabled>
                                </div>
                                <div class="col-lg-3" style="align-self: flex-end;">
                                    <div class="icheck-danger d-inline">
                                        <input type="checkbox" id="ckTelefonoE" checked>
                                        <label for="ckTelefonoE"><i class="nav-icon fas fa-3x fa-edit"></i></label>
                                    </div>
                                </div>

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

            <%--modal-lg-usuario--%>
            <div class="modal fade" id="modal-lg-usuario" style="display: none;" aria-hidden="true" data-tipo="guardar">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="lblUsuario" style="width: 100%;">NUEVO USUARIO</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="cedula">Número de cédula</label>
                                <input type="text" class="form-control" id="cedula" placeholder="Ingrese el número de cédula" maxlength="20">
                            </div>
                            <div class="form-group">
                                <label for="nombre">Nombre completo</label>
                                <input type="text" class="form-control" id="nombre" placeholder="Ingrese el nombre completo" maxlength="70">
                            </div>
                            <div class="form-group">
                                <label for="telefono">Número Telefónico</label>
                                <input type="text" class="form-control" id="telefono" placeholder="Ingrese el número telefónico" maxlength="8">
                            </div>
                            <div class="form-group">
                                <label id="lbSelRol" for="selRol">Seleccione un Rol:</label>
                                <select class="form-control" id="selRol">
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="idDelegado">Id Delegado</label>
                                <input type="text" class="form-control" id="idDelegado" disabled>
                            </div>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="chkactivo" checked>
                                        <label for="chkactivo">Activo</label>
                                    </div>
                                </div>
                            </div>
                            <br>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="chkDelegado" checked>
                                        <label for="chkDelegado">Es delegado</label>
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

            <%--modal-lg-masivo--%>
            <div class="modal fade" id="modal-lg-masivo" style="display: none;" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="width: 100%;">ENVÍO MASIVO DE PIN</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="selRolM">Solo el siguiente rol:</label>
                                <select class="form-control" id="selRolM">
                                </select>
                            </div>
                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline" style="margin-right: 26px;">
                                        <input type="radio" id="radioE1" name="optestado" checked="" value="A">
                                        <label for="radioE1">Usuarios Activos</label>
                                    </div>
                                    <div class="icheck-primary d-inline">
                                        <input type="radio" id="radioE2" name="optestado" value="I">
                                        <label for="radioE2">Usuarios inactivos</label>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline" style="margin-right: 10px;">
                                        <input type="radio" id="radioD1" name="optDelegados" checked="" value="S">
                                        <label for="radioD1">Usuarios delegados</label>
                                    </div>
                                    <div class="icheck-primary d-inline">
                                        <input type="radio" id="radioD2" name="optDelegados" value="N">
                                        <label for="radioD2">Usuarios no delegados</label>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="form-group clearfix">
                                    <div class="icheck-primary d-inline">
                                        <input type="checkbox" id="ckCambiaPinM">
                                        <label for="ckCambiaPinM">Resetear Pin</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <button type="button" class="btn btn-primary" id="btnEnviarM">Generar proceso</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </div>

        <%--Input FIle - Carga de Usuarios--%>
        <input type="file" id="Miarchivo" name="myfile" style="display: none" accept=".txt">
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
    <script src="Content/scriptsSGA_v0006/js/usuario.min.js"></script>

</asp:Content>
