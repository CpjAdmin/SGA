<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VotacionesSGA.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Votaciones SGA</title>
    <!-- CSS -->
    <link rel="stylesheet" href="assets/font-awesome/css/font-Roboto.min.css" />
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/form-elements.min.css" />
    <link rel="stylesheet" href="assets/css/style.min.css" />
    <link rel="shortcut icon" href="assets/ico/logo-icon.gif" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="Content/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
   
</head>
<body>
    <%--Form--%>
    <form role="form" method="post" class="login-form" runat="server">
        <div class="top-content">
            <div class="inner-bg">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-6 col-sm-offset-3 form-box">
                            <div class="form-top">
                                <div class="form-top-left">
                                    <h3><strong>Asamblea General 2020</strong></h3>
                                    <p style="font-size: 18px;">Ingrese su Cédula y su Clave</p>
                                </div>
                                <div class="form-top-right">
                                    <i class="fa fa-lock"></i>
                                </div>
                            </div>
                            <div class="form-bottom">

                                <div class="form-group">
                                    <label class="sr-only" for="form-username">Cédula</label>
                                    <input type="text" name="form-username" placeholder="Cédula..." class="form-username form-control" id="username" maxlength="20" runat="server" />
                                </div>
                                <div class="form-group">
                                    <label class="sr-only" for="form-password">Clave</label>
                                    <input type="text" name="form-password" placeholder="Clave..." class="form-password form-control" id="pin" maxlength="6" runat="server" />
                                </div>
                                <button type="button" class="btn" id="btnlogin" style="width: 100%;">INGRESAR</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Javascript -->
        <script src="assets/js/jquery-1.11.1.min.js"></script>
        <script src="assets/bootstrap/js/bootstrap.min.js"></script>
        <script src="assets/js/jquery.backstretch.min.js"></script>
        <script src="assets/js/scripts.min.js"></script>
        <!-- SweetAlert2 -->
          <script src="Content/plugins/sweetalert2/sweetalert2.min.js"></script>
        <!-- jQuery SGA-->
        <script src="Content/scriptsSGA_v0006/js/js.cookie.min.js"></script>
        <script src="Content/scriptsSGA_v0006/js/login.js"></script>

    </form>
</body>
</html>
