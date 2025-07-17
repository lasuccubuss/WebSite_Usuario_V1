Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Configuration

Partial Class NovaSenha
    Inherits System.Web.UI.Page

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Dim novaSenha As String = txtNovaSenha.Text.Trim()
        Dim confirmarSenha As String = txtConfirmarSenha.Text.Trim()
        Dim token As String = Request.QueryString("token")

        If String.IsNullOrWhiteSpace(novaSenha) OrElse String.IsNullOrWhiteSpace(confirmarSenha) Then
            lblMensagem.Text = "Preencha todos os campos."
            Return
        End If

        If novaSenha <> confirmarSenha Then
            lblMensagem.Text = "As senhas não coincidem."
            Return
        End If

        If String.IsNullOrWhiteSpace(token) Then
            lblMensagem.Text = "Token inválido ou expirado."
            Return
        End If

        Try
            Dim senhaHash As String = GerarHashSHA256(novaSenha)
            Dim connStr As String = ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString

            Using conexao As New SqlConnection(connStr)
                Dim query As String = "UPDATE USUARIOS SET SENHA_HASH = @SenhaHash, CHAVE_RECUPERACAO = NULL WHERE CHAVE_RECUPERACAO = @Token"

                Using cmd As New SqlCommand(query, conexao)
                    cmd.Parameters.AddWithValue("@SenhaHash", senhaHash)
                    cmd.Parameters.AddWithValue("@Token", token)

                    conexao.Open()
                    Dim linhas = cmd.ExecuteNonQuery()

                    If linhas > 0 Then
                        lblMensagem.CssClass = "text-success"
                        lblMensagem.Text = "Senha redefinida com sucesso!"
                    Else
                        lblMensagem.Text = "Token inválido ou já utilizado."
                    End If
                End Using
            End Using
        Catch ex As Exception
            lblMensagem.Text = "Erro: " & ex.Message
        End Try
    End Sub

    Private Function GerarHashSHA256(senha As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(senha)
            Dim hash = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function
End Class
