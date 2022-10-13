<%@ Page Title="Votaciones | COOPECAJA RL" Language="C#" MasterPageFile="~/SGA.Master" AutoEventWireup="true" CodeBehind="Votaciones.aspx.cs" Inherits="VotacionesSGA.Votaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="Content/pluginsSGA_v0006/material-cards-master/format-cards.min.css">
    <link rel="stylesheet" href="Content/pluginsSGA_v0006/material-cards-master/dist/material-cards.min.css">
    <link rel="stylesheet" href="Content/pluginsSGA_v0006/material-cards-master/material-general.min.css">
    <link rel="stylesheet" href="Content/pluginsSGA_v0006/objetos/btn_dinamico.min.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content">
        <!-- /.container-fluid -->
        <div class="container-fluid">
            <div class="card card-primary card-outline">
                <div class="row card-header">
                    <div class="col-sm-12" style="align-self: center;">
                        <p id="card-titulo" class="text-bold card-titulo"></p>
                    </div>
                </div>

                <!-- /.card-body -->
                <div class="card-body">

                    <%--Mobile, Tablet And Desktop--%>
                    <%--Resolución LG: queremos los 4 bloques en fila. Como hay 12 columnas… pues está claro: 3 partes de 4 columnas
                        Resolución MD: queremos 3 bloques por fila: como hay 12 columnas, necesitaremos 4 partes de 3 columnas
                        Resolución SM: queremos 2 bloques por fila: como hay 12 columnas, pondremos 6 partes de 2 columnas
                        Resolución XS: queremos 1 bloque por fila: es decir, una parte de 12 columnas.--%>

                    <div id="containerPanel">
                    </div>
                </div>
                <div class="card-footer">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="Content/pluginsSGA_v0006/material-cards-master/js/jquery.material-cards.min.js"></script>
    <script src="Content/pluginsSGA_v0006/material-cards-master/material-general.min.js"></script>
</asp:Content>
