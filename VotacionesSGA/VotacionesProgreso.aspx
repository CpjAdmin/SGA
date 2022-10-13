<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="VotacionesProgreso.aspx.cs" Inherits="VotacionesSGA.VotacionesProgreso" %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content">
        <!-- /.container-fluid -->
        <div class="container-fluid">
            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-12" style="align-self: center;">
                        <p class="text-bold card-titulo">PROGRESO DE VOTACIONES</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <div class="box-body table-responsive">
                        <table id="tbl_tabla" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>DELEGADO</th>
                                    <th>LOGIN</th>
                                    <th>NOMBRE</th>
                                    <th>TELEFONO</th>
                                    <th>PAPELETAS</th>
                                    <th>VOTOS</th>
                                    <th>FECHA INICIO</th>
                                    <th>FECHA FIN</th>
                                    <th>DURACION</th>
                                    <th>PROGRESO</th>
                                    <th>SESIONES</th>
                                </tr>
                            </thead>
                            <tbody id="tbl_body_table">
                                <%-- Datos por AJAX--%>
                            </tbody>
                        </table>
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
                        <h4 class="modal-title" id="lblUsuario" style="width: 100%;">VOTACIÓN EN PROGRESO</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-inline">
                            <div class="form-group" style="padding-left: 10px;">
                                <label class="text-danger mr-2">SEGUNDOS PARA ACTUALIZAR:</label>
                                <input type="number" class="form-control" id="modalSegundos" value="5" min="5" max="60" style="text-align: center; font-weight: bold; width: 70px">
                            </div>
                        </div>
                        <br>
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
    <!-- Página -->
    <script src="Content/scriptsSGA_v0006/js/votacion_progreso.min.js"></script>

</asp:Content>
