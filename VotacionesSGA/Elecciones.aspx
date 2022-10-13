<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Elecciones.aspx.cs" Inherits="VotacionesSGA.Elecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="content/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <!-- Botones DataTable -->
    <link rel="stylesheet" href="Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">
    <link rel="stylesheet" href="Content/plugins/daterangepicker/daterangepicker.css">
    <link rel="stylesheet" href="Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <link href="https://cdn.jsdelivr.net/gh/gitbrent/bootstrap4-toggle@3.6.1/css/bootstrap4-toggle.min.css" rel="stylesheet">

    <style>
        .dataTable td {
            border-top: 1px solid #acbad5 !important;
            vertical-align: middle !important;
            padding: 1px !important;
        }

        .dataTable tr {
            padding: 1px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--modals--%>
    <%--modal-lg-elecciones--%>
    <div class="modal fade" id="modal-lg-eleccion" style="display: none;" aria-hidden="true" data-tipo="guardar">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="lblEleccion">Nueva Elección</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="id">Id</label>
                        <input type="text" class="form-control" id="id" maxlength="20" disabled>
                    </div>
                    <div class="form-group">
                        <label for="nombre">Nombre</label>
                        <input type="text" class="form-control" id="nombre" placeholder="Ingrese el nombre de la elección" maxlength="50">
                    </div>
                    <div class="form-group">
                        <label for="descripcion">Descripción</label>
                        <input type="text" class="form-control" id="descripcion" placeholder="Ingrese la descripción" maxlength="100">
                    </div>
                    <div class="form-group">
                        <label>Rango Inicio - Finalización:</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="far fa-clock"></i></span>
                            </div>
                            <input type="text" class="form-control float-right" id="dtpRango">
                        </div>
                        <!-- /.input group -->
                    </div>
                    <div class="form-group">
                        <div class="form-group clearfix">
                            <div class="icheck-primary d-inline">
                                <input type="checkbox" id="chkactivo" checked>
                                <label for="chkactivo">Activo</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-group clearfix">
                            <div class="icheck-primary d-inline">
                                <input type="checkbox" id="chkquorum" checked>
                                <label for="chkquorum">Quorum Cerrado</label>
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
    <%--Fin modal-lg-elecciones--%>

    <%--modal-lg-elecciones Ronda--%>
    <div class="modal fade" id="modal-lg-eleccionR" style="display: none;" aria-hidden="true" data-tipo="guardar">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="lblEleccionR">Nueva Ronda de Elección</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="id">Id</label>
                        <input type="text" class="form-control" id="idR" maxlength="5" disabled>
                    </div>
                    <div class="form-group">
                        <label for="nombre">Nombre</label>
                        <input type="text" class="form-control" id="nombreR" placeholder="Ingrese el nombre de la ronda de elección" maxlength="50">
                    </div>
                    <div class="form-group">
                        <label for="descripcion">Descripción</label>
                        <input type="text" class="form-control" id="descripcionR" placeholder="Ingrese la descripción" maxlength="100">
                    </div>
                    <div class="form-group">
                        <label>Rango Inicio - Finalización:</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="far fa-clock"></i></span>
                            </div>
                            <input type="text" class="form-control float-right" id="dtpRangoR">
                        </div>
                        <!-- /.input group -->
                    </div>
                    <div class="form-group">
                        <div class="form-group clearfix">
                            <div class="icheck-primary d-inline">
                                <input type="checkbox" id="chkactivoR" checked>
                                <label for="chkactivoR">Activo</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnGuardarR">Guardar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--Fin modal-lg-elecciones_Ronda--%>

    <%--modal-lg-papeleta Ronda--%>
    <div class="modal fade" id="modal-lg-papeletaR" style="display: none;" aria-hidden="true" data-tipo="guardar">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="lblpapeletaR">Nueva papeleta</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="selPapeleta">Seleccione una Papeleta:</label>
                        <select class="form-control" id="selPapeleta">
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="descripcionPR">Descripción</label>
                        <input type="text" class="form-control" id="descripcionPR" placeholder="Ingrese la descripción de la papeleta" maxlength="100">
                    </div>
                    <div class="form-group">
                        <label for="numVotos">Número Votos</label>
                        <input type="number" class="form-control" id="numVotos" min="0" onkeypress="return isNumber(event)" onpaste="return false" />
                    </div>
                    <div class="form-group">
                        <label for="orden">Orden</label>
                        <input type="number" class="form-control" id="orden" min="0" onkeypress="return isNumber(event)" onpaste="return false" />
                    </div>
                    <div class="form-group">
                        <div class="form-group clearfix">
                            <div class="icheck-primary d-inline">
                                <input type="checkbox" id="chkactivoPR" checked>
                                <label for="chkactivoPR">Activo</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnGuardarPR">Guardar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--Fin modal-lg-elecciones_Ronda--%>

    <%--modal-lg-papeleta Ronda--%>
    <div class="modal fade" id="modal-lg-papeletaC" style="display: none;" aria-hidden="true" data-tipo="guardar">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="lblpapeletaC">Candidatos</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm"></div>
                        <div class="col-sm mx-auto ">
                            <div class="form-group text-center">
                                <input type="checkbox" checked id="chkTipo" data-toggle="toggle" data-on="Mostrar Todos" data-off="Seleccionados" data-onstyle="success" data-offstyle="danger">
                            </div>
                        </div>
                        <div class="col-sm"></div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <table class="table table-bordered tablatipo1" id="tablaPapeletaC">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Id</th>
                                        <th>Cedula</th>
                                        <th>Nombre</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <%--<button type="button" class="btn btn-primary" id="btnGuardarPR">Guardar</button>--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--Fin modal-lg-elecciones_Ronda--%>

    <%--fin modals--%>
    <section class="content">
        <div class="container-fluid">
            <%--Card--%>
            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-12" style="align-self: center;">
                        <p class="text-bold card-titulo">MANTENIMIENTO DE ELECCIONES</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <!-- Contenido Principal -->
                    <section class="content">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- Elecciones -->
                                    <div class="card card-info">
                                        <div class="card-header" data-card-widget="collapse" style="cursor:pointer;">
                                            <h3 class="card-title">Elecciones</h3>

                                            <div class="card-tools">
                                                <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <%--TABLA DE ELECCIONES--%>

                                            <div class="row">
                                                <div class="col-sm">
                                                    <button type="button" class="btn btn-block bg-gradient-primary" id="btnAgregar">Nueva Elección</button>
                                                </div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                            </div>
                                            <div class="table-responsive">
                                                <table class="table table-bordered" id="tablaEleccion">
                                                    <thead>
                                                        <tr>
                                                            <th>Id</th>
                                                            <th>Nombre</th>
                                                            <th>Descripción</th>
                                                            <th>Inicio</th>
                                                            <th>Fin</th>
                                                            <th>Estado</th>
                                                            <th>Quorum</th>
                                                            <th></th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                            <%--FIN TABLA DE ELECCIONES--%>
                                        </div>
                                        <!-- /.card-body -->
                                    </div>
                                    <!-- /.card -->
                                    <!-- Elecciones rondas -->
                                    <div class="card card-info collapsed-card">
                                        <div class="card-header" data-card-widget="collapse" style="cursor:pointer;">
                                            <h3 class="card-title" id="tituloRondaE">Rondas por Elecciones</h3>

                                            <div class="card-tools">
                                                <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <%--TABLA DE RONDAS POR ELECCION--%>

                                            <div class="row">
                                                <div class="col-sm">
                                                    <button type="button" class="btn btn-block bg-gradient-primary" id="btnAgregarR">Nueva Ronda</button>
                                                </div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                            </div>
                                            <div class="table-responsive">
                                                <table class="table table-bordered" id="tablaEleccionR">
                                                    <thead>
                                                        <tr>
                                                            <th>Id</th>
                                                            <th>Nombre</th>
                                                            <th>Descripción</th>
                                                            <th>Inicio</th>
                                                            <th>Fin</th>
                                                            <th>Estado</th>
                                                            <th></th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                            <%--FIN TABLA DE RONDAS POR ELECCION--%>
                                        </div>
                                        <!-- /.card-body -->
                                    </div>
                                    <!-- /.card -->

                                    <!-- Papeletas por ronda -->
                                    <div class="card card-info collapsed-card">
                                        <div class="card-header" data-card-widget="collapse" style="cursor:pointer;">
                                            <h3 class="card-title" id="tituloPapeletaR">Papeletas por ronda</h3>

                                            <div class="card-tools">
                                                <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button>
                                            </div>
                                        </div>
                                        <div class="card-body">

                                            <%--TABLA DE PAPELETAS POR RONDA--%>
                                            <div class="row">
                                                <div class="col-sm">
                                                    <button type="button" class="btn btn-block bg-gradient-primary" id="btnAgregarPR">Nueva Papeleta</button>
                                                </div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                                <div class="col-sm"></div>
                                            </div>
                                            <div class="table-responsive">
                                                <table class="table table-bordered" id="tablaEleccionPR">
                                                    <thead>
                                                        <tr>
                                                            <th>Id</th>
                                                            <th>Nombre</th>
                                                            <th>Descripción</th>
                                                            <th>Votos</th>
                                                            <th>Orden</th>
                                                            <th>Estado</th>
                                                            <th><img src="images/Iconos/reset.png"/></th>
                                                            <th><img src="images/Iconos/candidatos.png" /></th>
                                                            <th><img src="images/Iconos/update.png" /></th>
                                                            <th><img src="images/Iconos/delete.png" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                            <%--FIN TABLA DE TABLA DE PAPELETAS POR RONDA--%>
                                        </div>
                                        <!-- /.card-body -->
                                    </div>




                                    <!-- /.card -->
                                </div>
                                <!-- /.col (RIGHT) -->
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.container-fluid -->






























                    </section>
                    <!-- /.content -->
                </div>
            </div>
            <!-- /.card -->
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

    <!-- Select2 -->
    <script src="Content/plugins/select2/js/select2.full.min.js"></script>
    <!-- InputMask -->
    <script src="Content/plugins/moment/moment.min.js"></script>
    <script src="Content/plugins/inputmask/min/jquery.inputmask.bundle.min.js"></script>
    <!-- date-range-picker -->
    <script src="Content/plugins/daterangepicker/daterangepicker.js"></script>
    <!-- Tempusdominus Bootstrap 4 -->
    <script src="Content/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js"></script>

    <script src="https://cdn.jsdelivr.net/gh/gitbrent/bootstrap4-toggle@3.6.1/js/bootstrap4-toggle.min.js"></script>

    <script src="Content/plugins/datatables/jquery.dataTables.js"></script>
    <script src="Content/plugins/datatables-bs4/js/dataTables.bootstrap4.js"></script>

    <!-- Botones DataTable -->
    <script src="Content/plugins/datatables-buttons/js/dataTables.buttons.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js"></script>
    <script src="Content/plugins/jszip/jszip.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.flash.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.html5.min.js"></script>
    <script src="Content/plugins/datatables-buttons/js/buttons.colVis.min.js"></script>
    <%--Locales--%>
    <script src="Content/scriptsSGA_v0006/js/eleccion.js"></script>

</asp:Content>
