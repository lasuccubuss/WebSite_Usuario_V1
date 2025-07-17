Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Net.Mail
Imports System.Net

Partial Class UsuarioCadastro
    Inherits System.Web.UI.Page

    ' Conexão com o banco
    Dim conexao As New SqlConnection("Data Source=SRVTREND-02;Initial Catalog=DES_Laboratorio1;Integrated Security=True")

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarTiposEntidade()
            If Request.QueryString("id") IsNot Nothing Then
                CarregarUsuario()
            End If
        End If
    End Sub

    ' Carregar dados do usuário
    Private Sub CarregarUsuario()
        Dim id As Integer = Convert.ToInt32(Request.QueryString("id"))
        Dim cmd As New SqlCommand("SELECT * FROM USUARIOS WHERE ID = @ID", conexao)
        cmd.Parameters.AddWithValue("@ID", id)

        conexao.Open()
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            txtNome.Text = reader("NOME").ToString()
            txtEmail.Text = reader("EMAIL").ToString()
            ddlTipoUsuario.SelectedValue = reader("TIPO_USUARIO").ToString()
            txtDocumento.Text = reader("DOCUMENTO").ToString()
            ddlTipoDocumento.SelectedValue = reader("TIPO_DOCUMENTO").ToString()
            chkAtivo.Checked = (reader("STATUS").ToString() = "1")
        End If
        conexao.Close()
    End Sub

    ' Salvar (inserir ou atualizar)
    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs)
        Dim nome As String = txtNome.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim senha As String = txtSenha.Text.Trim()
        Dim tipoUsuario As String = ddlTipoUsuario.SelectedValue
        Dim documento As String = txtDocumento.Text.Trim()
        Dim tipoDocumento As String = ddlTipoDocumento.SelectedValue
        Dim status As Integer = If(chkAtivo.Checked, 1, 0)

        If nome = "" Or email = "" Or documento = "" Then
            lblMensagem.Text = "Preencha todos os campos obrigatórios."
            Return
        End If

        ' Verifica se o e-mail já existe
        Dim cmdCheck As New SqlCommand("SELECT COUNT(*) FROM USUARIOS WHERE EMAIL = @EMAIL" & If(Request.QueryString("id") IsNot Nothing, " AND ID <> @ID", ""), conexao)
        cmdCheck.Parameters.AddWithValue("@EMAIL", email)
        If Request.QueryString("id") IsNot Nothing Then
            cmdCheck.Parameters.AddWithValue("@ID", Convert.ToInt32(Request.QueryString("id")))
        End If

        conexao.Open()
        Dim emailExiste As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
        conexao.Close()

        If emailExiste > 0 Then
            lblMensagem.Text = "Erro: já existe um usuário com esse e-mail."
            Return
        End If

        ' Atualizar ou inserir
        If Request.QueryString("id") IsNot Nothing Then
            Dim id As Integer = Convert.ToInt32(Request.QueryString("id"))
            Dim cmd As New SqlCommand("UPDATE USUARIOS SET NOME = @NOME, EMAIL = @EMAIL, TIPO_USUARIO = @TIPO_USUARIO, DOCUMENTO = @DOCUMENTO, TIPO_DOCUMENTO = @TIPO_DOCUMENTO, STATUS = @STATUS WHERE ID = @ID", conexao)
            cmd.Parameters.AddWithValue("@NOME", nome)
            cmd.Parameters.AddWithValue("@EMAIL", email)
            cmd.Parameters.AddWithValue("@TIPO_USUARIO", tipoUsuario)
            cmd.Parameters.AddWithValue("@DOCUMENTO", documento)
            cmd.Parameters.AddWithValue("@TIPO_DOCUMENTO", tipoDocumento)
            cmd.Parameters.AddWithValue("@STATUS", status)
            cmd.Parameters.AddWithValue("@ID", id)

            conexao.Open()
            cmd.ExecuteNonQuery()
            conexao.Close()

            lblMensagem.Text = "Usuário atualizado com sucesso!"
            lblMensagem.ForeColor = Drawing.Color.Green
        Else
            Dim hashSenha As String = GerarHashSHA256(senha)
            Dim cmd As New SqlCommand("sp_InserirUsuarioDocumentosObrigatorios", conexao)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@Nome", nome)
            cmd.Parameters.AddWithValue("@Email", email)
            cmd.Parameters.AddWithValue("@SenhaHash", hashSenha)
            cmd.Parameters.AddWithValue("@TipoUsuario", tipoUsuario)
            cmd.Parameters.AddWithValue("@Documento", documento)
            cmd.Parameters.AddWithValue("@TipoDocumento", tipoDocumento)
            cmd.Parameters.AddWithValue("@Status", status)

            conexao.Open()
            Try
                cmd.ExecuteNonQuery()
                conexao.Close()

                ' Enviar e-mail
                EnviarEmailPrimeiroAcesso(email, nome)

                lblMensagem.Text = "Usuário cadastrado com sucesso! E-mail enviado para: " & email
                lblMensagem.ForeColor = Drawing.Color.Green
            Catch ex As SqlException
                conexao.Close()
                lblMensagem.Text = "Erro ao cadastrar: " & ex.Message
                lblMensagem.ForeColor = Drawing.Color.Red
            End Try
        End If
    End Sub

    ' Gerar hash SHA256
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

    ' Enviar e-mail de primeiro acesso
    Private Sub EnviarEmailPrimeiroAcesso(destinatario As String, nomeUsuario As String)
        Try
            Dim remetente As New MailAddress("garciasusana21@gmail.com", "Equipe Trend Consulting")
            Dim destinatarioEmail As New MailAddress(destinatario)

            Dim mensagem As New MailMessage(remetente, destinatarioEmail)
            mensagem.Subject = "Bem-vindo! Acesse sua conta no sistema"
            mensagem.IsBodyHtml = True

            Dim linkAcesso As String = "http://192.168.0.105:1234/PrimeiroAcesso.aspx?email=" & destinatario

            mensagem.Body = $"<h3>Olá, {nomeUsuario}!</h3>
                              <p>Seu acesso ao sistema foi criado com sucesso.</p>
                              <p>Clique no link abaixo para definir sua senha e completar seu primeiro acesso:</p>
                              <p><a href='{linkAcesso}' target='_blank'>{linkAcesso}</a></p>
                              <p>Atenciosamente,<br/>Equipe Trend Consulting</p>"

            Dim smtp As New SmtpClient("smtp.gmail.com")
            smtp.Port = 587
            smtp.Credentials = New NetworkCredential("garciasusana21@gmail.com", "zdovqbejmtxacwuf")
            smtp.EnableSsl = True

            smtp.Send(mensagem)
        Catch ex As Exception
            lblMensagem.Text &= "<br/>Erro ao enviar e-mail: " & ex.Message
        End Try
    End Sub

    Private Sub CarregarTiposEntidade()
        Dim cmd As New SqlCommand("SELECT ID, NOME FROM TIPOS_ENTIDADE", conexao)
        conexao.Open()
        ddlTipoEntidade.DataSource = cmd.ExecuteReader()
        ddlTipoEntidade.DataTextField = "NOME"
        ddlTipoEntidade.DataValueField = "ID"
        ddlTipoEntidade.DataBind()
        conexao.Close()
    End Sub
End Class
