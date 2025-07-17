Imports System.Data.SqlClient

Public Class UsuarioLista
    Inherits System.Web.UI.Page

    Private ReadOnly connectionString As String = "Server=srvtrend-02;Database=DES_Laboratorio1;Integrated Security=True;"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarUsuarios()
        End If
    End Sub

    Private Sub CarregarUsuarios()
        Dim query As String = "SELECT ID, NOME, TIPO_USUARIO, STATUS FROM USUARIOS WHERE STATUS <> 9"
        Using conexao As New SqlConnection(connectionString)
            Using comando As New SqlCommand(query, conexao)
                conexao.Open()
                Dim reader As SqlDataReader = comando.ExecuteReader()
                gvUsuarios.DataSource = reader
                gvUsuarios.DataBind()
            End Using
        End Using
    End Sub

    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUsuarios.RowCommand
        Dim id As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Alterar" Then
            Response.Redirect("UsuarioCadastro.aspx?id=" & id)

        ElseIf e.CommandName = "ToggleStatus" Then
            Dim statusAtual As Integer = ObterStatusUsuario(id)
            Dim novoStatus As Integer = If(statusAtual = 1, 0, 1)

            Using conexao As New SqlConnection(connectionString)
                Dim query As String = "UPDATE USUARIOS SET STATUS = @STATUS WHERE ID = @ID"
                Using comando As New SqlCommand(query, conexao)
                    comando.Parameters.AddWithValue("@STATUS", novoStatus)
                    comando.Parameters.AddWithValue("@ID", id)
                    conexao.Open()
                    comando.ExecuteNonQuery()
                End Using
            End Using

            lblMensagem.Text = If(novoStatus = 1, "Usuário reativado com sucesso!", "Usuário desativado com sucesso!")

        ElseIf e.CommandName = "Excluir" Then
            Using conexao As New SqlConnection(connectionString)
                Dim query As String = "UPDATE USUARIOS SET STATUS = 9 WHERE ID = @ID"
                Using comando As New SqlCommand(query, conexao)
                    comando.Parameters.AddWithValue("@ID", id)
                    conexao.Open()
                    comando.ExecuteNonQuery()
                End Using
            End Using

            Response.Write("Tentando excluir o ID: " & id)

            lblMensagem.Text = "Usuário excluído com sucesso!"
        End If

        CarregarUsuarios()
    End Sub

    Private Function ObterStatusUsuario(id As Integer) As Integer
        Dim status As Integer = -1
        Using conexao As New SqlConnection(connectionString)
            Using comando As New SqlCommand("SELECT STATUS FROM USUARIOS WHERE ID = @ID", conexao)
                comando.Parameters.AddWithValue("@ID", id)
                conexao.Open()
                Dim resultado = comando.ExecuteScalar()
                If resultado IsNot Nothing Then
                    status = Convert.ToInt32(resultado)
                End If
            End Using
        End Using
        Return status
    End Function
End Class
