Imports System.Text.RegularExpressions

Public Class Validador
    Public Shared Function ExtrairCPF(texto As String) As String
        Dim padrao As String = "\b\d{3}\.?\d{3}\.?\d{3}-?\d{2}\b"
        Dim match As Match = Regex.Match(texto, padrao)
        If match.Success Then
            Return match.Value
        End If
        Return String.Empty
    End Function

    Public Shared Function ValidarCPF(cpf As String) As Boolean
        cpf = cpf.Replace(".", "").Replace("-", "").Trim()

        If cpf.Length <> 11 OrElse Not IsNumeric(cpf) Then Return False
        If cpf.Distinct().Count() = 1 Then Return False

        Dim soma As Integer = 0
        For i = 0 To 8
            soma += CInt(cpf(i).ToString()) * (10 - i)
        Next
        Dim resto = soma Mod 11
        Dim digito1 = If(resto < 2, 0, 11 - resto)

        soma = 0
        For i = 0 To 9
            soma += CInt(cpf(i).ToString()) * (11 - i)
        Next
        resto = soma Mod 11
        Dim digito2 = If(resto < 2, 0, 11 - resto)

        Return cpf(9).ToString() = digito1.ToString() AndAlso cpf(10).ToString() = digito2.ToString()
    End Function
End Class
