Imports System.Globalization

Public Class ProjectLibraryItem
    Public Property ProjectId As Integer
    Public Property ProjectCode As String = ""
    Public Property ProjectName As String = ""
    Public Property VersionNumber As String = ""
    Public Property ProjectSize As String = ""
    Public Property ProjectType As String = "New"
    Public Property TaskCount As Integer
    Public Property ResourceHours As Decimal
    Public Property StartDate As Date?
    Public Property FinishDate As Date?
    Public Property StartDateText As String = ""
    Public Property FinishDateText As String = ""
    Public Property UpdatedOn As Date
    Public Property FilePath As String = ""
    Public Property IsActive As Boolean = True

    Public ReadOnly Property DisplayProjectId As String
        Get
            If Not String.IsNullOrWhiteSpace(ProjectCode) Then
                Return ProjectCode
            End If

            If ProjectId > 0 Then
                Return ProjectId.ToString(CultureInfo.InvariantCulture)
            End If

            Return ""
        End Get
    End Property
End Class
