Public Class TaskCatalogService
    Private ReadOnly _sqlRepository As SqlProjectRepository

    Public Sub New(Optional sqlRepository As SqlProjectRepository = Nothing)
        _sqlRepository = sqlRepository
    End Sub

    Public Function LoadAvailableTasks() As List(Of TaskCatalogItem)
        If _sqlRepository Is Nothing Then
            Return New List(Of TaskCatalogItem)()
        End If

        Return _sqlRepository.LoadTaskCatalog()
    End Function

    Public Function LoadTemplateTasks(templateName As String, projectSize As String, Optional reportType As String = "") As List(Of TaskCatalogItem)
        If _sqlRepository Is Nothing Then
            Return New List(Of TaskCatalogItem)()
        End If

        Return _sqlRepository.LoadTaskTemplates(templateName, projectSize, reportType).
            Where(Function(task) task.HoursForSize(projectSize) > 0D).
            OrderBy(Function(task) If(task.TaskOrder > 0, task.TaskOrder, task.DatabaseTaskId)).
            ToList()
    End Function
End Class
