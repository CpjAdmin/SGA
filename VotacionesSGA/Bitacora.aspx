<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="VotacionesSGA.Bitacora" %>
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
                    <div class="col-sm-12" style="align-self: center;">
                        <p class="text-bold card-titulo">BITÁCORA DE PROCESOS DEL SISTEMA</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <%--Tabla--%>
                    <div class="table-responsive">
                        <table class="table table-bordered tablatipo1" id="tablaBitacora" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Proceso</th>
                                    <th>Login</th>
                                    <th>Nombre Usuario</th>
                                    <th>Página</th>
                                    <th>Descripción</th>
                                    <th>Navegador</th>
                                    <th>Usuario BD.</th>
                                    <th>Terminal</th>
                                    <th>Fecha</th>
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
    <%--Locales--%>
    <script src="Content/scriptsSGA_v0006/js/bitacora.min.js"></script>
</asp:Content>
