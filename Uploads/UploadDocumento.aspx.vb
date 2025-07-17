Imports System.IO

Partial Class UploadDocumento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarDocumentosObrigatorios()
        End If
    End Sub

    Private Sub CarregarDocumentosObrigatorios()
        ' Simulando os dados de documentos obrigatórios
        Dim dt As New DataTable()
        dt.Columns.Add("ID_DOCUMENTO")
        dt.Columns.Add("NOME_DOCUMENTO")

        dt.Rows.Add("1", "RG")
        dt.Rows.Add("2", "CPF")
        dt.Rows.Add("3", "Comprovante de Endereço")

        rptDocumentos.DataSource = dt
        rptDocumentos.DataBind()
    End Sub

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs)
        For Each item As RepeaterItem In rptDocumentos.Items
            Dim fu As FileUpload = CType(item.FindControl("fuDocumento"), FileUpload)
            Dim hfID As HiddenField = CType(item.FindControl("hfIDDocumento"), HiddenField)

            If fu.HasFile Then
                Dim fileName As String = Path.GetFileName(fu.FileName)
                Dim folderPath As String = Server.MapPath("~/Uploads/")
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                fu.SaveAs(Path.Combine(folderPath, fileName))
            End If
        Next

        lblMensagem.Text = "Documentos enviados com sucesso!"
    End Sub
End Class
