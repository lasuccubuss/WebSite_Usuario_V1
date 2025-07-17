<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Relatorios.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.Relatorios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatórios de Usuários</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID= "btnAtivos" runat="server" Text="Usuários Ativos" OnClick="btnUsuariosAtivos_Click" />
            <asp:Button ID= "btnInativos" runat="server" Text="Usuários Inativos" OnClick="btnUsuariosInativos_Click" />
            <asp:Button ID="btnExportarPDFAtivos" runat="server" Text="Exportar Pdf Ativos" OnClick="btnExportarPdfAtivos_Click" />
            <asp:Button ID="btnExportarPDFInativos" runat="server" Text="Exportar Pdf Inativos" OnClick="btnExportarPdfInativos_Click" />
            <asp:Button ID="btnExportarExcelAtivos" runat="server" Text="Exportar Excel Ativos" OnClick="btnExportarExcelAtivos_Click" />
            <asp:Button ID="btnExportarExcelInativos" runat="server" Text="Exportar Excel Inativos" OnClick="btnExportarExcelInativos_Click" />
            <asp:Xml ID="xmlViewer" runat="server" DocumentSource="" TransformSource="" />
        </div>
    </form>
</body>
</html>
