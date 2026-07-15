Imports System.ComponentModel
Imports System.Configuration
Imports System.Globalization
Imports System.ComponentModel.Design

Public Class SMAPlannerForm
    Inherits Form

    Private ReadOnly _sqlRepository As SqlProjectRepository = CreateSqlRepository()
    Private ReadOnly _liveProjectCatalog As LiveProjectCatalogService
    Private ReadOnly _projects As New BindingList(Of ProjectLibraryItem)()
    Private ReadOnly _searchProjectMatches As New List(Of LiveProjectItem)()
    Private _selectedSearchProject As LiveProjectItem
    Private _currentTheme As SchedulerThemePalette = SchedulerThemePalette.ThemeByName("Dusk")
    Private _isUpdatingSearchText As Boolean

    Public Sub New()
        _liveProjectCatalog = New LiveProjectCatalogService(_sqlRepository)
        InitializeComponent()
        ConfigurePlannerForm()
        If IsInDesignerHost() Then
            SeedPlannerDesignerData()
        Else
            InitializePlannerWithoutSql()
        End If
        ApplyCurrentTheme()
    End Sub

    Private Sub ConfigurePlannerForm()
        _liveProjectSearchBox.AutoCompleteMode = AutoCompleteMode.Suggest
        _liveProjectSearchBox.AutoCompleteSource = AutoCompleteSource.CustomSource
        _liveProjectSearchBox.MaxLength = 8
        _recentProjectSearchBox.MaxLength = 8

        _grid.AutoGenerateColumns = False
        _grid.DataSource = _projects
        _grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 46, 66)
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        _grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.0F)
        _grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
        _grid.RowTemplate.Height = 32
        _grid.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)
        _grid.DefaultCellStyle.Padding = New Padding(4, 2, 4, 2)
        _grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 235, 255)
        _grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(24, 31, 42)

    End Sub

    Private Sub InitializePlannerWithoutSql()
        _projects.Clear()
        _grid.DataSource = _projects
        RefreshSearchProjectSuggestions()
        UpdatePlanningSummary()
        SetPlannerStatus("Enter a Project ID and click Schedule Project to load project details from SQL.")
    End Sub

    Private Shared Function CreateSqlRepository() As SqlProjectRepository
        Dim connectionString = ""

        Try
            Dim settings = ConfigurationManager.ConnectionStrings("SmaSchedulerDb")
            If settings IsNot Nothing Then
                connectionString = settings.ConnectionString
            End If
        Catch ex As ConfigurationErrorsException
        End Try

        If String.IsNullOrWhiteSpace(connectionString) Then
            connectionString = Environment.GetEnvironmentVariable("SMA_SCHEDULER_SQL_CONNECTION")
        End If

        If String.IsNullOrWhiteSpace(connectionString) Then
            Return Nothing
        End If

        Return New SqlProjectRepository(connectionString)
    End Function

    Private Sub PlannerActivated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ApplyCurrentTheme()
    End Sub

    Private Sub ApplyCurrentTheme()
        _currentTheme = DeterminePlannerTheme()
        Dim theme = _currentTheme

        If theme Is Nothing OrElse IsDisposed Then
            Return
        End If

        BackColor = theme.WindowBack

        If ControlReady(headerPanel) Then
            headerPanel.BackColor = theme.HeaderBack
        End If

        If ControlReady(gridPanel) Then
            gridPanel.BackColor = theme.PanelBack
        End If

        If ControlReady(planningSummaryPanel) Then
            planningSummaryPanel.BackColor = theme.PanelBack
        End If

        For Each label In New Label() {titleLabel, listTitle, summaryTitleLabel}
            If ControlReady(label) Then
                label.ForeColor = theme.Text
            End If
        Next

        If ControlReady(_status) Then
            _status.ForeColor = theme.MutedText
        End If

        For Each label In New Label() {searchLabel, summaryPeriodLabel, recentSearchLabel}
            If ControlReady(label) Then
                label.ForeColor = theme.MutedText
            End If
        Next

        If ControlReady(btnScheduleProject) Then
            btnScheduleProject.BackColor = theme.Action
        End If

        For Each button In New Button() {btnScheduleProject}
            If ControlReady(button) Then
                button.ForeColor = Color.White
            End If
        Next

        If ControlReady(newProjectsPanel) Then
            newProjectsPanel.BackColor = theme.TileOne
        End If

        If ControlReady(updateProjectsPanel) Then
            updateProjectsPanel.BackColor = theme.TileThree
        End If

        If ControlReady(feedbackProjectsPanel) Then
            feedbackProjectsPanel.BackColor = theme.TileFour
        End If

        For Each label In New Label() {newProjectsLabel, newProjectsCountLabel, updateProjectsLabel, updateProjectsCountLabel, feedbackProjectsLabel, feedbackProjectsCountLabel}
            If ControlReady(label) Then
                label.ForeColor = theme.Text
            End If
        Next

        If _grid IsNot Nothing AndAlso Not _grid.IsDisposed Then
            Dim headerStyle = If(_grid.ColumnHeadersDefaultCellStyle, New DataGridViewCellStyle())
            Dim defaultStyle = If(_grid.DefaultCellStyle, New DataGridViewCellStyle())
            Dim alternatingStyle = If(_grid.AlternatingRowsDefaultCellStyle, New DataGridViewCellStyle())

            _grid.BackgroundColor = theme.PanelBack
            _grid.GridColor = theme.GridLine

            headerStyle.BackColor = theme.GridHeader
            headerStyle.ForeColor = Color.White
            _grid.ColumnHeadersDefaultCellStyle = headerStyle

            defaultStyle.BackColor = theme.PanelBack
            defaultStyle.ForeColor = theme.Text
            defaultStyle.SelectionBackColor = theme.Selection
            defaultStyle.SelectionForeColor = theme.Text
            _grid.DefaultCellStyle = defaultStyle

            alternatingStyle.BackColor = theme.AlternatingRow
            _grid.AlternatingRowsDefaultCellStyle = alternatingStyle
            _grid.EnableHeadersVisualStyles = False
        End If
    End Sub

    Private Function DeterminePlannerTheme() As SchedulerThemePalette
        Return SchedulerThemePalette.ThemeByName("Dusk")
    End Function

    Private Shared Function ControlReady(control As Control) As Boolean
        Return control IsNot Nothing AndAlso Not control.IsDisposed
    End Function

    Private Sub LiveProjectSearchTextChanged(sender As Object, e As EventArgs) Handles _liveProjectSearchBox.TextChanged
        If _isUpdatingSearchText Then
            Return
        End If
        RefreshSearchProjectSuggestions()
    End Sub

    Private Sub RefreshSearchProjectSuggestions()
        Dim query = If(_liveProjectSearchBox.Text, "").Trim()
        Dim previousCode = If(_selectedSearchProject Is Nothing, "", _selectedSearchProject.ProjectCode)
        Dim autoComplete As New AutoCompleteStringCollection()

        _searchProjectMatches.Clear()
        For Each project In SearchStoredProjects(query).
            GroupBy(Function(item) item.ProjectCode, StringComparer.OrdinalIgnoreCase).
            Select(Function(group) group.First()).
            OrderBy(Function(item) item.SavedProjectId)
            _searchProjectMatches.Add(project)
            Dim searchToken = ProjectSearchToken(project)
            If searchToken.Length > 0 Then
                autoComplete.Add(searchToken)
            End If
        Next

        _liveProjectSearchBox.AutoCompleteCustomSource = autoComplete

        If query.Length = 0 Then
            _selectedSearchProject = Nothing
            Return
        End If

        _selectedSearchProject = _searchProjectMatches.FirstOrDefault(
            Function(project) Not String.IsNullOrWhiteSpace(previousCode) AndAlso
                String.Equals(project.ProjectCode, previousCode, StringComparison.OrdinalIgnoreCase))

        If _selectedSearchProject Is Nothing Then
            _selectedSearchProject = ResolveSearchProject(False)
        End If
    End Sub

    Private Function SearchStoredProjects(searchText As String) As IEnumerable(Of LiveProjectItem)
        Dim query = If(searchText, "").Trim()
        Dim matches = _projects.AsEnumerable()

        If query.Length > 0 Then
            matches = matches.Where(Function(project)
                                        Return project.DisplayProjectId.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                            project.ProjectName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0
                                    End Function)
        End If

        Return matches.Select(Function(project) CreateStoredProjectOption(project))
    End Function

    Private Shared Function CreateStoredProjectOption(project As ProjectLibraryItem) As LiveProjectItem
        Dim projectCode = project.DisplayProjectId
        Return New LiveProjectItem With {
            .ProjectCode = projectCode,
            .ProjectName = project.ProjectName,
            .ClientName = "SQL",
            .VersionNumber = If(project.VersionNumber, "").Trim(),
            .ProjectSize = If(project.ProjectSize, "").Trim(),
            .TemplateName = project.ProjectName,
            .ProjectType = If(String.IsNullOrWhiteSpace(project.ProjectType), "New", project.ProjectType),
            .SavedProjectId = project.ProjectId,
            .SourceFilePath = project.FilePath,
            .ReportType = If(String.IsNullOrWhiteSpace(project.ProjectType), "New", project.ProjectType)
        }
    End Function

    Private Shared Function ProjectSearchToken(project As LiveProjectItem) As String
        If project Is Nothing Then
            Return ""
        End If

        If Not String.IsNullOrWhiteSpace(project.ProjectCode) Then
            Return project.ProjectCode.Trim()
        End If

        If project.SavedProjectId > 0 Then
            Return project.SavedProjectId.ToString(CultureInfo.InvariantCulture)
        End If

        Return ""
    End Function

    Private Sub LoadProjectList()
        _projects.Clear()
        Dim loadedFromSql = False
        Dim recentSearchText = If(_recentProjectSearchBox Is Nothing, "", _recentProjectSearchBox.Text.Trim())
        Dim activeOnly = _activeProjectsCheckBox Is Nothing OrElse _activeProjectsCheckBox.Checked

        If _sqlRepository Is Nothing Then
            SetPlannerStatus("SQL connection is not configured. Update App.config with SmaSchedulerDb.")
            RefreshSearchProjectSuggestions()
            UpdatePlanningSummary()
            Return
        End If

        Try
            For Each project In _sqlRepository.ListRecentScheduledProjects(recentSearchText, activeOnly)
                _projects.Add(project)
            Next
            loadedFromSql = True
            _status.Text = _projects.Count.ToString(CultureInfo.InvariantCulture) & " recent scheduled project(s) loaded from SQL. Double-click a project to update its schedule."
        Catch ex As Exception
            SetPlannerStatus("SQL project list could not be loaded. Check the SmaSchedulerDb connection.")
        End Try

        If loadedFromSql AndAlso recentSearchText.Length > 0 AndAlso _projects.Count = 0 Then
            SetPlannerStatus("This project is not planned in this application.")
        End If
        RefreshSearchProjectSuggestions()
        UpdatePlanningSummary()
    End Sub

    Private Sub RefreshPlannerLists()
        LoadProjectList()
    End Sub

    Private Sub UpdatePlanningSummary()
        Dim periodStart = New Date(Date.Today.Year, Date.Today.Month, 20)
        If Date.Today.Day < 20 Then
            periodStart = periodStart.AddMonths(-1)
        End If
        Dim periodEnd = periodStart.AddMonths(1)

        summaryPeriodLabel.Text = periodStart.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) &
            " to " & periodEnd.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)

        Dim periodProjects = _projects.
            Where(Function(project) project.StartDate.HasValue AndAlso
                project.StartDate.Value.Date >= periodStart AndAlso
                project.StartDate.Value.Date <= periodEnd).
            ToList()

        newProjectsCountLabel.Text = CountProjects(periodProjects, "New").ToString(CultureInfo.InvariantCulture)
        updateProjectsCountLabel.Text = CountProjects(periodProjects, "Update").ToString(CultureInfo.InvariantCulture)
        feedbackProjectsCountLabel.Text = CountProjects(periodProjects, "Feedback").ToString(CultureInfo.InvariantCulture)
    End Sub

    Private Shared Function CountProjects(projects As IEnumerable(Of ProjectLibraryItem), projectType As String) As Integer
        Return projects.Count(Function(project) String.Equals(project.ProjectType, projectType, StringComparison.OrdinalIgnoreCase))
    End Function

    Private Function ResolveSearchProject(preferFirstMatch As Boolean) As LiveProjectItem
        Dim query = If(_liveProjectSearchBox.Text, "").Trim()
        If query.Length = 0 Then
            Return Nothing
        End If

        Dim exactMatch = _searchProjectMatches.FirstOrDefault(
            Function(project)
                Return ProjectSearchToken(project).Equals(query, StringComparison.OrdinalIgnoreCase)
            End Function)
        If exactMatch IsNot Nothing Then
            Return exactMatch
        End If

        If preferFirstMatch AndAlso _searchProjectMatches.Count > 0 Then
            Return _searchProjectMatches(0)
        End If

        Return Nothing
    End Function

    Private Sub LiveProjectSearchKeyDown(sender As Object, e As KeyEventArgs) Handles _liveProjectSearchBox.KeyDown
        If e.KeyCode = Keys.Back Then
            HandleSearchBoxBackspace(e)
            Return
        End If

        If e.KeyCode <> Keys.Enter Then
            Return
        End If

        e.Handled = True
        e.SuppressKeyPress = True

        Dim matchedProject = ResolveSearchProject(True)
        If matchedProject IsNot Nothing Then
            _selectedSearchProject = matchedProject
            ReplaceSearchText(ProjectSearchToken(matchedProject))
        End If
    End Sub

    Private Sub HandleSearchBoxBackspace(e As KeyEventArgs)
        If _isUpdatingSearchText OrElse _liveProjectSearchBox.SelectionLength <= 0 Then
            Return
        End If

        Dim selectionStart = _liveProjectSearchBox.SelectionStart
        Dim currentText = _liveProjectSearchBox.Text
        Dim updatedText = If(selectionStart <= 0, "", currentText.Substring(0, selectionStart - 1))
        ReplaceSearchText(updatedText)
        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub ReplaceSearchText(value As String)
        _isUpdatingSearchText = True
        Try
            _liveProjectSearchBox.Text = value
            _liveProjectSearchBox.SelectionStart = _liveProjectSearchBox.TextLength
            _liveProjectSearchBox.SelectionLength = 0
        Finally
            _isUpdatingSearchText = False
        End Try
    End Sub

    Private Sub RecentProjectSearchTextChanged(sender As Object, e As EventArgs) Handles _recentProjectSearchBox.TextChanged
        LoadProjectList()
    End Sub

    Private Sub ActiveProjectsCheckBoxChanged(sender As Object, e As EventArgs) Handles _activeProjectsCheckBox.CheckedChanged
        LoadProjectList()
    End Sub

    Private Sub RecentProjectSearchKeyDown(sender As Object, e As KeyEventArgs) Handles _recentProjectSearchBox.KeyDown
        If e.KeyCode <> Keys.Enter Then
            Return
        End If

        e.Handled = True
        e.SuppressKeyPress = True
        LoadProjectList()
        If _recentProjectSearchBox.Text.Trim().Length > 0 AndAlso _projects.Count = 0 Then
            MessageBox.Show(Me, "This project is not planned in this application.", "Recent Scheduled Projects", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnScheduleProject_Click(sender As Object, e As EventArgs) Handles btnScheduleProject.Click
        Dim projectCode = _liveProjectSearchBox.Text.Trim()
        If projectCode.Length = 0 Then
            MessageBox.Show(Me, "Type a Project ID first.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If IsDemoProjectId(projectCode) Then
            OpenDemoSchedulerProject()
            Return
        End If

        If _sqlRepository Is Nothing Then
            MessageBox.Show(Me, "SQL connection is not configured. Update App.config with the SmaSchedulerDb connection string.", "SQL Not Configured", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim sqlProject As SqlProjectPlanningInfo = Nothing
        ' Step 1: load the live project details from SQL by Project ID at SMA.
        ' Project size comes from Table_Project_Tracking and drives scheduler resource hours.
        Try
            sqlProject = _sqlRepository.GetProjectPlanningInfo(projectCode)
        Catch ex As Exception
            MessageBox.Show(Me, "SQL project details could not be loaded." & Environment.NewLine & ex.Message, "SQL Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetPlannerStatus("SQL project details could not be loaded.")
            Return
        End Try

        If sqlProject Is Nothing Then
            MessageBox.Show(Me, "No project found for this Project ID.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SetPlannerStatus("No project found for this Project ID.")
            Return
        End If

        ' Step 2: enforce planning rules before opening the Scheduler form.
        If sqlProject IsNot Nothing AndAlso sqlProject.IsPlanned.HasValue AndAlso sqlProject.IsPlanned.Value Then
            MessageBox.Show(Me, "This project has already been planned. Please search for it in the 'Recent Scheduled Projects' list.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If sqlProject IsNot Nothing AndAlso Not sqlProject.IsActive Then
            MessageBox.Show(Me, "This project is not planned in this application.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrWhiteSpace(sqlProject.ProjectName) Then
            MessageBox.Show(Me, "Project name is missing in SQL for this Project ID.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            SetPlannerStatus("Project name is missing in SQL.")
            Return
        End If

        If String.IsNullOrWhiteSpace(sqlProject.VersionNumber) Then
            MessageBox.Show(Me, "Project version is missing in SQL for this Project ID.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            SetPlannerStatus("Project version is missing in SQL.")
            Return
        End If

        If String.IsNullOrWhiteSpace(sqlProject.ProjectSize) Then
            MessageBox.Show(Me, "Project size is missing in SQL for this Project ID.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            SetPlannerStatus("Project size is missing in SQL.")
            Return
        End If

        If Not IsKnownProjectSize(sqlProject.ProjectSize) Then
            MessageBox.Show(Me, "Project size from SQL is not valid for this Project ID.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            SetPlannerStatus("Project size from SQL is not valid.")
            Return
        End If

        ' Step 3: build the scheduler input model from SQL project metadata and report filters.
        Dim projectName = sqlProject.ProjectName.Trim()

        If String.IsNullOrWhiteSpace(projectName) Then
            MessageBox.Show(Me, "Type or select a live project first.", "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim projectToSchedule As New LiveProjectItem With {
            .ProjectCode = FirstNonBlank(sqlProject.ProjectIdAtSma, projectName),
            .ProjectName = projectName,
            .ClientName = "SQL",
            .VersionNumber = sqlProject.VersionNumber.Trim(),
            .ProjectSize = sqlProject.ProjectSize.Trim(),
            .TemplateName = FirstNonBlank(sqlProject.ProjectType, "New"),
            .ProjectType = FirstNonBlank(sqlProject.ProjectType, "New"),
            .ReportType = sqlProject.ReportType,
            .TaskReportFilter = BuildTaskReportFilter(sqlProject),
            .ProjectDetailsText = BuildProjectDetailsText(sqlProject),
            .FinalCompletionDate = sqlProject.FinalCompletionDate,
            .PlanningMessage = sqlProject.PlanningMessage,
            .ControllerAtRolc = sqlProject.ControllerAtRolc,
            .ClientType = sqlProject.ClientType,
            .IsPointcloud = sqlProject.IsPointcloud,
            .TechPack = sqlProject.TechPack,
            .DeedProfile = sqlProject.DeedProfile,
            .ShadowAnalysis = sqlProject.ShadowAnalysis,
            .UrgentSmallProjects = sqlProject.UrgentSmallProjects
        }

        ' Step 4: open SMA Scheduler; SQL task rows are loaded into the task grid.
        Dim scheduler As SMASchedulerForm = Nothing
        Try
            scheduler = New SMASchedulerForm()
            scheduler.LoadLiveProjectTemplate(projectToSchedule)
            FormTransitionService.ShowDialogWithMotion(Me, scheduler)
        Catch ex As InvalidOperationException
            MessageBox.Show(Me, ex.Message, "Schedule Project", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SetPlannerStatus(ex.Message)
            Return
        Catch ex As Exception
            MessageBox.Show(Me, "SQL task details could not be loaded." & Environment.NewLine & ex.Message, "SQL Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetPlannerStatus("SQL task details could not be loaded.")
            Return
        Finally
            If scheduler IsNot Nothing Then
                scheduler.Dispose()
            End If
        End Try

        ApplyCurrentTheme()
        RefreshPlannerLists()
    End Sub

    Private Shared Function IsDemoProjectId(projectCode As String) As Boolean
#If DEBUG Then
        Return String.Equals(If(projectCode, "").Trim(), "12345", StringComparison.OrdinalIgnoreCase)
#Else
        Return False
#End If
    End Function

    Private Shared Function CreateDemoProject() As LiveProjectItem
        Return New LiveProjectItem With {
            .ProjectCode = "12345",
            .ProjectName = "Demo SMA Planning Project",
            .ClientName = "Demo",
            .VersionNumber = "1.0",
            .ProjectSize = "Small",
            .TemplateName = "Demo BRE/ROL",
            .ProjectType = "New",
            .ReportType = "BRE/ROL",
            .TaskReportFilter = "BRE/ROL",
            .ProjectDetailsText = "Debug demo project. SQL is not used for Project ID 12345.",
            .PlanningMessage = "Demo scheduler loaded without SQL."
        }
    End Function

    Private Sub OpenDemoSchedulerProject()
#If DEBUG Then
        Dim scheduler As SMASchedulerForm = Nothing
        Try
            scheduler = New SMASchedulerForm()
            scheduler.LoadDemoProject(CreateDemoProject())
            FormTransitionService.ShowDialogWithMotion(Me, scheduler)
            SetPlannerStatus("Demo project 12345 opened without SQL.")
        Catch ex As Exception
            MessageBox.Show(Me, "Demo scheduler could not be opened." & Environment.NewLine & ex.Message, "Demo Project", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetPlannerStatus("Demo scheduler could not be opened.")
        Finally
            If scheduler IsNot Nothing Then
                scheduler.Dispose()
            End If
        End Try

        ApplyCurrentTheme()
#Else
        Throw New InvalidOperationException("Demo project loading is only available in Debug builds.")
#End If
    End Sub

    Private Shared Function IsKnownProjectSize(projectSize As String) As Boolean
        Return {"Small", "Medium", "Large", "Very Large"}.
            Any(Function(sizeName) String.Equals(sizeName, If(projectSize, "").Trim(), StringComparison.OrdinalIgnoreCase))
    End Function

    Private Shared Function FirstNonBlank(ParamArray values() As String) As String
        For Each value In values
            If Not String.IsNullOrWhiteSpace(value) Then
                Return value.Trim()
            End If
        Next

        Return ""
    End Function

    Private Shared Function BuildTaskReportFilter(sqlProject As SqlProjectPlanningInfo) As String
        Dim parts As New List(Of String)()
        If Not String.IsNullOrWhiteSpace(sqlProject.ReportType) Then
            parts.Add(sqlProject.ReportType)
        End If
        If sqlProject.ShadowAnalysis Then
            parts.Add("Shadow Analysis")
        End If
        If sqlProject.DeedProfile Then
            parts.Add("Deed Profile")
        End If

        Return String.Join("; ", parts)
    End Function

    Private Shared Function BuildProjectDetailsText(sqlProject As SqlProjectPlanningInfo) As String
        If sqlProject Is Nothing Then
            Return ""
        End If

        Dim details As New List(Of String) From {
            "Report Type: " & If(String.IsNullOrWhiteSpace(sqlProject.ReportType), "None", sqlProject.ReportType)
        }

        If sqlProject.FinalCompletionDate.HasValue Then
            details.Add("Final Completion: " & sqlProject.FinalCompletionDate.Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture))
        End If
        If Not String.IsNullOrWhiteSpace(sqlProject.PlanningMessage) Then
            details.Add("Planning Message: " & sqlProject.PlanningMessage)
        End If
        If Not String.IsNullOrWhiteSpace(sqlProject.ControllerAtRolc) Then
            details.Add("Controller at ROLC: " & sqlProject.ControllerAtRolc)
        End If
        If Not String.IsNullOrWhiteSpace(sqlProject.ClientType) Then
            details.Add("Client Type: " & sqlProject.ClientType)
        End If
        If sqlProject.IsPointcloud Then
            details.Add("Pointcloud")
        End If
        If sqlProject.TechPack Then
            details.Add("Tech Pack")
        End If
        If sqlProject.DeedProfile Then
            details.Add("Deed Profile")
        End If
        If sqlProject.ShadowAnalysis Then
            details.Add("Shadow Analysis")
        End If
        If sqlProject.UrgentSmallProjects Then
            details.Add("Urgent Small Project")
        End If

        Return String.Join(" | ", details)
    End Function

    Private Sub OpenSelectedExistingProject(sender As Object, e As DataGridViewCellEventArgs) Handles _grid.CellDoubleClick
        If e.RowIndex < 0 Then
            Return
        End If

        Dim item = TryCast(_grid.Rows(e.RowIndex).DataBoundItem, ProjectLibraryItem)
        If item Is Nothing Then
            Return
        End If

        OpenStoredProject(item)
    End Sub

    Private Sub OpenStoredProject(item As ProjectLibraryItem)
        If item Is Nothing Then
            MessageBox.Show(Me, "This scheduled project could not be found.", "Open Project", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If item.ProjectId <= 0 AndAlso String.IsNullOrWhiteSpace(item.ProjectCode) Then
            MessageBox.Show(Me, "This scheduled project does not have a saved identifier yet.", "Open Project", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim savedSchedule As SavedProjectSchedule = Nothing

        If _sqlRepository IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(item.ProjectCode) Then
            Try
                savedSchedule = _sqlRepository.LoadSavedProjectScheduleByProjectCode(item.ProjectCode)
            Catch ex As Exception
                SetPlannerStatus("SQL project schedule load failed.")
            End Try
        End If

        If savedSchedule Is Nothing AndAlso _sqlRepository IsNot Nothing AndAlso item.ProjectId > 0 Then
            Try
                savedSchedule = _sqlRepository.LoadSavedProjectSchedule(item.ProjectId)
            Catch ex As Exception
                SetPlannerStatus("SQL project load failed.")
            End Try
        End If

        If savedSchedule Is Nothing Then
            MessageBox.Show(Me, "This planned project could not be opened from SQL.", "Open Project", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            LoadProjectList()
            Return
        End If

        Using scheduler As New SMASchedulerForm()
            scheduler.LoadSavedProjectSchedule(savedSchedule)
            FormTransitionService.ShowDialogWithMotion(Me, scheduler)
        End Using
        ApplyCurrentTheme()
        RefreshPlannerLists()
    End Sub

    Private Sub SetPlannerStatus(message As String)
        If String.IsNullOrWhiteSpace(message) Then
            Return
        End If

        _status.Text = message
    End Sub

    Private Shared Function IsInDesignerHost() As Boolean
        Return LicenseManager.UsageMode = LicenseUsageMode.Designtime
    End Function

    Private Sub SeedPlannerDesignerData()
        _projects.Clear()

        _grid.DataSource = _projects
        _status.Text = "Designer preview"
        UpdatePlanningSummary()
    End Sub


End Class

