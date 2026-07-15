Imports System.ComponentModel
Imports System.Data
Imports System.Globalization
Imports System.Reflection
Imports Microsoft.Data.SqlClient

Public Class SqlProjectRepository
    Public Sub SaveProject(projectName As String, tasks As BindingList(Of ScheduleTask), projectVersion As String, projectSize As String, projectType As String, totalProjectHours As Decimal, resourcesNeeded As Integer, resourceHours As Decimal, Optional projectIdAtSma As String = "")
        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()

            If String.IsNullOrWhiteSpace(projectVersion) Then
                Throw New InvalidOperationException("Project version is required before saving to SQL.")
            End If

            If String.IsNullOrWhiteSpace(projectIdAtSma) Then
                Throw New InvalidOperationException("Project ID at SMA is required before saving to SQL.")
            End If

            Dim normalizedVersion = projectVersion.Trim()
            Dim projectCode = projectIdAtSma.Trim()
            ValidateExcelSqlDesign(connection, requireScheduleSaveTables:=True)
            Dim affectedCapacityRows = LoadScheduleCapacityRows(connection, projectCode, normalizedVersion)

            Dim savedRows = SaveProjectScheduleRows(connection, projectCode, normalizedVersion, tasks)
            affectedCapacityRows.AddRange(savedRows)
            UpdateEmployeeCapacityFromSchedule(connection, affectedCapacityRows)
        End Using
    End Sub

    Public Function ListProjects(Optional maxResults As Integer = 50) As List(Of ProjectLibraryItem)
        Return ListRecentScheduledProjects("", activeProjects:=True, maxResults:=maxResults)
    End Function

    Public Function GetProjectPlanningInfo(projectIdAtSma As String) As SqlProjectPlanningInfo
        Dim projectCode = If(projectIdAtSma, "").Trim()
        If projectCode.Length = 0 Then
            Return Nothing
        End If

        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()

            Dim GetProjectDetailsDataTable As New DataTable()

            Using GetProjectDetails As IDbCommand = connection.CreateCommand()
                GetProjectDetails.CommandText =
                    "SELECT TOP 1 [Project Name], [Project ID at SMA], [Version], [Active], [Report BRE], [Report ROL], [Report Within], [Final Completion Date], [Planning Message], [ControlleratROLC], [Client Type], [Is_Pointcloud], [Teck Pack], [Deed Profile], [Shadow_Analysis], [Urgent Small Projects], [IsPlanned] " &
                    "FROM dbo.Version_Table " &
                    "WHERE [Project ID at SMA] = @ProjectIdAtSma AND ISNULL([Active], 0) = 1"
                AddParameter(GetProjectDetails, "@ProjectIdAtSma", projectCode)

                Using GetProjectDetailsReader = GetProjectDetails.ExecuteReader()
                    GetProjectDetailsDataTable.Load(GetProjectDetailsReader)
                End Using
            End Using

            If GetProjectDetailsDataTable.Rows.Count = 0 Then
                Return Nothing
            End If

            Dim projectDetailsRow = GetProjectDetailsDataTable.Rows(0)
            Return New SqlProjectPlanningInfo With {
                .ProjectIdAtSma = Convert.ToString(projectDetailsRow("Project ID at SMA"), CultureInfo.InvariantCulture),
                .ProjectName = Convert.ToString(projectDetailsRow("Project Name"), CultureInfo.InvariantCulture),
                .VersionNumber = RequiredSqlVersionText(projectDetailsRow("Version")),
                .IsActive = SqlBoolean(projectDetailsRow("Active")),
                .IsPlanned = SqlNullableBoolean(projectDetailsRow("IsPlanned")),
                .ProjectSize = GetActualProjectSize(connection, projectCode),
                .ProjectType = ProjectTypeFromVersion(projectDetailsRow("Version")),
                .ReportType = BuildReportTypeDisplay(projectDetailsRow("Report BRE"), projectDetailsRow("Report ROL"), projectDetailsRow("Report Within")),
                .FinalCompletionDate = SqlNullableDate(projectDetailsRow("Final Completion Date")),
                .PlanningMessage = SqlText(projectDetailsRow("Planning Message")),
                .ControllerAtRolc = SqlText(projectDetailsRow("ControlleratROLC")),
                .ClientType = SqlText(projectDetailsRow("Client Type")),
                .IsPointcloud = SqlBoolean(projectDetailsRow("Is_Pointcloud")),
                .TechPack = SqlBoolean(projectDetailsRow("Teck Pack")),
                .DeedProfile = SqlBoolean(projectDetailsRow("Deed Profile")),
                .ShadowAnalysis = SqlBoolean(projectDetailsRow("Shadow_Analysis")),
                .UrgentSmallProjects = SqlBoolean(projectDetailsRow("Urgent Small Projects"))
            }
        End Using
    End Function

    Public Function ListRecentScheduledProjects(projectIdAtSma As String, activeProjects As Boolean, Optional maxResults As Integer = 50) As List(Of ProjectLibraryItem)
        Dim projects As New List(Of ProjectLibraryItem)()
        Dim projectCode = If(projectIdAtSma, "").Trim()

        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()

            If Not TableExists(connection, "dbo.Version_Table") OrElse Not TableExists(connection, "dbo.Project Schedule Table") Then
                Return projects
            End If

            Dim projectDetails = LoadVersionProjectLookup(connection)
            Dim actualProjectSizes = LoadActualProjectSizeLookup(connection)

            Using command = connection.CreateCommand()
                command.CommandText =
                    "SELECT TOP " & Math.Max(1, maxResults).ToString(CultureInfo.InvariantCulture) & " " &
                    "s.[ProjectID at SMA], COUNT(DISTINCT s.[Task Order]) AS TaskCount, SUM(CAST(ISNULL(s.[Planned Hours], 0) AS DECIMAL(12,2))) AS ResourceHours, " &
                    "MIN(s.[Schedule Date]) AS StartDate, MAX(s.[Schedule Date]) AS FinishDate, MAX(s.[Planned On]) AS UpdatedOn " &
                    "FROM [dbo].[Project Schedule Table] s " &
                    "WHERE (@ProjectIdAtSma = '' OR s.[ProjectID at SMA] = @ProjectIdAtSma) " &
                    "GROUP BY s.[ProjectID at SMA] " &
                    "ORDER BY MAX(s.[Planned On]) DESC, s.[ProjectID at SMA]"
                AddParameter(command, "@ProjectIdAtSma", projectCode)

                Using reader = command.ExecuteReader()
                    While reader.Read()
                        Dim scheduledProjectCode = SqlText(reader("ProjectID at SMA"))
                        Dim projectInfo As SqlProjectPlanningInfo = Nothing
                        If scheduledProjectCode.Length = 0 OrElse Not projectDetails.TryGetValue(scheduledProjectCode, projectInfo) Then
                            Continue While
                        End If

                        If Not projectInfo.IsPlanned.GetValueOrDefault(False) OrElse projectInfo.IsActive <> activeProjects Then
                            Continue While
                        End If

                        Dim actualProjectSize = ""
                        actualProjectSizes.TryGetValue(scheduledProjectCode, actualProjectSize)

                        Dim item As New ProjectLibraryItem With {
                            .ProjectId = 0,
                            .ProjectCode = scheduledProjectCode,
                            .ProjectName = projectInfo.ProjectName,
                            .VersionNumber = projectInfo.VersionNumber,
                            .ProjectSize = actualProjectSize,
                            .ProjectType = projectInfo.ProjectType,
                            .TaskCount = If(reader("TaskCount") Is DBNull.Value, 0, CInt(reader("TaskCount"))),
                            .ResourceHours = If(reader("ResourceHours") Is DBNull.Value, 0D, CDec(reader("ResourceHours"))),
                            .UpdatedOn = If(reader("UpdatedOn") Is DBNull.Value, Date.Now, CDate(reader("UpdatedOn"))),
                            .IsActive = projectInfo.IsActive
                        }

                        If reader("StartDate") IsNot DBNull.Value Then
                            item.StartDate = CDate(reader("StartDate")).Date
                            item.StartDateText = item.StartDate.Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)
                        End If

                        If reader("FinishDate") IsNot DBNull.Value Then
                            item.FinishDate = CDate(reader("FinishDate")).Date
                            item.FinishDateText = item.FinishDate.Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)
                        End If

                        projects.Add(item)
                    End While
                End Using
            End Using
        End Using

        Return projects
    End Function

    Public Function LoadSavedProjectSchedule(projectId As Integer) As SavedProjectSchedule
        Return Nothing
    End Function

    Public Function LoadSavedProjectScheduleByProjectCode(projectIdAtSma As String) As SavedProjectSchedule
        Dim projectCode = If(projectIdAtSma, "").Trim()
        If projectCode.Length = 0 Then
            Return Nothing
        End If

        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()

            If Not TableExists(connection, "dbo.Version_Table") OrElse Not TableExists(connection, "dbo.Project Schedule Table") Then
                Return Nothing
            End If

            Dim projectInfo = GetProjectPlanningInfo(projectCode)
            If projectInfo Is Nothing Then
                Return Nothing
            End If

            Dim employeeNames = LoadEmployeeNameLookupFromMaster(connection)
            Dim rows As New List(Of AssignmentSavedScheduleRow)()

            Using command = connection.CreateCommand()
                command.CommandText =
                    "SELECT s.[Task Name], s.[Task Order], s.[Employee ID], s.[Planned Hours], s.[Schedule Date], s.[Planned On] " &
                    "FROM [dbo].[Project Schedule Table] s " &
                    "WHERE s.[ProjectID at SMA] = @ProjectIdAtSma " &
                    "ORDER BY s.[Task Order], s.[Schedule Date], s.[Employee ID]"
                AddParameter(command, "@ProjectIdAtSma", projectCode)

                Using reader = command.ExecuteReader()
                    While reader.Read()
                        If reader("Task Order") Is DBNull.Value OrElse reader("Schedule Date") Is DBNull.Value Then
                            Continue While
                        End If

                        Dim employeeId = If(reader("Employee ID") Is DBNull.Value, 0, CInt(reader("Employee ID")))
                        rows.Add(New AssignmentSavedScheduleRow With {
                            .ProjectId = 0,
                            .ProjectName = projectInfo.ProjectName,
                            .VersionNumber = projectInfo.VersionNumber,
                            .ProjectSize = projectInfo.ProjectSize,
                            .ProjectType = projectInfo.ProjectType,
                            .TaskId = CInt(reader("Task Order")),
                            .TaskName = Convert.ToString(reader("Task Name"), CultureInfo.InvariantCulture),
                            .TaskOrder = CInt(reader("Task Order")),
                            .EmployeeId = employeeId,
                            .WorkDate = CDate(reader("Schedule Date")).Date,
                            .AssignedHours = If(reader("Planned Hours") Is DBNull.Value, 0D, CDec(reader("Planned Hours"))),
                            .StartDate = CDate(reader("Schedule Date")).Date,
                            .FinishDate = CDate(reader("Schedule Date")).Date,
                            .DependencyTaskOrder = Nothing,
                            .DependencyType = "FS",
                            .UpdatedOn = If(reader("Planned On") Is DBNull.Value, Date.Now, CDate(reader("Planned On")))
                        })
                    End While
                End Using
            End Using

            If rows.Count = 0 Then
                Return Nothing
            End If

            Dim savedSchedule As New SavedProjectSchedule With {
                .ProjectIdAtSma = projectCode,
                .ProjectName = projectInfo.ProjectName,
                .VersionNumber = projectInfo.VersionNumber,
                .ProjectSize = projectInfo.ProjectSize,
                .ProjectType = projectInfo.ProjectType,
                .ResourceHours = rows.Sum(Function(row) row.AssignedHours),
                .UpdatedOn = rows.Max(Function(row) row.UpdatedOn)
            }

            For Each taskGroup In rows.GroupBy(Function(row) row.TaskOrder).OrderBy(Function(group) group.Key)
                Dim taskRows = taskGroup.OrderBy(Function(row) row.WorkDate).ToList()
                Dim taskNames = taskRows.
                    Select(Function(row)
                               If employeeNames.ContainsKey(row.EmployeeId) Then
                                   Return employeeNames(row.EmployeeId)
                               End If
                               Return "Employee " & row.EmployeeId.ToString(CultureInfo.InvariantCulture)
                           End Function).
                    Distinct(StringComparer.OrdinalIgnoreCase).
                    ToList()

                Dim allocationTotals As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
                Dim dailyAllocations As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

                For Each row In taskRows
                    Dim employeeName = If(employeeNames.ContainsKey(row.EmployeeId), employeeNames(row.EmployeeId), "Employee " & row.EmployeeId.ToString(CultureInfo.InvariantCulture))
                    If Not allocationTotals.ContainsKey(employeeName) Then
                        allocationTotals(employeeName) = 0D
                    End If

                    allocationTotals(employeeName) += row.AssignedHours
                    dailyAllocations(employeeName.Trim() & "|" & row.WorkDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)) = row.AssignedHours
                Next

                Dim startDate = taskRows.Min(Function(row) row.WorkDate)
                Dim finishDate = taskRows.Max(Function(row) row.WorkDate)
                Dim resourceHours = taskRows.Sum(Function(row) row.AssignedHours)

                savedSchedule.Tasks.Add(New ScheduleTask With {
                    .TaskId = taskGroup.Key,
                    .DatabaseTaskId = taskGroup.Key,
                    .TaskName = taskRows(0).TaskName,
                    .StartDate = startDate,
                    .FinishDate = finishDate,
                    .DurationDays = Math.Max(0.031D, Math.Round(resourceHours / 8D, 3, MidpointRounding.AwayFromZero)),
                    .PercentComplete = 0,
                    .Predecessors = If(taskGroup.Key <= 1, "", (taskGroup.Key - 1).ToString(CultureInfo.InvariantCulture)),
                    .DependencyType = "FS",
                    .AssignedTo = String.Join("; ", taskNames),
                    .AssignmentDate = startDate,
                    .ResourceNames = String.Join("; ", taskNames),
                    .ResourceAllocations = BuildResourceAllocationsString(allocationTotals),
                    .DailyResourceAllocations = BuildDailyAllocationsString(dailyAllocations),
                    .ResourceHours = resourceHours,
                    .ModuleId = 0
                })
            Next

            savedSchedule.TotalProjectHours = savedSchedule.Tasks.Sum(Function(task) task.ResourceHours)
            savedSchedule.ResourcesNeeded = rows.Select(Function(row) row.EmployeeId).Distinct().Count()
            Return savedSchedule
        End Using
    End Function

    Public Function LoadProject(projectName As String) As BindingList(Of ScheduleTask)
        Return New BindingList(Of ScheduleTask)()
    End Function

    Public Function LoadResourceAvailability(startDate As Date, finishDate As Date) As Dictionary(Of String, Dictionary(Of Date, Decimal))
        Dim result As New Dictionary(Of String, Dictionary(Of Date, Decimal))(StringComparer.OrdinalIgnoreCase)

        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()
            ValidateExcelSqlDesign(connection, requireScheduleSaveTables:=True)

            If TableHasRows(connection, "dbo.Employee_Capacity") Then
                LoadAvailabilityFromEmployeeCapacity(connection, result, startDate, finishDate)
                Return result
            End If
        End Using

        Return result
    End Function

    Public Function LoadEmployees() As List(Of String)
        Dim result As New List(Of String)()

        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()
            ValidateExcelSqlDesign(connection, requireScheduleSaveTables:=False)

            If TableExists(connection, "dbo.Employees_Master") Then
                Using command = connection.CreateCommand()
                    command.CommandText =
                        "SELECT DISTINCT LTRIM(RTRIM(firstname)) AS EmployeeName FROM dbo.Employees_Master WHERE ISNULL(Active, 1) = 1 AND ISNULL(LTRIM(RTRIM(firstname)), '') <> '' ORDER BY EmployeeName"

                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            If reader("EmployeeName") Is DBNull.Value Then
                                Continue While
                            End If

                            Dim employeeName = CStr(reader("EmployeeName")).Trim()
                            If employeeName.Length > 0 Then
                                result.Add(employeeName)
                            End If
                        End While
                    End Using
                End Using

                Return result.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(Function(name) name, StringComparer.OrdinalIgnoreCase).ToList()
            End If
        End Using

        Return result.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(Function(name) name, StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Public Function LoadCapacityPlanningData(startDate As Date, finishDate As Date) As SqlCapacityPlanningData
        Dim result As New SqlCapacityPlanningData()

        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()
            ValidateExcelSqlDesign(connection, requireScheduleSaveTables:=True)

            If TableExists(connection, "dbo.Employees_Master") Then
                Using command = connection.CreateCommand()
                    command.CommandText =
                        "SELECT DISTINCT LTRIM(RTRIM(CAST([Office Account] AS NVARCHAR(100)))) AS EmployeeId, LTRIM(RTRIM(firstname)) AS EmployeeName " &
                        "FROM dbo.Employees_Master " &
                        "WHERE ISNULL(Active, 1) = 1 AND ISNULL(LTRIM(RTRIM(firstname)), '') <> '' " &
                        "ORDER BY EmployeeName"

                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            Dim employeeName = SqlText(reader("EmployeeName"))
                            If employeeName.Length = 0 Then
                                Continue While
                            End If

                            result.Employees.Add(New SqlCapacityEmployee With {
                                .EmployeeId = SqlText(reader("EmployeeId")),
                                .EmployeeName = employeeName
                            })
                        End While
                    End Using
                End Using
            End If

            Dim activeEmployees = result.Employees.
                GroupBy(Function(employee) employee.EmployeeId, StringComparer.OrdinalIgnoreCase).
                ToDictionary(Function(group) group.Key, Function(group) group.First().EmployeeName, StringComparer.OrdinalIgnoreCase)

            If TableExists(connection, "dbo.Employee_Capacity") Then
                Using command = connection.CreateCommand()
                    command.CommandText =
                        "SELECT LTRIM(RTRIM(CAST([Employee ID] AS NVARCHAR(100)))) AS EmployeeId, [Work Date], [Available Hours] " &
                        "FROM dbo.Employee_Capacity " &
                        "WHERE [Work Date] BETWEEN @StartDate AND @FinishDate " &
                        "ORDER BY [Employee ID], [Work Date]"
                    AddParameter(command, "@StartDate", startDate.Date)
                    AddParameter(command, "@FinishDate", finishDate.Date)

                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            If reader("Work Date") Is DBNull.Value Then
                                Continue While
                            End If

                            Dim employeeId = SqlText(reader("EmployeeId"))
                            Dim employeeName = ""
                            If employeeId.Length = 0 OrElse Not activeEmployees.TryGetValue(employeeId, employeeName) Then
                                Continue While
                            End If

                            result.Availability.Add(New SqlCapacityAvailability With {
                                .EmployeeId = employeeId,
                                .EmployeeName = employeeName,
                                .WorkDate = CDate(reader("Work Date")).Date,
                                .AvailableHours = If(reader("Available Hours") Is DBNull.Value, 8D, Math.Max(0D, CDec(reader("Available Hours"))))
                            })
                        End While
                    End Using
                End Using
            End If

            If TableExists(connection, "dbo.Project Schedule Table") Then
                Dim projectNames = LoadProjectNameLookup(connection)

                Using command = connection.CreateCommand()
                    command.CommandText =
                        "SELECT LTRIM(RTRIM(CAST([Employee ID] AS NVARCHAR(100)))) AS EmployeeId, [Schedule Date], [Planned Hours], [ProjectID at SMA], [Task Name], [Task Order] " &
                        "FROM [dbo].[Project Schedule Table] " &
                        "WHERE [Schedule Date] BETWEEN @StartDate AND @FinishDate " &
                        "ORDER BY [Employee ID], [Schedule Date], [ProjectID at SMA], [Task Order]"
                    AddParameter(command, "@StartDate", startDate.Date)
                    AddParameter(command, "@FinishDate", finishDate.Date)

                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            If reader("Schedule Date") Is DBNull.Value Then
                                Continue While
                            End If

                            Dim employeeId = SqlText(reader("EmployeeId"))
                            Dim employeeName = ""
                            If employeeId.Length = 0 OrElse Not activeEmployees.TryGetValue(employeeId, employeeName) Then
                                Continue While
                            End If

                            Dim scheduleProjectCode = SqlText(reader("ProjectID at SMA"))
                            Dim scheduleProjectName = scheduleProjectCode
                            If projectNames.ContainsKey(scheduleProjectCode) Then
                                scheduleProjectName = projectNames(scheduleProjectCode)
                            End If

                            result.Assignments.Add(New SqlCapacityAssignment With {
                                .EmployeeId = employeeId,
                                .EmployeeName = employeeName,
                                .WorkDate = CDate(reader("Schedule Date")).Date,
                                .PlannedHours = If(reader("Planned Hours") Is DBNull.Value, 0D, Math.Max(0D, CDec(reader("Planned Hours")))),
                                .ProjectIdAtSma = scheduleProjectCode,
                                .ProjectName = scheduleProjectName,
                                .TaskName = SqlText(reader("Task Name")),
                                .TaskOrder = If(reader("Task Order") Is DBNull.Value, 0, CInt(reader("Task Order")))
                            })
                        End While
                    End Using
                End Using
            End If
        End Using

        Return result
    End Function

    Public Function LoadTaskCatalog() As List(Of TaskCatalogItem)
        Return LoadTaskTemplates("", "")
    End Function

    Public Function LoadTaskTemplates(templateName As String, projectSize As String, Optional reportType As String = "") As List(Of TaskCatalogItem)
        Dim result As New List(Of TaskCatalogItem)()

        Using connection As SqlConnection = ActiveSQLConnection()
            connection.Open()
            ValidateExcelSqlDesign(connection, requireScheduleSaveTables:=False)

            Dim taskTemplateTable = ResolveTaskTemplateTable(connection)
            If taskTemplateTable.Length = 0 Then
                Return result
            End If

            If Not ColumnExists(connection, taskTemplateTable, "Task ID") Then
                Return result
            End If

            Return LoadTaskTemplatesFromDatabaseDesign(connection, taskTemplateTable, templateName, projectSize, reportType)
        End Using

        Return result
    End Function


    Private Function LoadTaskTemplatesFromDatabaseDesign(connection As IDbConnection, tableName As String, projectType As String, projectSize As String, reportType As String) As List(Of TaskCatalogItem)
        Dim result As New List(Of TaskCatalogItem)()
        Dim projectTypeFilter = If(projectType, "").Trim()
        Dim allowedReports = AllowedTaskReportTypes(reportType)

        Using command = connection.CreateCommand()
            command.CommandText =
                "SELECT [Task ID], [Task Name], [Small (hrs)], [Medium (hrs)], [Large (hrs)], [Very Large (hrs)], [Project Type], [Task Order], [IsActive], [Stage], [Responsibity], [Type of Report] " &
                "FROM " & SqlTableName(tableName) & " " &
                "WHERE ISNULL([IsActive], 1) = 1 " &
                "AND (@ProjectType = '' OR ISNULL([Project Type], 'New') = @ProjectType OR (@ProjectType = 'New' AND ISNULL([Project Type], 'New') IN ('New/Update', 'Update')) OR (@ProjectType = 'Feedback' AND ISNULL([Project Type], 'New') IN ('Feedback', 'Update'))) " &
                "AND (@ReportFilter = '' OR ISNULL([Type of Report], 'ALL') IN (" & SqlInList(allowedReports) & ")) " &
                "ORDER BY [Task Order], [Task ID]"
            AddParameter(command, "@ProjectType", projectTypeFilter)
            AddParameter(command, "@ReportFilter", If(allowedReports.Count = 0, "", "1"))

            Using reader = command.ExecuteReader()
                While reader.Read()
                    Dim taskName = Convert.ToString(reader("Task Name"), CultureInfo.InvariantCulture).Trim()
                    If taskName.Length = 0 Then
                        Continue While
                    End If

                    result.Add(New TaskCatalogItem With {
                        .DatabaseTaskId = If(reader("Task ID") Is DBNull.Value, 0, CInt(reader("Task ID"))),
                        .Title = taskName,
                        .Predecessor = "Previous Task",
                        .DependencyType = "FS",
                        .Summary = If(reader("Stage") Is DBNull.Value, "", CStr(reader("Stage"))),
                        .SmallHours = If(reader("Small (hrs)") Is DBNull.Value, 0D, CDec(reader("Small (hrs)"))),
                        .MediumHours = If(reader("Medium (hrs)") Is DBNull.Value, 0D, CDec(reader("Medium (hrs)"))),
                        .LargeHours = If(reader("Large (hrs)") Is DBNull.Value, 0D, CDec(reader("Large (hrs)"))),
                        .VeryLargeHours = If(reader("Very Large (hrs)") Is DBNull.Value, 0D, CDec(reader("Very Large (hrs)"))),
                        .Assignee = If(reader("Responsibity") Is DBNull.Value, "", CStr(reader("Responsibity"))),
                        .ModuleId = 0,
                        .ProjectType = If(reader("Project Type") Is DBNull.Value, "", CStr(reader("Project Type"))),
                        .TypeOfReport = If(reader("Type of Report") Is DBNull.Value, "", CStr(reader("Type of Report"))),
                        .TaskOrder = If(reader("Task Order") Is DBNull.Value, 0, CInt(reader("Task Order")))
                    })
                End While
            End Using
        End Using

        Return result
    End Function

    Private Function BuildTaskAssignmentRows(task As ScheduleTask) As List(Of AssignmentPersistRow)
        Dim rows As New List(Of AssignmentPersistRow)()
        If task Is Nothing Then
            Return rows
        End If

        Dim daily = ParseDailyAllocations(task.DailyResourceAllocations)
        If daily.Count = 0 Then
            daily = BuildFallbackDailyAllocations(task)
        End If

        For Each item In daily
            Dim pieces = item.Key.Split({"|"c}, 2, StringSplitOptions.None)
            If pieces.Length <> 2 Then
                Continue For
            End If

            Dim workDate As Date
            If Not Date.TryParseExact(pieces(1), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, workDate) Then
                Continue For
            End If

            rows.Add(New AssignmentPersistRow With {
                .EmployeeName = pieces(0).Trim(),
                .WorkDate = workDate.Date,
                .AssignedHours = Math.Max(0D, item.Value)
            })
        Next

        Return rows.Where(Function(row) row.AssignedHours > 0D).ToList()
    End Function

    Private Function SaveProjectScheduleRows(connection As IDbConnection, projectIdAtSma As String, projectVersion As String, tasks As BindingList(Of ScheduleTask)) As List(Of SavedExcelScheduleRow)
        Dim savedRows As New List(Of SavedExcelScheduleRow)()
        Dim employeeLookup = LoadEmployeeIdLookup(connection)
        Dim missingEmployees As New List(Of String)()
        Dim scheduleIdIsIdentity = IsIdentityColumn(connection, "dbo.Project Schedule Table", "Schedule ID")
        Dim existingRows = LoadExistingProjectScheduleRows(connection, projectIdAtSma, projectVersion)
        Dim usedScheduleIds As New HashSet(Of Integer)()

        For Each task In tasks
            Dim assignmentRows = BuildTaskAssignmentRows(task)
            For Each assignmentRow In assignmentRows
                If String.IsNullOrWhiteSpace(assignmentRow.EmployeeName) Then
                    Continue For
                End If

                If Not employeeLookup.ContainsKey(assignmentRow.EmployeeName) Then
                    If Not missingEmployees.Contains(assignmentRow.EmployeeName, StringComparer.OrdinalIgnoreCase) Then
                        missingEmployees.Add(assignmentRow.EmployeeName)
                    End If
                    Continue For
                End If

                Dim employeeId = employeeLookup(assignmentRow.EmployeeName)
                Dim existingRow = FindReusableScheduleRow(existingRows, usedScheduleIds, task.TaskId, assignmentRow.WorkDate, employeeId)
                If existingRow IsNot Nothing Then
                    UpdateProjectScheduleRow(connection, existingRow.ScheduleId, task, employeeId, assignmentRow)
                    usedScheduleIds.Add(existingRow.ScheduleId)
                Else
                    InsertProjectScheduleRow(connection, projectIdAtSma, projectVersion, task, employeeId, assignmentRow, scheduleIdIsIdentity)
                End If

                savedRows.Add(New SavedExcelScheduleRow With {
                    .EmployeeId = employeeId,
                    .WorkDate = assignmentRow.WorkDate.Date
                })
            Next
        Next

        If missingEmployees.Count > 0 Then
            Throw New InvalidOperationException("Employee IDs were not found in Employees_Master for: " & String.Join(", ", missingEmployees.OrderBy(Function(name) name, StringComparer.OrdinalIgnoreCase)))
        End If

        DeleteRemovedProjectScheduleRows(connection, existingRows, usedScheduleIds)

        Return savedRows.
            GroupBy(Function(row) row.EmployeeId.ToString(CultureInfo.InvariantCulture) & "|" & row.WorkDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)).
            Select(Function(group) group.First()).
            ToList()
    End Function

    Private Function LoadExistingProjectScheduleRows(connection As IDbConnection, projectIdAtSma As String, projectVersion As String) As List(Of ExistingProjectScheduleRow)
        Dim rows As New List(Of ExistingProjectScheduleRow)()

        Using GetExistingProjectSchedule As IDbCommand = connection.CreateCommand()
            GetExistingProjectSchedule.CommandText =
                "SELECT [Schedule ID], [Task Order], [Employee ID], [Schedule Date] " &
                "FROM [dbo].[Project Schedule Table] " &
                "WHERE [ProjectID at SMA] = @ProjectIdAtSma AND [Version] = @VersionNumber " &
                "ORDER BY [Task Order], [Schedule Date], [Schedule ID]"
            AddParameter(GetExistingProjectSchedule, "@ProjectIdAtSma", projectIdAtSma)
            AddParameter(GetExistingProjectSchedule, "@VersionNumber", projectVersion)

            Using reader = GetExistingProjectSchedule.ExecuteReader()
                While reader.Read()
                    If reader("Schedule ID") Is DBNull.Value OrElse reader("Task Order") Is DBNull.Value OrElse reader("Schedule Date") Is DBNull.Value Then
                        Continue While
                    End If

                    rows.Add(New ExistingProjectScheduleRow With {
                        .ScheduleId = CInt(reader("Schedule ID")),
                        .TaskOrder = CInt(reader("Task Order")),
                        .EmployeeId = If(reader("Employee ID") Is DBNull.Value, 0, CInt(reader("Employee ID"))),
                        .ScheduleDate = CDate(reader("Schedule Date")).Date
                    })
                End While
            End Using
        End Using

        Return rows
    End Function

    Private Function FindReusableScheduleRow(existingRows As List(Of ExistingProjectScheduleRow), usedScheduleIds As HashSet(Of Integer), taskOrder As Integer, scheduleDate As Date, employeeId As Integer) As ExistingProjectScheduleRow
        Dim exactMatch = existingRows.FirstOrDefault(Function(row) Not usedScheduleIds.Contains(row.ScheduleId) AndAlso row.TaskOrder = taskOrder AndAlso row.ScheduleDate.Date = scheduleDate.Date AndAlso row.EmployeeId = employeeId)
        If exactMatch IsNot Nothing Then
            Return exactMatch
        End If

        Return existingRows.FirstOrDefault(Function(row) Not usedScheduleIds.Contains(row.ScheduleId) AndAlso row.TaskOrder = taskOrder AndAlso row.ScheduleDate.Date = scheduleDate.Date)
    End Function

    Private Sub UpdateProjectScheduleRow(connection As IDbConnection, scheduleId As Integer, task As ScheduleTask, employeeId As Integer, assignmentRow As AssignmentPersistRow)
        Using UpdateProjectSchedule As IDbCommand = connection.CreateCommand()
            UpdateProjectSchedule.CommandText =
                "UPDATE [dbo].[Project Schedule Table] " &
                "SET [Task Name] = @TaskName, [Task Order] = @TaskOrder, [Employee ID] = @EmployeeId, [Planned Hours] = @PlannedHours, [Schedule Date] = @ScheduleDate, [Status] = @Status, [Start Date ] = @StartDate, [End Date ] = @EndDate, [Planned On] = @PlannedOn, [Planned By] = @PlannedBy " &
                "WHERE [Schedule ID] = @ScheduleId"
            AddProjectScheduleParameters(UpdateProjectSchedule, task, employeeId, assignmentRow)
            AddParameter(UpdateProjectSchedule, "@ScheduleId", scheduleId)
            UpdateProjectSchedule.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub InsertProjectScheduleRow(connection As IDbConnection, projectIdAtSma As String, projectVersion As String, task As ScheduleTask, employeeId As Integer, assignmentRow As AssignmentPersistRow, scheduleIdIsIdentity As Boolean)
        Using InsertProjectSchedule As IDbCommand = connection.CreateCommand()
            If scheduleIdIsIdentity Then
                InsertProjectSchedule.CommandText =
                    "INSERT INTO [dbo].[Project Schedule Table] " &
                    "([ProjectID at SMA], [Version], [Task Name], [Task Order], [Employee ID], [Planned Hours], [Schedule Date], [Status], [Start Date ], [End Date ], [Planned On], [Planned By]) " &
                    "VALUES (@ProjectIdAtSma, @VersionNumber, @TaskName, @TaskOrder, @EmployeeId, @PlannedHours, @ScheduleDate, @Status, @StartDate, @EndDate, @PlannedOn, @PlannedBy)"
            Else
                InsertProjectSchedule.CommandText =
                    "INSERT INTO [dbo].[Project Schedule Table] " &
                    "([Schedule ID], [ProjectID at SMA], [Version], [Task Name], [Task Order], [Employee ID], [Planned Hours], [Schedule Date], [Status], [Start Date ], [End Date ], [Planned On], [Planned By]) " &
                    "VALUES ((SELECT ISNULL(MAX([Schedule ID]), 0) + 1 FROM [dbo].[Project Schedule Table]), @ProjectIdAtSma, @VersionNumber, @TaskName, @TaskOrder, @EmployeeId, @PlannedHours, @ScheduleDate, @Status, @StartDate, @EndDate, @PlannedOn, @PlannedBy)"
            End If

            AddParameter(InsertProjectSchedule, "@ProjectIdAtSma", projectIdAtSma)
            AddParameter(InsertProjectSchedule, "@VersionNumber", projectVersion)
            AddProjectScheduleParameters(InsertProjectSchedule, task, employeeId, assignmentRow)
            InsertProjectSchedule.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub AddProjectScheduleParameters(command As IDbCommand, task As ScheduleTask, employeeId As Integer, assignmentRow As AssignmentPersistRow)
        AddParameter(command, "@TaskName", task.TaskName)
        AddParameter(command, "@TaskOrder", task.TaskId)
        AddParameter(command, "@EmployeeId", employeeId)
        AddParameter(command, "@PlannedHours", assignmentRow.AssignedHours)
        AddParameter(command, "@ScheduleDate", assignmentRow.WorkDate)
        AddParameter(command, "@Status", If(task.PercentComplete >= 100, "Completed", "Inprogress"))
        AddParameter(command, "@StartDate", task.StartDate.Date)
        AddParameter(command, "@EndDate", task.FinishDate.Date)
        AddParameter(command, "@PlannedOn", Date.Now)
        AddParameter(command, "@PlannedBy", Environment.UserName)
    End Sub

    Private Sub DeleteRemovedProjectScheduleRows(connection As IDbConnection, existingRows As List(Of ExistingProjectScheduleRow), usedScheduleIds As HashSet(Of Integer))
        For Each existingRow In existingRows.Where(Function(row) Not usedScheduleIds.Contains(row.ScheduleId))
            Using DeleteProjectSchedule As IDbCommand = connection.CreateCommand()
                DeleteProjectSchedule.CommandText = "DELETE FROM [dbo].[Project Schedule Table] WHERE [Schedule ID] = @ScheduleId"
                AddParameter(DeleteProjectSchedule, "@ScheduleId", existingRow.ScheduleId)
                DeleteProjectSchedule.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Function LoadScheduleCapacityRows(connection As IDbConnection, projectIdAtSma As String, projectVersion As String) As List(Of SavedExcelScheduleRow)
        Dim rows As New List(Of SavedExcelScheduleRow)()

        Using GetExistingProjectSchedule As IDbCommand = connection.CreateCommand()
            GetExistingProjectSchedule.CommandText =
                "SELECT DISTINCT [Employee ID], [Schedule Date] " &
                "FROM [dbo].[Project Schedule Table] " &
                "WHERE [ProjectID at SMA] = @ProjectIdAtSma AND [Version] = @VersionNumber"
            AddParameter(GetExistingProjectSchedule, "@ProjectIdAtSma", projectIdAtSma)
            AddParameter(GetExistingProjectSchedule, "@VersionNumber", projectVersion)

            Using reader = GetExistingProjectSchedule.ExecuteReader()
                While reader.Read()
                    If reader("Employee ID") Is DBNull.Value OrElse reader("Schedule Date") Is DBNull.Value Then
                        Continue While
                    End If

                    rows.Add(New SavedExcelScheduleRow With {
                        .EmployeeId = CInt(reader("Employee ID")),
                        .WorkDate = CDate(reader("Schedule Date")).Date
                    })
                End While
            End Using
        End Using

        Return rows
    End Function

    Private Sub UpdateEmployeeCapacityFromSchedule(connection As IDbConnection, savedRows As List(Of SavedExcelScheduleRow))
        For Each row In savedRows.
            GroupBy(Function(item) item.EmployeeId.ToString(CultureInfo.InvariantCulture) & "|" & item.WorkDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)).
            Select(Function(group) group.First())
            Using UpdateEmployeeCapacity As IDbCommand = connection.CreateCommand()
                UpdateEmployeeCapacity.CommandText =
                    "UPDATE c " &
                    "SET [Planned Hours] = ISNULL(s.PlannedHours, 0), " &
                    "[Available Hours] = CASE WHEN ISNULL(c.[Default Hours], 0) - ISNULL(c.[Leave Hours], 0) - ISNULL(c.[Training hours], 0) - ISNULL(c.[Meeting Hours], 0) - ISNULL(s.PlannedHours, 0) < 0 THEN 0 ELSE ISNULL(c.[Default Hours], 0) - ISNULL(c.[Leave Hours], 0) - ISNULL(c.[Training hours], 0) - ISNULL(c.[Meeting Hours], 0) - ISNULL(s.PlannedHours, 0) END " &
                    "FROM dbo.Employee_Capacity c " &
                    "OUTER APPLY (SELECT SUM(CAST(ISNULL([Planned Hours], 0) AS DECIMAL(12,2))) AS PlannedHours FROM [dbo].[Project Schedule Table] WHERE [Employee ID] = c.[Employee ID] AND [Schedule Date] = c.[Work Date]) s " &
                    "WHERE c.[Employee ID] = @EmployeeId AND c.[Work Date] = @WorkDate"
                AddParameter(UpdateEmployeeCapacity, "@EmployeeId", row.EmployeeId)
                AddParameter(UpdateEmployeeCapacity, "@WorkDate", row.WorkDate)
                UpdateEmployeeCapacity.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Function BuildFallbackDailyAllocations(task As ScheduleTask) As Dictionary(Of String, Decimal)
        Dim result As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
        Dim resourceAllocations = ParseResourceAllocations(task.ResourceAllocations)
        Dim dates = EnumerateAllDates(task.StartDate.Date, task.FinishDate.Date)
        If resourceAllocations.Count = 0 OrElse dates.Count = 0 Then
            Return result
        End If

        For Each resourceEntry In resourceAllocations
            Dim remaining = Math.Max(0D, resourceEntry.Value)
            Dim dailyShare = Math.Round(remaining / dates.Count, 2, MidpointRounding.AwayFromZero)
            For index = 0 To dates.Count - 1
                Dim workDate = dates(index)
                Dim hours = If(index = dates.Count - 1, remaining, dailyShare)
                hours = Math.Max(0D, Math.Round(hours, 2, MidpointRounding.AwayFromZero))
                If hours > 0D Then
                    result(resourceEntry.Key.Trim() & "|" & workDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)) = hours
                End If
                remaining -= hours
            Next
        Next

        Return result
    End Function

    Private Function LoadEmployeeIdLookup(connection As IDbConnection) As Dictionary(Of String, Integer)
        Dim result As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Using command = connection.CreateCommand()
            command.CommandText =
                "SELECT [Office Account], LTRIM(RTRIM(firstname)) AS EmployeeName FROM dbo.Employees_Master WHERE ISNULL(Active, 1) = 1 AND ISNULL(LTRIM(RTRIM(firstname)), '') <> ''"

            Using reader = command.ExecuteReader()
                While reader.Read()
                    If reader("Office Account") Is DBNull.Value OrElse reader("EmployeeName") Is DBNull.Value Then
                        Continue While
                    End If

                    Dim employeeName = CStr(reader("EmployeeName")).Trim()
                    If employeeName.Length > 0 AndAlso Not result.ContainsKey(employeeName) Then
                        result(employeeName) = CInt(reader("Office Account"))
                    End If
                End While
            End Using
        End Using

        Return result
    End Function

    Private Function LoadEmployeeNameLookupFromMaster(connection As IDbConnection) As Dictionary(Of Integer, String)
        Dim result As New Dictionary(Of Integer, String)()
        If Not TableExists(connection, "dbo.Employees_Master") Then
            Return result
        End If

        Using command = connection.CreateCommand()
            command.CommandText =
                "SELECT [Office Account], LTRIM(RTRIM(firstname)) AS EmployeeName FROM dbo.Employees_Master WHERE ISNULL(Active, 1) = 1 AND ISNULL(LTRIM(RTRIM(firstname)), '') <> ''"

            Using reader = command.ExecuteReader()
                While reader.Read()
                    If reader("Office Account") Is DBNull.Value OrElse reader("EmployeeName") Is DBNull.Value Then
                        Continue While
                    End If

                    Dim employeeId = CInt(reader("Office Account"))
                    If Not result.ContainsKey(employeeId) Then
                        result(employeeId) = CStr(reader("EmployeeName")).Trim()
                    End If
                End While
            End Using
        End Using

        Return result
    End Function

    Private Function LoadActiveEmployeeNameLookup(connection As IDbConnection) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        If Not TableExists(connection, "dbo.Employees_Master") Then
            Return result
        End If

        Using GetEmployees As IDbCommand = connection.CreateCommand()
            GetEmployees.CommandText =
                "SELECT LTRIM(RTRIM(CAST([Office Account] AS NVARCHAR(100)))) AS EmployeeId, LTRIM(RTRIM(firstname)) AS EmployeeName " &
                "FROM dbo.Employees_Master " &
                "WHERE ISNULL(Active, 1) = 1 AND ISNULL(LTRIM(RTRIM(firstname)), '') <> ''"

            Using reader = GetEmployees.ExecuteReader()
                While reader.Read()
                    Dim employeeId = SqlText(reader("EmployeeId"))
                    Dim employeeName = SqlText(reader("EmployeeName"))
                    If employeeId.Length > 0 AndAlso employeeName.Length > 0 AndAlso Not result.ContainsKey(employeeId) Then
                        result(employeeId) = employeeName
                    End If
                End While
            End Using
        End Using

        Return result
    End Function

    Private Function GetActualProjectSize(connection As IDbConnection, projectIdAtSma As String) As String
        Dim projectSizes = LoadActualProjectSizeLookup(connection)
        Dim projectSize = ""
        projectSizes.TryGetValue(If(projectIdAtSma, "").Trim(), projectSize)
        Return projectSize
    End Function

    Private Function LoadActualProjectSizeLookup(connection As IDbConnection) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        If Not TableExists(connection, "dbo.Table_Project_Tracking") Then
            Return result
        End If

        RequireColumns(connection, "dbo.Table_Project_Tracking", {"Project ID at SMA", "Project Size"})

        Using GetActualProjectSize As IDbCommand = connection.CreateCommand()
            GetActualProjectSize.CommandText =
                "SELECT [Project ID at SMA], [Project Size] " &
                "FROM dbo.Table_Project_Tracking " &
                "WHERE ISNULL(LTRIM(RTRIM(CAST([Project ID at SMA] AS NVARCHAR(100)))), '') <> ''"

            Using reader = GetActualProjectSize.ExecuteReader()
                While reader.Read()
                    Dim projectCode = SqlText(reader("Project ID at SMA"))
                    If projectCode.Length = 0 Then
                        Continue While
                    End If

                    result(projectCode) = ProjectSizeNameFromSql(reader("Project Size"))
                End While
            End Using
        End Using

        Return result
    End Function

    Private Function LoadVersionProjectLookup(connection As IDbConnection) As Dictionary(Of String, SqlProjectPlanningInfo)
        Dim result As New Dictionary(Of String, SqlProjectPlanningInfo)(StringComparer.OrdinalIgnoreCase)

        Using GetProjectDetails As IDbCommand = connection.CreateCommand()
            GetProjectDetails.CommandText =
                "SELECT [Project Name], [Project ID at SMA], [Version], [Active], [Report BRE], [Report ROL], [Report Within], [Final Completion Date], [Planning Message], [ControlleratROLC], [Client Type], [Is_Pointcloud], [Teck Pack], [Deed Profile], [Shadow_Analysis], [Urgent Small Projects], [IsPlanned]   FROM [Version_Table]  WHERE [Project ID at SMA] AS NVARCHAR(100)))), '') <> ''"

            Using reader = GetProjectDetails.ExecuteReader()
                While reader.Read()
                    Dim projectCode = SqlText(reader("Project ID at SMA"))
                    If projectCode.Length = 0 Then
                        Continue While
                    End If

                    result(projectCode) = New SqlProjectPlanningInfo With {
                        .ProjectIdAtSma = projectCode,
                        .ProjectName = SqlText(reader("Project Name")),
                        .VersionNumber = SqlVersionText(reader("Version")),
                        .IsActive = SqlBoolean(reader("Active")),
                        .IsPlanned = SqlNullableBoolean(reader("IsPlanned")),
                        .ProjectType = ProjectTypeFromVersion(reader("Version")),
                        .ReportType = BuildReportTypeDisplay(reader("Report BRE"), reader("Report ROL"), reader("Report Within")),
                        .FinalCompletionDate = SqlNullableDate(reader("Final Completion Date")),
                        .PlanningMessage = SqlText(reader("Planning Message")),
                        .ControllerAtRolc = SqlText(reader("ControlleratROLC")),
                        .ClientType = SqlText(reader("Client Type")),
                        .IsPointcloud = SqlBoolean(reader("Is_Pointcloud")),
                        .TechPack = SqlBoolean(reader("Teck Pack")),
                        .DeedProfile = SqlBoolean(reader("Deed Profile")),
                        .ShadowAnalysis = SqlBoolean(reader("Shadow_Analysis")),
                        .UrgentSmallProjects = SqlBoolean(reader("Urgent Small Projects"))
                    }
                End While
            End Using
        End Using

        Return result
    End Function

    Private Function LoadProjectNameLookup(connection As IDbConnection) As Dictionary(Of String, String)
        Return LoadVersionProjectLookup(connection).
            ToDictionary(Function(pair) pair.Key, Function(pair) If(String.IsNullOrWhiteSpace(pair.Value.ProjectName), pair.Key, pair.Value.ProjectName), StringComparer.OrdinalIgnoreCase)
    End Function

    Private Sub LoadAvailabilityFromEmployeeCapacity(connection As IDbConnection, target As Dictionary(Of String, Dictionary(Of Date, Decimal)), startDate As Date, finishDate As Date)
        Dim employees = LoadActiveEmployeeNameLookup(connection)

        Using command = connection.CreateCommand()
            command.CommandText =
                "SELECT LTRIM(RTRIM(CAST([Employee ID] AS NVARCHAR(100)))) AS EmployeeId, [Work Date], [Available Hours] " &
                "FROM dbo.Employee_Capacity " &
                "WHERE [Work Date] BETWEEN @StartDate AND @FinishDate " &
                "ORDER BY [Employee ID], [Work Date]"
            AddParameter(command, "@StartDate", startDate.Date)
            AddParameter(command, "@FinishDate", finishDate.Date)

            Using reader = command.ExecuteReader()
                While reader.Read()
                    If reader("Work Date") Is DBNull.Value Then
                        Continue While
                    End If

                    Dim employeeId = SqlText(reader("EmployeeId"))
                    Dim resourceName = ""
                    If employeeId.Length = 0 OrElse Not employees.TryGetValue(employeeId, resourceName) Then
                        Continue While
                    End If

                    Dim workDate = CDate(reader("Work Date")).Date
                    Dim hours = If(reader("Available Hours") Is DBNull.Value, 8D, CDec(reader("Available Hours")))

                    Dim byDate As Dictionary(Of Date, Decimal) = Nothing
                    If Not target.TryGetValue(resourceName, byDate) Then
                        byDate = New Dictionary(Of Date, Decimal)()
                        target(resourceName) = byDate
                    End If

                    byDate(workDate) = Math.Max(0D, hours)
                End While
            End Using
        End Using
    End Sub

    Private Sub ValidateExcelSqlDesign(connection As IDbConnection, requireScheduleSaveTables As Boolean)
        Dim requiredTables As New List(Of String) From {
            "dbo.Version_Table",
            "dbo.Table_Project_Tracking",
            "dbo.Employees_Master",
            "dbo.Task_Template"
        }

        If requireScheduleSaveTables Then
            requiredTables.Add("dbo.Project Schedule Table")
            requiredTables.Add("dbo.Employee_Capacity")
        End If

        For Each tableName In requiredTables
            If Not TableExists(connection, tableName) Then
                Throw New InvalidOperationException("Required SQL table is missing: " & tableName & ". Please create the tables from SMA_Database_Design before using the planner.")
            End If
        Next

        RequireColumns(connection, "dbo.Version_Table", {"Project Name", "Project ID at SMA", "Version", "Active", "Report BRE", "Report ROL", "Report Within", "Final Completion Date", "Planning Message", "ControlleratROLC", "Client Type", "Is_Pointcloud", "Teck Pack", "Deed Profile", "Shadow_Analysis", "Urgent Small Projects", "IsPlanned"})
        RequireColumns(connection, "dbo.Table_Project_Tracking", {"Project ID at SMA", "Project Size"})
        RequireColumns(connection, "dbo.Employees_Master", {"Office Account", "firstname", "Active"})
        RequireColumns(connection, "dbo.Task_Template", {"Task ID", "Task Name", "Small (hrs)", "Medium (hrs)", "Large (hrs)", "Very Large (hrs)", "Project Type", "Task Order", "IsActive", "Stage", "Responsibity", "Type of Report"})

        If requireScheduleSaveTables Then
            RequireColumns(connection, "dbo.Project Schedule Table", {"Schedule ID", "ProjectID at SMA", "Version", "Task Name", "Task Order", "Employee ID", "Planned Hours", "Schedule Date", "Status", "Start Date ", "End Date ", "Planned On", "Planned By"})
            RequireColumns(connection, "dbo.Employee_Capacity", {"Capacity ID", "Employee ID", "Work Date", "Default Hours", "Leave Hours", "Training hours", "Meeting Hours", "Available Hours", "Planned Hours"})
        End If
    End Sub

    Private Sub RequireColumns(connection As IDbConnection, tableName As String, columnNames As IEnumerable(Of String))
        Dim missingColumns = columnNames.
            Where(Function(columnName) Not ColumnExists(connection, tableName, columnName)).
            ToList()

        If missingColumns.Count > 0 Then
            Throw New InvalidOperationException("Required SQL column(s) missing in " & tableName & ": " & String.Join(", ", missingColumns))
        End If
    End Sub

    Private Function IsIdentityColumn(connection As IDbConnection, tableName As String, columnName As String) As Boolean
        Using CheckIdentityColumn As IDbCommand = connection.CreateCommand()
            CheckIdentityColumn.CommandText = "SELECT COLUMNPROPERTY(OBJECT_ID(@TableName), @ColumnName, 'IsIdentity')"
            AddParameter(CheckIdentityColumn, "@TableName", tableName)
            AddParameter(CheckIdentityColumn, "@ColumnName", columnName)
            Dim value = CheckIdentityColumn.ExecuteScalar()
            Return value IsNot Nothing AndAlso value IsNot DBNull.Value AndAlso Convert.ToInt32(value, CultureInfo.InvariantCulture) = 1
        End Using
    End Function

    Private Function TableExists(connection As IDbConnection, tableName As String) As Boolean
        Using command = connection.CreateCommand()
            command.CommandText = "SELECT CASE WHEN OBJECT_ID('" & tableName & "', 'U') IS NULL THEN 0 ELSE 1 END"
            Return Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture) = 1
        End Using
    End Function

    Private Function ColumnExists(connection As IDbConnection, tableName As String, columnName As String) As Boolean
        Using command = connection.CreateCommand()
            command.CommandText = "SELECT CASE WHEN COL_LENGTH(@TableName, @ColumnName) IS NULL THEN 0 ELSE 1 END"
            AddParameter(command, "@TableName", tableName)
            AddParameter(command, "@ColumnName", columnName)
            Return Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture) = 1
        End Using
    End Function

    Private Function TableHasRows(connection As IDbConnection, tableName As String) As Boolean
        If Not TableExists(connection, tableName) Then
            Return False
        End If

        Using command = connection.CreateCommand()
            command.CommandText = "SELECT TOP 1 1 FROM " & SqlTableName(tableName)
            Dim value = command.ExecuteScalar()
            Return value IsNot Nothing AndAlso value IsNot DBNull.Value
        End Using
    End Function

    Private Function ResolveTaskTemplateTable(connection As IDbConnection) As String
        If TableExists(connection, "dbo.Task_Template") Then
            Return "dbo.Task_Template"
        End If

        Return ""
    End Function

    Private Shared Function SqlTableName(tableName As String) As String
        Dim value = If(tableName, "").Trim()
        If value.Length = 0 Then
            Return value
        End If

        Dim parts = value.Split("."c)
        If parts.Length = 2 Then
            Return "[" & parts(0).Trim("["c, "]"c) & "].[" & parts(1).Trim("["c, "]"c) & "]"
        End If

        Return "[" & value.Trim("["c, "]"c) & "]"
    End Function

    Private Shared Function SqlBoolean(value As Object) As Boolean
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return False
        End If

        If TypeOf value Is Boolean Then
            Return CBool(value)
        End If

        Dim intValue As Integer
        If Integer.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), intValue) Then
            Return intValue <> 0
        End If

        Return String.Equals(Convert.ToString(value, CultureInfo.InvariantCulture), "true", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function SqlNullableBoolean(value As Object) As Boolean?
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return Nothing
        End If

        Return SqlBoolean(value)
    End Function

    Private Shared Function SqlText(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return ""
        End If

        Dim text = Convert.ToString(value, CultureInfo.InvariantCulture)
        If String.Equals(text, "null", StringComparison.OrdinalIgnoreCase) Then
            Return ""
        End If

        Return If(text, "").Trim()
    End Function

    Private Shared Function SqlNullableDate(value As Object) As Date?
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return Nothing
        End If

        Dim parsedDate As Date
        If Date.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) OrElse
            Date.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture, DateTimeStyles.None, parsedDate) Then
            Return parsedDate.Date
        End If

        Return Nothing
    End Function

    Private Shared Function SqlVersionText(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return ""
        End If

        Dim decimalValue As Decimal
        If Decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, decimalValue) Then
            Return decimalValue.ToString("0.0##", CultureInfo.InvariantCulture)
        End If

        Dim text = Convert.ToString(value, CultureInfo.InvariantCulture)
        Return If(String.IsNullOrWhiteSpace(text), "", text.Trim())
    End Function

    Private Shared Function RequiredSqlVersionText(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return ""
        End If

        Dim decimalValue As Decimal
        If Decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, decimalValue) Then
            Return decimalValue.ToString("0.0##", CultureInfo.InvariantCulture)
        End If

        Return SqlText(value)
    End Function

    Private Shared Function ProjectTypeFromVersion(value As Object) As String
        Dim versionText = SqlVersionText(value)
        Dim versionValue As Decimal
        If Decimal.TryParse(versionText, NumberStyles.Number, CultureInfo.InvariantCulture, versionValue) Then
            Dim versionFraction = versionValue - Decimal.Truncate(versionValue)

            If versionFraction = 0D Then
                Return "New"
            End If

            If versionFraction = 0.1D Then
                Return "Feedback"
            End If
        End If

        Return "New"
    End Function

    Public Shared Function ProjectSizeName(sizeValue As Integer) As String
        Select Case sizeValue
            Case 1
                Return "Small"
            Case 2
                Return "Medium"
            Case 3
                Return "Large"
            Case 4
                Return "Very Large"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function ProjectSizeNameFromSql(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return ""
        End If

        Dim sizeText = SqlText(value)
        If sizeText.Length = 0 Then
            Return ""
        End If

        For Each sizeName In {"Small", "Medium", "Large", "Very Large"}
            If String.Equals(sizeText, sizeName, StringComparison.OrdinalIgnoreCase) Then
                Return sizeName
            End If
        Next

        Dim sizeValue As Integer
        If Not Integer.TryParse(sizeText, sizeValue) Then
            Return ""
        End If

        Select Case sizeValue
            Case 1, 2, 3, 4
                Return ProjectSizeName(sizeValue)
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function BuildReportTypeDisplay(reportBre As Object, reportRol As Object, reportWithin As Object) As String
        Dim parts As New List(Of String)()
        If SqlBoolean(reportBre) Then
            parts.Add("BRE")
        End If
        If SqlBoolean(reportRol) Then
            parts.Add("ROL")
        End If
        If SqlBoolean(reportWithin) Then
            parts.Add("Within")
        End If

        Return String.Join("/", parts)
    End Function

    Private Shared Function AllowedTaskReportTypes(reportType As String) As List(Of String)
        Dim normalized = If(reportType, "").Trim()
        Dim result As New List(Of String)()

        If normalized.Length = 0 Then
            Return result
        End If

        If normalized.IndexOf("BRE", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
            normalized.IndexOf("ROL", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
            normalized.IndexOf("Within", StringComparison.OrdinalIgnoreCase) >= 0 Then
            result.Add("ALL")
        End If

        If normalized.IndexOf("Within", StringComparison.OrdinalIgnoreCase) >= 0 Then
            result.Add("WITHIN")
        End If

        If normalized.IndexOf("Shadow", StringComparison.OrdinalIgnoreCase) >= 0 Then
            result.Add("SHADOW ANALYSIS")
        End If

        If normalized.IndexOf("Deed", StringComparison.OrdinalIgnoreCase) >= 0 Then
            result.Add("Deed Profile")
        End If

        Return result.Distinct(StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Private Shared Function SqlInList(values As IEnumerable(Of String)) As String
        Dim cleaned = values.
            Where(Function(value) Not String.IsNullOrWhiteSpace(value)).
            Select(Function(value) "'" & value.Replace("'", "''").Trim() & "'").
            ToList()

        If cleaned.Count = 0 Then
            Return "''"
        End If

        Return String.Join(",", cleaned)
    End Function

    Private Shared Function ParseResourceAllocations(value As String) As Dictionary(Of String, Decimal)
        Dim result As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(value) Then
            Return result
        End If

        For Each part In value.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim pieces = part.Split({"="c}, 2, StringSplitOptions.None)
            If pieces.Length <> 2 Then
                Continue For
            End If

            Dim hours As Decimal
            If Decimal.TryParse(pieces(1).Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, hours) OrElse
                Decimal.TryParse(pieces(1).Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, hours) Then
                Dim name = pieces(0).Trim()
                If name.Length > 0 Then
                    result(name) = Math.Max(0D, hours)
                End If
            End If
        Next

        Return result
    End Function

    Private Shared Function ParseDailyAllocations(value As String) As Dictionary(Of String, Decimal)
        Dim result As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(value) Then
            Return result
        End If

        For Each part In value.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim pieces = part.Split({"="c}, 2, StringSplitOptions.None)
            If pieces.Length <> 2 Then
                Continue For
            End If

            Dim hours As Decimal
            If Decimal.TryParse(pieces(1).Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, hours) OrElse
                Decimal.TryParse(pieces(1).Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, hours) Then
                Dim key = pieces(0).Trim()
                If key.Length > 0 Then
                    result(key) = Math.Max(0D, hours)
                End If
            End If
        Next

        Return result
    End Function

    Private Shared Function BuildResourceAllocationsString(resourceHours As Dictionary(Of String, Decimal)) As String
        Return String.Join("; ", resourceHours.
            Where(Function(item) Not String.IsNullOrWhiteSpace(item.Key) AndAlso item.Value > 0D).
            Select(Function(item) item.Key.Trim() & "=" & item.Value.ToString("0.##", CultureInfo.InvariantCulture)))
    End Function

    Private Shared Function BuildDailyAllocationsString(daily As Dictionary(Of String, Decimal)) As String
        Return String.Join("; ", daily.
            Where(Function(item) Not String.IsNullOrWhiteSpace(item.Key) AndAlso item.Value > 0D).
            Select(Function(item) item.Key.Trim() & "=" & item.Value.ToString("0.##", CultureInfo.InvariantCulture)))
    End Function

    Private Shared Function EnumerateAllDates(startDate As Date, finishDate As Date) As List(Of Date)
        Dim dates As New List(Of Date)()
        Dim currentDate = startDate.Date
        Dim lastDate = If(finishDate.Date < currentDate, currentDate, finishDate.Date)
        While currentDate <= lastDate
            dates.Add(currentDate)
            currentDate = currentDate.AddDays(1)
        End While
        Return dates
    End Function


    Private Sub AddParameter(command As IDbCommand, name As String, value As Object)
        Dim parameter = command.CreateParameter()
        parameter.ParameterName = name
        parameter.Value = If(value, DBNull.Value)
        command.Parameters.Add(parameter)
    End Sub


End Class

Public Class SqlProjectPlanningInfo
    Public Property ProjectIdAtSma As String = ""
    Public Property ProjectName As String = ""
    Public Property VersionNumber As String = ""
    Public Property IsActive As Boolean
    Public Property IsPlanned As Boolean?
    Public Property ProjectSize As String = ""
    Public Property ProjectType As String = ""
    Public Property ReportType As String = ""
    Public Property FinalCompletionDate As Date?
    Public Property PlanningMessage As String = ""
    Public Property ControllerAtRolc As String = ""
    Public Property ClientType As String = ""
    Public Property IsPointcloud As Boolean
    Public Property TechPack As Boolean
    Public Property DeedProfile As Boolean
    Public Property ShadowAnalysis As Boolean
    Public Property UrgentSmallProjects As Boolean
End Class

Public Class SqlCapacityPlanningData
    Public Property Employees As New List(Of SqlCapacityEmployee)()
    Public Property Availability As New List(Of SqlCapacityAvailability)()
    Public Property Assignments As New List(Of SqlCapacityAssignment)()
End Class

Public Class SqlCapacityEmployee
    Public Property EmployeeId As String = ""
    Public Property EmployeeName As String = ""
End Class

Public Class SqlCapacityAvailability
    Public Property EmployeeId As String = ""
    Public Property EmployeeName As String = ""
    Public Property WorkDate As Date
    Public Property AvailableHours As Decimal
End Class

Public Class SqlCapacityAssignment
    Public Property EmployeeId As String = ""
    Public Property EmployeeName As String = ""
    Public Property WorkDate As Date
    Public Property PlannedHours As Decimal
    Public Property ProjectIdAtSma As String = ""
    Public Property ProjectName As String = ""
    Public Property TaskName As String = ""
    Public Property TaskOrder As Integer
End Class

Friend Class AssignmentPersistRow
    Public Property EmployeeName As String = ""
    Public Property WorkDate As Date
    Public Property AssignedHours As Decimal
End Class

Friend Class SavedExcelScheduleRow
    Public Property EmployeeId As Integer
    Public Property WorkDate As Date
End Class

Friend Class ExistingProjectScheduleRow
    Public Property ScheduleId As Integer
    Public Property TaskOrder As Integer
    Public Property EmployeeId As Integer
    Public Property ScheduleDate As Date
End Class

Friend Class AssignmentSavedScheduleRow
    Public Property ProjectId As Integer
    Public Property ProjectName As String = ""
    Public Property VersionNumber As String = ""
    Public Property ProjectSize As String = ""
    Public Property ProjectType As String = "New"
    Public Property TaskId As Integer
    Public Property TaskName As String = ""
    Public Property TaskOrder As Integer
    Public Property EmployeeId As Integer
    Public Property WorkDate As Date
    Public Property AssignedHours As Decimal
    Public Property StartDate As Date?
    Public Property FinishDate As Date?
    Public Property DependencyTaskOrder As Integer?
    Public Property DependencyType As String = "FS"
    Public Property UpdatedOn As Date
End Class


