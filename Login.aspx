<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login - Usuário Portal</title>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/estilo.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <h3 class="text-center mb-4">Login do Sistema</h3>

                    <div class="form-group mb-3">
                        <asp:Label ID="lblEmail" runat="server" Text="E-mail" CssClass="form-label" />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group mb-3">
                        <asp:Label ID="lblSenha" runat="server" Text="Senha" CssClass="form-label" />
                        <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control" />
                    </div>

                    <asp:Button ID="btnEntrar" runat="server" Text="Entrar" CssClass="btn btn-primary w-100" OnClick="btnEntrar_Click" />

                    <div class="text-center mt-2">
                        <a href="RecuperarSenha.aspx">Esqueceu sua senha?</a>
                    </div>

                    <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger mt-3 d-block" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
