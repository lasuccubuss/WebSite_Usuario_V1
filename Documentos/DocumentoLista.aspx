<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DocumentoLista.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.DocumentoLista" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Documentos</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Lista de Documentos</h2>

       <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gvDocumentos_RowCommand">
    <Columns>
        <asp:BoundField DataField="NOME" HeaderText="Nome" />
        <asp:BoundField DataField="TIPO" HeaderText="Tipo" />

        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:LinkButton ID="btnBaixar" runat="server" CommandName="Baixar" CommandArgument='<%# Eval("CAMINHO") %>'>Baixar</asp:LinkButton>
                &nbsp;|&nbsp;
                <asp:HyperLink ID="btnVisualizar" runat="server" NavigateUrl='<%# Eval("CAMINHO") %>' Target="_blank" Text="Visualizar" />
                &nbsp;|&nbsp;
                <asp:LinkButton ID="btnExif" runat="server" CommandName="Exif" CommandArgument='<%# Eval("CAMINHO") %>'>Ver EXIF</asp:LinkButton>
                &nbsp;|&nbsp;
                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("ID") %>'>Editar</asp:LinkButton>
                &nbsp;|&nbsp;
                <asp:LinkButton ID="btnExcluir" runat="server" CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');">Excluir</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<br />
<asp:Label ID="lblMensagem" runat="server" ForeColor="Black" />

    </form>
</body>
</html>
