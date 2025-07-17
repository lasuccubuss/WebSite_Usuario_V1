Imports Tesseract
Imports System.IO

Namespace MeuProjeto
    Public Class Util
        ' Função para gerar log de erro
        Public Shared Function GerarLogErro(ex As Exception) As String
            Dim caminhoPasta As String = HttpContext.Current.Server.MapPath("~/erros/")
            If Not Directory.Exists(caminhoPasta) Then
                Directory.CreateDirectory(caminhoPasta)
            End If

            Dim nomeArquivo As String = "ERR_" & Now.ToString("yyyyMMdd_HHmmss") & ".txt"
            Dim caminhoCompleto As String = Path.Combine(caminhoPasta, nomeArquivo)

            Using sw As New StreamWriter(caminhoCompleto)
                sw.WriteLine("Data/Hora: " & Now.ToString())
                sw.WriteLine("Mensagem: " & ex.Message)
                sw.WriteLine("StackTrace: " & ex.StackTrace)
            End Using

            Return ex.Message
        End Function

        ' 🚨 AQUI VEM A FUNÇÃO DE OCR
        Public Shared Function ExtrairTextoDoDocumento(caminhoImagem As String) As String
            Try
                Using engine As New TesseractEngine(HttpContext.Current.Server.MapPath("~/tessdata"), "por", EngineMode.Default)
                    Using img = Pix.LoadFromFile(caminhoImagem)
                        Using page = engine.Process(img)
                            Return page.GetText()
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Return "Erro ao processar OCR: " & ex.Message
            End Try
        End Function

        Imports System.Text.RegularExpressions
        Imports VB.Util ' <- garantir que o namespace da função OCR está aqui
        Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
            Dim caminhoSalvo As String = CaminhoCompletoDoArquivo ' onde você salvou o arquivo
            Dim textoExtraido As String = Util.ExtrairTextoDoDocumento(caminhoSalvo)

            ' Regex para localizar CPF
            Dim regex As New Regex("\d{3}\.\d{3}\.\d{3}-\d{2}")
            Dim match As Match = regex.Match(textoExtraido)

            If match.Success Then
                Dim cpfNoDoc As String = match.Value
                Dim cpfUsuario As String = lblCpfUsuario.Text ' exemplo: pega o CPF de algum label ou banco

                If cpfNoDoc = cpfUsuario Then
                    lblResultado.Text = "Documento válido!"
                Else
                    lblResultado.Text = "CPF no documento não confere com o usuário."
                End If
            Else
                lblResultado.Text = "Nenhum CPF encontrado no documento."
            End If
        End Sub
    End Class
End Namespace
