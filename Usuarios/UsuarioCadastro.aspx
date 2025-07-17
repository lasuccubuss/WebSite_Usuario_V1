<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UsuarioCadastro.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.UsuarioCadastro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cadastro de Usuário</title>
    <link href="../Scripts/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4 text-center">Cadastro de Usuário</h2>
            <div class="row mb-3">
                <div class="col">
                    <asp:Label ID="lblNome" runat="server" Text="Nome" CssClass="form-label" />
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
                </div>
                <div class="col">
                    <asp:Label ID="lblEmail" runat="server" Text="E-mail" CssClass="form-label" />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="row mb-3">
                <div class="col">
                    <asp:Label ID="lblSenha" runat="server" Text="Senha" CssClass="form-label" />
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="col">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo de Usuário" CssClass="form-label" />
                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Administrador" Value="ADM" />
                        <asp:ListItem Text="Operador" Value="OPE" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col">
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento" CssClass="form-label" />
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                </div>
                <div class="col">
                    <asp:Label ID="lblTipoDoc" runat="server" Text="Tipo de Documento" CssClass="form-label" />
                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control">
                        <asp:ListItem Text="CPF" Value="CPF" />
                        <asp:ListItem Text="CNPJ" Value="CNPJ" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-check mb-3">
                <asp:CheckBox ID="chkAtivo" runat="server" CssClass="form-check-input" Checked="true" />
                <asp:Label ID="lblAtivo" runat="server" Text="Ativo" AssociatedControlID="chkAtivo" CssClass="form-check-label" />
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-success" OnClick="btnSalvar_Click" />
            <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger mt-3 d-block" />
            <div class="mb-3">
            <asp:Label ID="lblTipoEntidade" runat="server" Text="Tipo de Entidade:" CssClass="form-label" />
            <asp:DropDownList ID="ddlTipoEntidade" runat="server" CssClass="form-control" />
            </div>
        </div>
    </form>
</body>
</html>
