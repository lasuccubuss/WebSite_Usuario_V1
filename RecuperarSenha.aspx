<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RecuperarSenha.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.RecuperarSenha" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Recuperar Senha</title>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-5">
                    <h3 class="text-center mb-4">Recuperar Senha</h3>

                    <div class="form-group mb-3">
                        <asp:Label ID="lblEmail" runat="server" Text="Informe seu e-mail" CssClass="form-label" />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>

                    <asp:Button ID="btnRecuperar" runat="server" Text="Enviar chave de recuperação" CssClass="btn btn-warning w-100" OnClick="btnRecuperar_Click" />
                    
                    <asp:Label ID="lblMensagem" runat="server" CssClass="text-success mt-3 d-block" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
