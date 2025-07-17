Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Drawing

Partial Class DocumentoLista
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarDocumentos()
        End If
    End Sub

    Private Sub CarregarDocumentos()
        Using conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString)
            Try
                conexao.Open()
                Dim comando As New SqlCommand("SELECT * FROM DOCUMENTOS", conexao)
                Dim adaptador As New SqlDataAdapter(comando)
                Dim tabela As New DataTable()
                adaptador.Fill(tabela)

                gvDocumentos.DataSource = tabela
                gvDocumentos.DataBind()
            Catch ex As Exception
                lblMensagem.Text = "Erro ao carregar documentos: " & ex.Message
                lblMensagem.ForeColor = Drawing.Color.Red
            End Try
        End Using
    End Sub

    Protected Sub gvDocumentos_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Select Case e.CommandName
            Case "Baixar"
                Dim caminho As String = e.CommandArgument.ToString()
                Dim caminhoFisico As String = Server.MapPath(caminho)

                If File.Exists(caminhoFisico) Then
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(caminhoFisico))
                    Response.TransmitFile(caminhoFisico)
                    Response.End()
                Else
                    lblMensagem.Text = "Arquivo não encontrado."
                    lblMensagem.ForeColor = Drawing.Color.Red
                End If

            Case "Editar"
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("DocumentoCadastro.aspx?id=" & id)

            Case "Excluir"
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                ExcluirDocumento(id)

            Case "Exif"
                Dim caminho As String = e.CommandArgument.ToString()
                Dim caminhoFisico As String = Server.MapPath(caminho)

                If File.Exists(caminhoFisico) Then
                    Try
                        Using img As Image = Image.FromFile(caminhoFisico)
                            Dim props As String = ""
                            For Each prop As PropertyItem In img.PropertyItems
                                props &= $"ID: {prop.Id} - Tipo: {prop.Type} - Tamanho: {prop.Len}" & "<br/>"
                            Next

                            lblMensagem.Text = "EXIF encontrados:<br/>" & props
                            lblMensagem.ForeColor = Drawing.Color.Black
                        End Using
                    Catch ex As Exception
                        lblMensagem.Text = "Erro ao ler EXIF: " & ex.Message
                        lblMensagem.ForeColor = Drawing.Color.Red
                    End Try
                Else
                    lblMensagem.Text = "Arquivo não encontrado para EXIF."
                    lblMensagem.ForeColor = Drawing.Color.Red
                End If
        End Select
    End Sub

    Private Sub ExcluirDocumento(id As Integer)
        Using conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString)
            Try
                conexao.Open()
                Dim comando As New SqlCommand("DELETE FROM DOCUMENTOS WHERE ID = @ID", conexao)
                comando.Parameters.AddWithValue("@ID", id)
                comando.ExecuteNonQuery()

                lblMensagem.Text = "Documento excluído com sucesso!"
                lblMensagem.ForeColor = Drawing.Color.Green

                CarregarDocumentos() ' <-- Chama sem parâmetro!
            Catch ex As Exception
                lblMensagem.Text = "Erro ao excluir: " & ex.Message
                lblMensagem.ForeColor = Drawing.Color.Red
            End Try
        End Using
    End Sub
End Class
