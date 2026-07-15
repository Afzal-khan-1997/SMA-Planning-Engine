Public Module Program

    <STAThread()>
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Using loginForm As New LoginForm()
            If loginForm.ShowDialog() <> DialogResult.OK Then
                Return
            End If
        End Using

        Application.Run(New SMAPlannerForm())
    End Sub

End Module
