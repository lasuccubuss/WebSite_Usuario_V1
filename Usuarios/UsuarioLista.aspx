<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="true" CodeBehind="UsuarioLista.aspx.vb" Inherits="WebSite_UsuarioPortal_V1.UsuarioLista" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Lista de Usuários</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4 text-center">Usuários Cadastrados</h2>

            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False"
                 DataKeyNames="ID"
                 CssClass="table table-striped table-bordered text-center align-middle"
                 OnRowCommand="gvUsuarios_RowCommand">

                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="NOME" HeaderText="Nome" />
                    <asp:BoundField DataField="TIPO_USUARIO" HeaderText="Tipo" />

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <%# If(Convert.ToInt32(Eval("STATUS")) = 1,
                                               "<span class='badge bg-success'>Ativo</span>",
                                               "<span class='badge bg-secondary'>Inativo</span>") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnAlterar" runat="server" CommandName="Alterar"
                                CommandArgument='<%# Eval("ID") %>'
                                Text="Alterar"
                                CssClass="btn btn-sm btn-warning me-2" />

                            <asp:Button ID="btnToggleStatus" runat="server"
                                CommandName="ToggleStatus"
                                CommandArgument='<%# Eval("ID") %>'
                                Text='<%# If(Convert.ToInt32(Eval("STATUS")) = 1, "Desativar", "Reativar") %>'
                                CssClass='<%# If(Convert.ToInt32(Eval("STATUS")) = 1, "btn btn-sm btn-danger me-2", "btn btn-sm btn-success me-2") %>' />

                            <asp:Button ID="btnExcluir" runat="server"
                                CommandName="Excluir"
                                CommandArgument='<%# Eval("ID") %>'
                                Text="Excluir"
                                CssClass="btn btn-sm btn-secondary" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblMensagem" runat="server" CssClass="text-success fw-bold mt-3 d-block" />
        </div>
    </form>
</body>
</html>
