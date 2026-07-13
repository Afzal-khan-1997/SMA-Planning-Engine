Imports System.Globalization

Public Class LiveProjectCatalogService
    Private ReadOnly _sqlRepository As SqlProjectRepository

    Public Sub New(Optional sqlRepository As SqlProjectRepository = Nothing)
        _sqlRepository = sqlRepository
    End Sub

    Public Function SearchProjects(searchText As String) As List(Of LiveProjectItem)
        Dim query = If(searchText, "").Trim()

        If _sqlRepository Is Nothing Then
            Return New List(Of LiveProjectItem)()
        End If

        Try
            Return _sqlRepository.LoadTemplateProjects(query).
            GroupBy(Function(project) project.ProjectName, StringComparer.OrdinalIgnoreCase).
            Select(Function(group) group.First()).
            OrderBy(Function(project) project.ProjectName).
            ToList()
        Catch
            Return New List(Of LiveProjectItem)()
        End Try
    End Function
End Class

Public Class LiveProjectItem
    Public Property ProjectCode As String = ""
    Public Property ProjectName As String = ""
    Public Property ClientName As String = ""
    Public Property VersionNumber As String = ""
    Public Property ProjectSize As String = ""
    Public Property TemplateName As String = "New Project"
    Public Property ProjectType As String = "New"
    Public Property SavedProjectId As Integer
    Public Property SourceFilePath As String = ""
    Public Property ReportType As String = ""
    Public Property TaskReportFilter As String = ""
    Public Property ProjectDetailsText As String = ""
    Public Property FinalCompletionDate As Date?
    Public Property PlanningMessage As String = ""
    Public Property ControllerAtRolc As String = ""
    Public Property ClientType As String = ""
    Public Property IsPointcloud As Boolean
    Public Property TechPack As Boolean
    Public Property DeedProfile As Boolean
    Public Property ShadowAnalysis As Boolean
    Public Property UrgentSmallProjects As Boolean

    Public ReadOnly Property IsStoredProject As Boolean
        Get
            Return SavedProjectId > 0 OrElse Not String.IsNullOrWhiteSpace(SourceFilePath)
        End Get
    End Property

    Public ReadOnly Property DisplayText As String
        Get
            Return ProjectName
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return DisplayText
    End Function
End Class
