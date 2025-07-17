Imports System.Net

Partial Class MeuIP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim ipLocal As String = ObterIPv4()
            lblIP.Text = ipLocal
        End If
    End Sub

    Private Function ObterIPv4() As String
        Dim hostName As String = Dns.GetHostName()
        Dim enderecos = Dns.GetHostAddresses(hostName)
        For Each ip In enderecos
            If ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then ' IPv4
                Return ip.ToString()
            End If
        Next
        Return "IP não encontrado"
    End Function
End Class
