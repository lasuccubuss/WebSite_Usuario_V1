Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System.Xml.Xsl
Imports System.Security.Cryptography
Imports iTextSharp.text.html.simpleparser


Public Class Relatorios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'Instância dos botões'
    Protected Sub btnUsuariosAtivos_Click(sender As Object, e As EventArgs)
        GerarXmlUsuariosAtivos()
        xmlViewer.DocumentSource = Server.MapPath("~/Relatorios/UsuariosAtivos.xml")
        xmlViewer.TransformSource = Server.MapPath("~/Relatorios/UsuariosAtivos.xsl")
    End Sub

    Protected Sub btnUsuariosInativos_Click(sender As Object, e As EventArgs)
        GerarXmlUsuariosInativos()
        xmlViewer.DocumentSource = Server.MapPath("~/Relatorios/UsuariosInativos.xml")
        xmlViewer.TransformSource = Server.MapPath("~/Relatorios/UsuariosInativos.xsl")
    End Sub

    'Métodos para fazer consulta no banco de dados'
    Private Sub GerarXmlUsuariosAtivos()
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString)
        Dim comando As New SqlCommand("SELECT NOME, EMAIL FROM USUARIOS WHERE STATUS = 0", conexao)

        Dim writer As XmlWriter = XmlWriter.Create(Server.MapPath("~/Relatorios/UsuariosAtivos.xml"))

        writer.WriteStartDocument()
        writer.WriteStartElement("Usuarios")

        conexao.Open()
        Dim leitor As SqlDataReader = comando.ExecuteReader()

        While leitor.Read()
            writer.WriteStartElement("Usuario")
            writer.WriteElementString("Nome", leitor("Nome").ToString())
            writer.WriteElementString("Email", leitor("Email").ToString())
            writer.WriteEndElement() ' Fecha </Usuario>
        End While

        writer.WriteEndElement()
        writer.Close()
        conexao.Close()
    End Sub

    'Métodos para fazer consulta no banco de dados'
    Private Sub GerarXmlUsuariosInativos()
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConexaoSQL").ConnectionString)
        Dim comando As New SqlCommand("SELECT NOME, EMAIL FROM USUARIOS WHERE STATUS = 1", conexao)

        Dim writer As XmlWriter = XmlWriter.Create(Server.MapPath("~/Relatorios/UsuariosInativos.xml"))

        writer.WriteStartDocument()
        writer.WriteStartElement("Usuarios")

        conexao.Open()
        Dim leitor As SqlDataReader = comando.ExecuteReader()

        While leitor.Read()
            writer.WriteStartElement("Usuario")
            writer.WriteElementString("Nome", leitor("Nome").ToString())
            writer.WriteElementString("Email", leitor("Email").ToString())
            writer.WriteEndElement() ' Fecha </Usuario>
        End While

        writer.WriteEndElement()
        writer.Close()
        conexao.Close()
    End Sub

    Protected Sub btnExportarPdfAtivos_Click(sender As Object, e As EventArgs)
        ' Transformar XML + XSL em HTML
        Dim xmlPath As String = Server.MapPath("~/Relatorios/UsuariosAtivos.xml")
        Dim xslPath As String = Server.MapPath("~/Relatorios/UsuariosAtivos.xsl")

        Dim stringWriter As New StringWriter()
        Dim xslTrans As New XslCompiledTransform()
        xslTrans.Load(xslPath)
        xslTrans.Transform(xmlPath, Nothing, XmlWriter.Create(stringWriter))

        ' Converter HTML em PDF
        Dim htmlContent As String = stringWriter.ToString()
        Dim bytes As Byte()

        Using memoryStream As New MemoryStream()
            Dim document As New Document(PageSize.A4, 25, 25, 30, 30)
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
            document.Open()

            Using htmlWorker As New iTextSharp.text.html.simpleparser.HTMLWorker(document)
                htmlWorker.Parse(New StringReader(htmlContent))
            End Using

            document.Close()
            bytes = memoryStream.ToArray()
        End Using

        ' Retornar o PDF para o navegador
        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "attachment; filename=RelatorioUsuariosAtivos.pdf")
        Response.BinaryWrite(bytes)
        Response.End()
    End Sub

    Protected Sub btnExportarPdfInativos_Click(sender As Object, e As EventArgs)
        ' Transformar XML + XSL em HTML
        Dim xmlPath As String = Server.MapPath("~/Relatorios/UsuariosInativos.xml")
        Dim xslPath As String = Server.MapPath("~/Relatorios/UsuariosInativos.xsl")

        Dim stringWriter As New StringWriter()
        Dim xslTrans As New XslCompiledTransform()
        xslTrans.Load(xslPath)
        xslTrans.Transform(xmlPath, Nothing, XmlWriter.Create(stringWriter))

        ' Converter HTML em PDF
        Dim htmlContent As String = stringWriter.ToString()
        Dim bytes As Byte()

        Using memoryStream As New MemoryStream()
            Dim document As New Document(PageSize.A4, 25, 25, 30, 30)
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
            document.Open()

            Using htmlWorker As New iTextSharp.text.html.simpleparser.HTMLWorker(document)
                htmlWorker.Parse(New StringReader(htmlContent))
            End Using

            document.Close()
            bytes = memoryStream.ToArray()
        End Using

        ' Retornar o PDF para o navegador
        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "attachment; filename=RelatorioUsuariosInativos.pdf")
        Response.BinaryWrite(bytes)
        Response.End()
    End Sub


    'Converte HTML em Excel'
    Protected Sub btnExportarExcelAtivos_Click(sender As Object, e As EventArgs)
        Dim xmlPath As String = Server.MapPath("~/Relatorios/UsuariosAtivos.xml")
        Dim xslPath As String = Server.MapPath("~/Relatorios/UsuariosAtivos.xsl")

        Dim stringWriter As New StringWriter()
        Dim xslTrans As New XslCompiledTransform()
        xslTrans.Load(xslPath)
        xslTrans.Transform(xmlPath, Nothing, XmlWriter.Create(stringWriter))

        Dim htmlContent As String = stringWriter.ToString()

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=RelatorioUsuariosAtivos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.UTF8
        Response.ContentType = "application/vnd.ms-excel"
        Response.Write("<html><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>")
        Response.Write(stringWriter.ToString())
        Response.Write("</html>")
        Response.End()

    End Sub

    Protected Sub btnExportarExcelInativos_Click(sender As Object, e As EventArgs)
        Dim xmlPath As String = Server.MapPath("~/Relatorios/UsuariosInativos.xml")
        Dim xslPath As String = Server.MapPath("~/Relatorios/UsuariosInativos.xsl")

        Dim stringWriter As New StringWriter()
        Dim xslTrans As New XslCompiledTransform()
        xslTrans.Load(xslPath)
        xslTrans.Transform(xmlPath, Nothing, XmlWriter.Create(stringWriter))

        Dim htmlContent As String = stringWriter.ToString()

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=RelatorioUsuariosInativos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.UTF8
        Response.ContentType = "application/vnd.ms-excel"
        Response.Write("<html><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>")
        Response.Write(stringWriter.ToString())
        Response.Write("</html>")
        Response.End()
    End Sub


End Class