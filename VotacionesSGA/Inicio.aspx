<%@ Page Title="" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="VotacionesSGA.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .disabledMenu{
            pointer-events:none;
            opacity:0.4;
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
                        <p class="text-bold card-titulo">SISTEMA DE VOTACIONES SGA</p>
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-body">
                    <div class="row" id="eleccionRondas">
                       <%--Rondas de las Elecciones--%>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

    <%--Locales--%>
    <script src="Content/scriptsSGA_v0006/js/inicio.min.js"></script>

</asp:Content>
