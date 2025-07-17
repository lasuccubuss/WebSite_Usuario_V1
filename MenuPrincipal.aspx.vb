Public Class MenuPrincipal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NomeUsuario") IsNot Nothing Then
                lblBoasVindas.Text = "Bem-vindo(a), " & Session("NomeUsuario").ToString() & "!"
            Else
                Response.Redirect("Login.aspx")
            End If
        End If
    End Sub
    Protected Sub btnLogout_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub

End Class