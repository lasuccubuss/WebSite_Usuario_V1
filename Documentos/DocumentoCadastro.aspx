<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DocumentoCadastro.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.DocumentoCadastro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Documentos</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Cadastro de Documentos</h2>

        <asp:Label ID="lblNome" runat="server" Text="Nome do Documento: " />
        <asp:TextBox ID="txtNome" runat="server" /><br /><br />

        <asp:Label ID="lblDescricao" runat="server" Text="Descrição: " />
        <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Rows="3" /><br /><br />

        <asp:Label ID="lblTipo" runat="server" Text="Tipo: " />
        <asp:DropDownList ID="ddlTipo" runat="server">
            <asp:ListItem Text="PDF" Value="pdf" />
            <asp:ListItem Text="Imagem" Value="img" />
            <asp:ListItem Text="Word" Value="docx" />
        </asp:DropDownList><br /><br />

        <asp:Label ID="lblArquivo" runat="server" Text="Selecionar Arquivo: " />
        <asp:FileUpload ID="fuArquivo" runat="server" /><br /><br />

        <asp:Button ID="btnSalvar" runat="server" Text="Salvar Documento" OnClick="btnSalvar_Click" />

        <br /><br />
        <asp:Label ID="lblMensagem" runat="server" ForeColor="Green" />
    </form>
</body>
</html>
