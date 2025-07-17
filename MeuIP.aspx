<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MeuIP.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.MeuIP" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ver Meu IP</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial; font-size: 18px; margin: 20px;">
            Seu IP é: <asp:Label ID="lblIP" runat="server" ForeColor="Blue" Font-Bold="True"></asp:Label>
        </div>
    </form>
</body>
</html>
