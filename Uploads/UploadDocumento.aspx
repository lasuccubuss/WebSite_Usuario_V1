<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UploadDocumento.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.UploadDocumento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
<br /><br />

<asp:Repeater ID="rptDocumentos" runat="server">
    <ItemTemplate>
        <div style="margin-bottom: 20px;">
            <strong>
                <asp:Label runat="server" Text='<%# Eval("NOME_DOCUMENTO") %>' />
            </strong>
            <br />
            <asp:FileUpload ID="fuDocumento" runat="server" />
            <asp:HiddenField ID="hfIDDocumento" runat="server" Value='<%# Eval("ID_DOCUMENTO") %>' />
        </div>
    </ItemTemplate>
</asp:Repeater>

<asp:Button ID="btnEnviar" runat="server" Text="Enviar Documentos" OnClick="btnEnviar_Click" />

        </div>
    </form>
</body>
</html>
