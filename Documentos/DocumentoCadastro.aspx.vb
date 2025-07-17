Imports System.Data.SqlClient
Imports System.IO

Partial Class DocumentoCadastro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("id") IsNot Nothing Then
                Dim id As Integer = Convert.ToInt32(Request.QueryString("id"))
                CarregarDocumento(id)
            End If
        End If
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs)
        If fuArquivo.HasFile Then
            Try
                ' Caminhos
                Dim pastaVirtual As String = "~/Uploads/"
                Dim pastaFisica As String = Server.MapPath(pastaVirtual)

                If Not Directory.Exists(pastaFisica) Then
                    Directory.CreateDirectory(pastaFisica)
                End If

                Dim nomeOriginal As String = Path.GetFileName(fuArquivo.FileName)
                Dim nomeUnico As String = Guid.NewGuid().ToString() & "_" & nomeOriginal
                Dim caminhoCompleto As String = pastaFisica & nomeUnico
                Dim caminhoVirtualCompleto As String = pastaVirtual & nomeUnico

                fuArquivo.SaveAs(caminhoCompleto)

                ' Dados do formulário
                Dim nome As String = txtNome.Text.Trim()
                Dim descricao As String = txtDescricao.Text.Trim()
                Dim tipo As String = ddlTipo.SelectedValue

                Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString)
                Dim comando As SqlCommand

                If Request.QueryString("id") IsNot Nothing Then
                    ' Atualizar
                    Dim id As Integer = Convert.ToInt32(Request.QueryString("id"))
                    comando = New SqlCommand("UPDATE DOCUMENTOS SET NOME=@NOME, DESCRICAO=@DESCRICAO, TIPO=@TIPO, CAMINHO=@CAMINHO WHERE ID=@ID", conexao)
                    comando.Parameters.AddWithValue("@ID", id)
                Else
                    ' Inserir
                    comando = New SqlCommand("INSERT INTO DOCUMENTOS (NOME, DESCRICAO, TIPO, CAMINHO) VALUES (@NOME, @DESCRICAO, @TIPO, @CAMINHO)", conexao)
                End If

                comando.Parameters.AddWithValue("@NOME", nome)
                comando.Parameters.AddWithValue("@DESCRICAO", descricao)
                comando.Parameters.AddWithValue("@TIPO", tipo)
                comando.Parameters.AddWithValue("@CAMINHO", caminhoVirtualCompleto)

                conexao.Open()
                comando.ExecuteNonQuery()
                conexao.Close()

                lblMensagem.Text = "Documento salvo com sucesso!"
                lblMensagem.ForeColor = Drawing.Color.Green
            Catch ex As Exception
                lblMensagem.Text = "Erro: " & ex.Message
                lblMensagem.ForeColor = Drawing.Color.Red
            End Try
        Else
            lblMensagem.Text = "Selecione um arquivo para enviar."
            lblMensagem.ForeColor = Drawing.Color.OrangeRed
        End If
    End Sub
    Private Sub CarregarDocumento(id As Integer)
        Using conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString)
            Try
                conexao.Open()
                Dim comando As New SqlCommand("SELECT * FROM DOCUMENTOS WHERE ID = @ID", conexao)
                comando.Parameters.AddWithValue("@ID", id)
                Dim leitor As SqlDataReader = comando.ExecuteReader()

                If leitor.Read() Then
                    txtNome.Text = leitor("NOME").ToString()
                    txtDescricao.Text = leitor("DESCRICAO").ToString()
                    ddlTipo.SelectedValue = leitor("TIPO").ToString()
                    ' O arquivo não precisa ser exibido nem carregado aqui
                End If
            Catch ex As Exception
                lblMensagem.Text = "Erro ao carregar o documento: " & ex.Message
                lblMensagem.ForeColor = Drawing.Color.Red
            End Try
        End Using
    End Sub

End Class
