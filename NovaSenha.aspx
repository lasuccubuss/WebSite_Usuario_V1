<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NovaSenha.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.NovaSenha" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Nova Senha</title>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <h3 class="text-center mb-4">Definir Nova Senha</h3>

                <asp:Label runat="server" Text="Nova senha:" CssClass="form-label" />
                <asp:TextBox ID="txtNovaSenha" runat="server" TextMode="Password" CssClass="form-control mb-3" />

                <asp:Label runat="server" Text="Confirmar senha:" CssClass="form-label" />
                <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password" CssClass="form-control mb-3" />

                <asp:Button ID="btnSalvar" runat="server" Text="Salvar Senha" CssClass="btn btn-success w-100" OnClick="btnSalvar_Click" />

                <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger mt-3 d-block" />
            </div>
        </div>
    </form>
</body>
</html>
