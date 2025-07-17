<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MenuPrincipal.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.MenuPrincipal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Menu Principal</title>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }

        .menu-box {
            background-color: #ffffff;
            border-radius: 15px;
            padding: 40px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
            max-width: 600px;
            margin: 0 auto;
        }

        h1 {
            font-size: 2rem;
        }

        .btn {
            margin: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="menu-box text-center">
                <h1>
                    <asp:Label ID="lblBoasVindas" runat="server" Text=""></asp:Label>
                </h1>
                <p class="lead">Você entrou no sistema com sucesso.</p>

                <asp:Button ID="btnLogout" runat="server" Text="Sair" CssClass="btn btn-danger" OnClick="btnLogout_Click" />

                <div class="mt-4">
                    <a href="Usuarios/UsuarioCadastro.aspx" class="btn btn-outline-primary">Cadastrar Usuário</a>
                    <a href="Usuarios/UsuarioLista.aspx" class="btn btn-outline-warning">Lista de Usuários</a>
                    <a href="Documentos/DocumentoCadastro.aspx" class="btn btn-outline-secondary">Gerenciar Documentos</a>
                    <a href="Documentos/DocumentoLista.aspx" class="btn btn-outline-info">Listar Documentos</a>
                    <a href="Relatorios/RelatorioUsuarios.aspx" class="btn btn-outline-success">Ver Relatórios</a>
                    <a href="Uploads/UploadDocumento.aspx" class="btn btn-outline-warning">Upload de Documentos Obrigatórios</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
