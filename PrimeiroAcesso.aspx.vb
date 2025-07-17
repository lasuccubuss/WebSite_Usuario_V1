Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Partial Class PrimeiroAcesso
    Inherits System.Web.UI.Page

    Dim conexao As New SqlConnection("Data Source=SRVTREND-02;Initial Catalog=DES_Laboratorio1;Integrated Security=True")

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim email As String = Request.QueryString("email")
            If Not String.IsNullOrEmpty(email) Then
                txtEmail.Text = email

                ' Verifica se existe no banco
                Dim cmdCheck As New SqlCommand("SELECT COUNT(*) FROM USUARIOS WHERE EMAIL = @EMAIL", conexao)
                cmdCheck.Parameters.AddWithValue("@EMAIL", email)

                conexao.Open()
                Dim existe As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                conexao.Close()

                If existe = 0 Then
                    lblMensagem.Text = "Usuário não encontrado."
                    btnSalvar.Enabled = False
                End If
            Else
                lblMensagem.Text = "Link inválido ou incompleto."
                btnSalvar.Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs)
        Dim email As String = txtEmail.Text.Trim()
        Dim senha As String = txtSenha.Text.Trim()
        Dim confirma As String = txtConfirmaSenha.Text.Trim()

        If senha <> confirma Then
            lblMensagem.Text = "As senhas não coincidem."
            Return
        End If

        If senha.Length < 6 Then
            lblMensagem.Text = "A senha deve ter no mínimo 6 caracteres."
            Return
        End If

        Try
            Dim hashSenha As String = GerarHashSHA256(senha)
            Dim cmd As New SqlCommand("UPDATE USUARIOS SET SENHA_HASH = @SENHA_HASH, PRIMEIRO_ACESSO = 0, DATA_ULTIMO_ACESSO = GETDATE() WHERE EMAIL = @EMAIL", conexao)
            cmd.Parameters.AddWithValue("@SENHA_HASH", hashSenha)
            cmd.Parameters.AddWithValue("@EMAIL", email)

            conexao.Open()
            Dim linhasAfetadas As Integer = cmd.ExecuteNonQuery()
            conexao.Close()

            If linhasAfetadas > 0 Then
                lblMensagem.ForeColor = Drawing.Color.Green
                lblMensagem.Text = "Senha definida com sucesso! Redirecionando..."
                btnSalvar.Enabled = False
                Response.AddHeader("REFRESH", "3;URL=Login.aspx")
            Else
                lblMensagem.Text = "Erro ao salvar senha. Verifique o link ou tente novamente."
            End If

        Catch ex As Exception
            conexao.Close()
            lblMensagem.Text = "Erro inesperado: " & ex.Message
        End Try
    End Sub

    Private Function GerarHashSHA256(valor As String) As String
        Dim sha256 As SHA256 = SHA256.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(valor)
        Dim hash As Byte() = sha256.ComputeHash(bytes)

        Dim sb As New StringBuilder()
        For Each b As Byte In hash
            sb.Append(b.ToString("x2"))
        Next
        Return sb.ToString()
    End Function
End Class
