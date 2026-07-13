Public Class EmployeeCatalogService
    Private ReadOnly _sqlRepository As SqlProjectRepository

    Public Sub New(Optional sqlRepository As SqlProjectRepository = Nothing)
        _sqlRepository = sqlRepository
    End Sub

    Public Function LoadEmployees() As List(Of String)
        If _sqlRepository IsNot Nothing Then
            Try
                Dim employeesFromSql = _sqlRepository.LoadEmployees()
                If employeesFromSql.Count > 0 Then
                    Return employeesFromSql
                End If
            Catch
            End Try
        End If

        Return New List(Of String)()
    End Function
End Class
