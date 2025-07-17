Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.Text

Partial Class RecuperarSenha
    Inherits System.Web.UI.Page

    Protected Sub btnRecuperar_Click(sender As Object, e As EventArgs) Handles btnRecuperar.Click
        Dim email As String = txtEmail.Text.Trim()

        If String.IsNullOrWhiteSpace(email) Then
            lblMensagem.Text = "Informe seu e-mail."
            lblMensagem.CssClass = "text-danger"
            Return
        End If

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString

        Try
            Dim token As String = Guid.NewGuid().ToString()
            Dim nomeUsuario As String = ""

            Using conexao As New SqlConnection(connectionString)
                Dim query As String = "UPDATE USUARIOS SET CHAVE_RECUPERACAO = @Token WHERE EMAIL = @Email"

                Using cmd As New SqlCommand(query, conexao)
                    cmd.Parameters.AddWithValue("@Token", token)
                    cmd.Parameters.AddWithValue("@Email", email)

                    conexao.Open()
                    Dim linhasAfetadas = cmd.ExecuteNonQuery()

                    If linhasAfetadas = 0 Then
                        lblMensagem.Text = "E-mail não encontrado."
                        lblMensagem.CssClass = "text-danger"
                        Return
                    End If
                End Using
            End Using

            ' Envia o e-mail com o link de recuperação
            EnviarEmailRecuperacao(email, token)

            lblMensagem.Text = "E-mail enviado com instruções para redefinir sua senha."
            lblMensagem.CssClass = "text-success"
        Catch ex As Exception
            lblMensagem.Text = "Erro ao processar: " & ex.Message
            lblMensagem.CssClass = "text-danger"
        End Try
    End Sub

    Private Sub EnviarEmailRecuperacao(email As String, token As String)
        Dim linkRecuperacao As String = $"http://localhost:44340/NovaSenha.aspx?token={token}"
        Dim mensagem As New MailMessage("garciasusana21@gmail.com", email)
        mensagem.Subject = "Recuperação de Senha - Usuário Portal"
        mensagem.Body = $"Olá! Você solicitou a recuperação de senha. Clique no link abaixo para criar uma nova senha:" & vbCrLf & vbCrLf & linkRecuperacao
        mensagem.IsBodyHtml = False

        Dim smtp As New SmtpClient("smtp.gmail.com")
        smtp.Port = 587
        smtp.Credentials = New System.Net.NetworkCredential("garciasusana21@gmail.com", "zdovqbejmtxacwuf")
        smtp.EnableSsl = True
        smtp.Send(mensagem)
    End Sub
End Class
