Imports System.Security.Principal

Partial Public Class LoginForm
    Inherits Form

    Private Shared ReadOnly AllowedUsers As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        "Sheik.Ahsan",
        "Afzal.khan"
    }

    Private Const AllowedPassword As String = "12345"

    <STAThread()>
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New LoginForm())
    End Sub

    Public Sub New()
        InitializeComponent()
        _usernameTextBox.Text = CurrentWindowsUserName()
        _passwordTextBox.Select()
    End Sub

    Private Sub LoginButtonClick(sender As Object, e As EventArgs) Handles _loginButton.Click
        TryLogin()
    End Sub

    Private Sub TryLogin()
        Dim enteredUserName = _usernameTextBox.Text.Trim()
        Dim enteredPassword = _passwordTextBox.Text
        Dim windowsUserName = CurrentWindowsUserName()

        If Not AllowedUsers.Contains(enteredUserName) Then
            ShowLoginError("Username is not allowed.")
            Return
        End If

        If Not String.Equals(enteredUserName, windowsUserName, StringComparison.OrdinalIgnoreCase) Then
            ShowLoginError("Username must match the current Windows user: " & windowsUserName)
            Return
        End If

        If Not String.Equals(enteredPassword, AllowedPassword, StringComparison.Ordinal) Then
            ShowLoginError("Password is incorrect.")
            Return
        End If

        OpenPlannerForm()
    End Sub

    Private Sub OpenPlannerForm()
        Hide()

        Using plannerForm As New SMAPlannerForm()
            plannerForm.ShowDialog()
        End Using

        Close()
    End Sub

    Private Sub ShowLoginError(message As String)
        _statusLabel.Text = message
        _passwordTextBox.Clear()
        _passwordTextBox.Select()
    End Sub

    Private Shared Function CurrentWindowsUserName() As String
        Dim identityName = WindowsIdentity.GetCurrent().Name
        If String.IsNullOrWhiteSpace(identityName) Then
            Return Environment.UserName
        End If

        Dim slashIndex = identityName.LastIndexOf("\"c)
        If slashIndex >= 0 AndAlso slashIndex < identityName.Length - 1 Then
            Return identityName.Substring(slashIndex + 1)
        End If

        Return identityName
    End Function
End Class
