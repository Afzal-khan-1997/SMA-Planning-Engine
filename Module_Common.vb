
Imports Microsoft.Data.SqlClient

Module Module_Common
    Public Function ActiveSQLConnection() As SqlConnection

        Dim SQLdbString As SqlConnection = New SqlConnection("")

        ActiveSQLConnection = SQLdbString

    End Function

End Module
