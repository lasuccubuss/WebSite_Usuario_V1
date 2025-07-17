Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports iTextSharp.text
Imports VB.Util
Imports System.IO

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Debug opcional
        End If
    End Sub

    Protected Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        Dim email As String = txtEmail.Text.Trim()
        Dim senha As String = txtSenha.Text.Trim().Normalize()
        Dim senhaHash As String = GerarHashSHA256(senha)

        If String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(senha) Then
            lblMensagem.Text = "Preencha o e-mail e a senha."
            Return
        End If

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString

        Try
            Using conexao As New SqlConnection(connectionString)
                Dim query As String = "SELECT * FROM USUARIOS WHERE EMAIL = @Email AND SENHA_HASH = @SenhaHash AND STATUS = 1"

                Using comando As New SqlCommand(query, conexao)
                    comando.Parameters.AddWithValue("@Email", email)
                    comando.Parameters.AddWithValue("@SenhaHash", senhaHash)

                    conexao.Open()
                    Dim reader As SqlDataReader = comando.ExecuteReader()

                    If reader.Read() Then
                        Dim idUsuario As Integer = Convert.ToInt32(reader("ID"))
                        Session("IDUsuario") = idUsuario
                        Session("NomeUsuario") = reader("NOME").ToString()

                        Dim primeiroAcesso As Boolean = Convert.ToBoolean(reader("PRIMEIRO_ACESSO"))
                        Dim temPendencia As Boolean = UsuarioPossuiPendenciaDocumentos(idUsuario)

                        If primeiroAcesso OrElse temPendencia Then
                            Response.Redirect("~/Uploads/UploadDocumento.aspx")
                        Else
                            Response.Redirect("MenuPrincipal.aspx")
                        End If
                    Else
                        lblMensagem.Text = "Usuário ou senha inválidos."
                    End If
                End Using
            End Using
        Catch ex As Exception
            lblMensagem.Text = "Erro inesperado: " & ex.Message
        End Try
    End Sub

    Private Function UsuarioPossuiPendenciaDocumentos(idUsuario As Integer) As Boolean
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString

        Using conexao As New SqlConnection(connectionString)
            Dim query As String = "SELECT COUNT(*) FROM USUARIO_DOCUMENTO WHERE ID_USUARIO = @IDUsuario"
            Using cmd As New SqlCommand(query, conexao)
                cmd.Parameters.AddWithValue("@IDUsuario", idUsuario)
                conexao.Open()
                Dim total As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return total < 4 ' RG e CPF duas vezes cada = 4 docs esperados
            End Using
        End Using
    End Function

    Private Function GerarHashSHA256(senha As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(senha)
            Dim hash = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function
End Class
