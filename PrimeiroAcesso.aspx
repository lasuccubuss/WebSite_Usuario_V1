<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrimeiroAcesso.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.PrimeiroAcesso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Primeiro Acesso</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="container mt-5">
    <form id="form1" runat="server">
        <div class="card p-4 shadow-sm">
            <h2 class="mb-4">Primeiro Acesso</h2>

            <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger mb-3" />

            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server" Text="E-mail:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblSenha" runat="server" Text="Nova Senha:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblConfirmaSenha" runat="server" Text="Confirme a Senha:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtConfirmaSenha" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <asp:Button ID="btnSalvar" runat="server" Text="Salvar Senha" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
        </div>
    </form>
</body>
</html>
