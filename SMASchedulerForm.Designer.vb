<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SMASchedulerForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        commandBar = New ToolStrip()
        btnSave = New ToolStripButton()
        btnRefreshCapacity = New ToolStripButton()
        sepFile = New ToolStripSeparator()
        btnAddTask = New ToolStripButton()
        btnDelete = New ToolStripButton()
        btnMoveUp = New ToolStripButton()
        btnMoveDown = New ToolStripButton()
        sepTasks = New ToolStripSeparator()
        btnLink = New ToolStripButton()
        btnUnlink = New ToolStripButton()
        btnMilestone = New ToolStripButton()
        headerPanel = New Panel()
        appTitle = New Label()
        projectLabel = New Label()
        _projectName = New TextBox()
        versionLabel = New Label()
        _versionNumber = New TextBox()
        totalHoursLabel = New Label()
        _totalProjectHours = New BlankNumericUpDown()
        projectSizeLabel = New Label()
        _projectSizeSelector = New ComboBox()
        _includeSaturdays = New CheckBox()
        _summaryTitle = New Label()
        _summaryDates = New Label()
        _summaryResources = New Label()
        _projectDetailsCaptionLabel = New Label()
        _projectDetailsValueLabel = New Label()
        _scheduleProjectButton = New Button()
        taskCatalogLabel = New Label()
        _taskCatalogSelector = New ComboBox()
        contentSplit = New SplitContainer()
        _workspaceTabs = New TabControl()
        taskAllocationTab = New TabPage()
        mainSplit = New SplitContainer()
        _grid = New DataGridView()
        ganttPreviewSplit = New SplitContainer()
        _gantt = New GanttPanel()
        allocationPreviewPanel = New Panel()
        allocationPreviewBodySplit = New SplitContainer()
        allocationPreviewChart = New PlannerPieChartPanel()
        allocationLegendGrid = New DataGridView()
        allocationPreviewBadges = New FlowLayoutPanel()
        allocationPrimaryLabel = New Label()
        allocationSecondaryLabel = New Label()
        allocationPreviewTitle = New Label()
        taskUsageTab = New TabPage()
        taskUsageSplit = New SplitContainer()
        _taskUsageGrid = New DataGridView()
        taskUsagePreviewPanel = New Panel()
        taskUsagePreviewBodySplit = New SplitContainer()
        taskUsagePreviewChart = New PlannerPieChartPanel()
        taskUsageLegendGrid = New DataGridView()
        taskUsagePreviewBadges = New FlowLayoutPanel()
        taskUsagePrimaryLabel = New Label()
        taskUsageSecondaryLabel = New Label()
        taskUsagePreviewTitle = New Label()
        resourceUsageTab = New TabPage()
        resourceUsageSplit = New SplitContainer()
        _resourceUsageGrid = New DataGridView()
        resourceUsagePreviewPanel = New Panel()
        resourceUsagePreviewBodySplit = New SplitContainer()
        resourceUsagePreviewChart = New PlannerPieChartPanel()
        resourceUsageLegendGrid = New DataGridView()
        resourceUsagePreviewBadges = New FlowLayoutPanel()
        resourceUsagePrimaryLabel = New Label()
        resourceUsageSecondaryLabel = New Label()
        resourceUsagePreviewTitle = New Label()
        capacityPlanningTab = New TabPage()
        _capacityGrid = New DataGridView()
        _capacityFilterPanel = New FlowLayoutPanel()
        _capacityFromLabel = New Label()
        _capacityStartPicker = New DateTimePicker()
        _capacityToLabel = New Label()
        _capacityFinishPicker = New DateTimePicker()
        _capacityEmployeesLabel = New Label()
        _capacityEmployeeList = New CheckedListBox()
        _capacityApplyButton = New Button()
        _capacitySelectAllButton = New Button()
        _capacityClearButton = New Button()
        _capacityFilterStatusLabel = New Label()
        resourceUtilizationTab = New TabPage()
        resourceUtilizationHost = New Panel()
        _resourceUtilizationGrid = New DataGridView()
        resourceUtilizationToolbar = New FlowLayoutPanel()
        _resourceUtilizationRefreshButton = New Button()
        _resourceUtilizationColorSelector = New ComboBox()
        _resourceUtilizationApplyButton = New Button()
        _resourceUtilizationClearButton = New Button()
        _resourceUtilizationMailButton = New Button()
        employeeCapacityTab = New TabPage()
        employeeCapacityHost = New Panel()
        _employeeCapacityGrid = New DataGridView()
        employeeCapacityToolbar = New FlowLayoutPanel()
        _employeeCapacityAddButton = New Button()
        _employeeCapacityDeleteButton = New Button()
        _employeeCapacityRefreshButton = New Button()
        _detailsPanel = New Panel()
        taskWorkspaceTitle = New Label()
        statusBar = New StatusStrip()
        _status = New ToolStripStatusLabel()
        commandBar.SuspendLayout()
        headerPanel.SuspendLayout()
        CType(_totalProjectHours, ComponentModel.ISupportInitialize).BeginInit()
        CType(contentSplit, ComponentModel.ISupportInitialize).BeginInit()
        contentSplit.Panel1.SuspendLayout()
        contentSplit.Panel2.SuspendLayout()
        contentSplit.SuspendLayout()
        _workspaceTabs.SuspendLayout()
        taskAllocationTab.SuspendLayout()
        CType(mainSplit, ComponentModel.ISupportInitialize).BeginInit()
        mainSplit.Panel1.SuspendLayout()
        mainSplit.Panel2.SuspendLayout()
        mainSplit.SuspendLayout()
        CType(_grid, ComponentModel.ISupportInitialize).BeginInit()
        CType(ganttPreviewSplit, ComponentModel.ISupportInitialize).BeginInit()
        ganttPreviewSplit.Panel1.SuspendLayout()
        ganttPreviewSplit.Panel2.SuspendLayout()
        ganttPreviewSplit.SuspendLayout()
        allocationPreviewPanel.SuspendLayout()
        CType(allocationPreviewBodySplit, ComponentModel.ISupportInitialize).BeginInit()
        allocationPreviewBodySplit.Panel1.SuspendLayout()
        allocationPreviewBodySplit.Panel2.SuspendLayout()
        allocationPreviewBodySplit.SuspendLayout()
        CType(allocationLegendGrid, ComponentModel.ISupportInitialize).BeginInit()
        allocationPreviewBadges.SuspendLayout()
        taskUsageTab.SuspendLayout()
        CType(taskUsageSplit, ComponentModel.ISupportInitialize).BeginInit()
        taskUsageSplit.Panel1.SuspendLayout()
        taskUsageSplit.Panel2.SuspendLayout()
        taskUsageSplit.SuspendLayout()
        CType(_taskUsageGrid, ComponentModel.ISupportInitialize).BeginInit()
        taskUsagePreviewPanel.SuspendLayout()
        CType(taskUsagePreviewBodySplit, ComponentModel.ISupportInitialize).BeginInit()
        taskUsagePreviewBodySplit.Panel1.SuspendLayout()
        taskUsagePreviewBodySplit.Panel2.SuspendLayout()
        taskUsagePreviewBodySplit.SuspendLayout()
        CType(taskUsageLegendGrid, ComponentModel.ISupportInitialize).BeginInit()
        taskUsagePreviewBadges.SuspendLayout()
        resourceUsageTab.SuspendLayout()
        CType(resourceUsageSplit, ComponentModel.ISupportInitialize).BeginInit()
        resourceUsageSplit.Panel1.SuspendLayout()
        resourceUsageSplit.Panel2.SuspendLayout()
        resourceUsageSplit.SuspendLayout()
        CType(_resourceUsageGrid, ComponentModel.ISupportInitialize).BeginInit()
        resourceUsagePreviewPanel.SuspendLayout()
        CType(resourceUsagePreviewBodySplit, ComponentModel.ISupportInitialize).BeginInit()
        resourceUsagePreviewBodySplit.Panel1.SuspendLayout()
        resourceUsagePreviewBodySplit.Panel2.SuspendLayout()
        resourceUsagePreviewBodySplit.SuspendLayout()
        CType(resourceUsageLegendGrid, ComponentModel.ISupportInitialize).BeginInit()
        resourceUsagePreviewBadges.SuspendLayout()
        capacityPlanningTab.SuspendLayout()
        CType(_capacityGrid, ComponentModel.ISupportInitialize).BeginInit()
        _capacityFilterPanel.SuspendLayout()
        resourceUtilizationTab.SuspendLayout()
        resourceUtilizationHost.SuspendLayout()
        CType(_resourceUtilizationGrid, ComponentModel.ISupportInitialize).BeginInit()
        resourceUtilizationToolbar.SuspendLayout()
        employeeCapacityTab.SuspendLayout()
        employeeCapacityHost.SuspendLayout()
        CType(_employeeCapacityGrid, ComponentModel.ISupportInitialize).BeginInit()
        employeeCapacityToolbar.SuspendLayout()
        _detailsPanel.SuspendLayout()
        statusBar.SuspendLayout()
        SuspendLayout()
        ' 
        ' commandBar
        ' 
        commandBar.BackColor = Color.FromArgb(CByte(35), CByte(46), CByte(66))
        commandBar.GripStyle = ToolStripGripStyle.Hidden
        commandBar.ImageScalingSize = New Size(18, 18)
        commandBar.Items.AddRange(New ToolStripItem() {btnSave, btnRefreshCapacity, sepFile, btnAddTask, btnDelete, btnMoveUp, btnMoveDown, sepTasks, btnLink, btnUnlink, btnMilestone})
        commandBar.Location = New Point(0, 0)
        commandBar.Name = "commandBar"
        commandBar.Padding = New Padding(11, 9, 11, 9)
        commandBar.Size = New Size(1558, 41)
        commandBar.TabIndex = 0
        ' 
        ' btnSave
        ' 
        btnSave.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnSave.ForeColor = Color.White
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(35, 20)
        btnSave.Text = "Save"
        ' 
        ' btnRefreshCapacity
        ' 
        btnRefreshCapacity.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnRefreshCapacity.ForeColor = Color.White
        btnRefreshCapacity.Name = "btnRefreshCapacity"
        btnRefreshCapacity.Size = New Size(149, 20)
        btnRefreshCapacity.Text = "Refresh Capacity Planning"
        ' 
        ' sepFile
        ' 
        sepFile.Name = "sepFile"
        sepFile.Size = New Size(6, 23)
        ' 
        ' btnAddTask
        ' 
        btnAddTask.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnAddTask.ForeColor = Color.White
        btnAddTask.Name = "btnAddTask"
        btnAddTask.Size = New Size(58, 20)
        btnAddTask.Text = "Add Task"
        ' 
        ' btnDelete
        ' 
        btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnDelete.ForeColor = Color.White
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(44, 20)
        btnDelete.Text = "Delete"
        ' 
        ' btnMoveUp
        ' 
        btnMoveUp.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnMoveUp.ForeColor = Color.White
        btnMoveUp.Name = "btnMoveUp"
        btnMoveUp.Size = New Size(59, 20)
        btnMoveUp.Text = "Move Up"
        ' 
        ' btnMoveDown
        ' 
        btnMoveDown.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnMoveDown.ForeColor = Color.White
        btnMoveDown.Name = "btnMoveDown"
        btnMoveDown.Size = New Size(75, 20)
        btnMoveDown.Text = "Move Down"
        ' 
        ' sepTasks
        ' 
        sepTasks.Name = "sepTasks"
        sepTasks.Size = New Size(6, 23)
        ' 
        ' btnLink
        ' 
        btnLink.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnLink.ForeColor = Color.White
        btnLink.Name = "btnLink"
        btnLink.Size = New Size(33, 20)
        btnLink.Text = "Link"
        ' 
        ' btnUnlink
        ' 
        btnUnlink.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnUnlink.ForeColor = Color.White
        btnUnlink.Name = "btnUnlink"
        btnUnlink.Size = New Size(45, 20)
        btnUnlink.Text = "Unlink"
        ' 
        ' btnMilestone
        ' 
        btnMilestone.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnMilestone.ForeColor = Color.White
        btnMilestone.Name = "btnMilestone"
        btnMilestone.Size = New Size(63, 20)
        btnMilestone.Text = "Milestone"
        ' 
        ' headerPanel
        ' 
        headerPanel.BackColor = Color.FromArgb(CByte(229), CByte(241), CByte(255))
        headerPanel.Controls.Add(appTitle)
        headerPanel.Controls.Add(projectLabel)
        headerPanel.Controls.Add(_projectName)
        headerPanel.Controls.Add(versionLabel)
        headerPanel.Controls.Add(_versionNumber)
        headerPanel.Controls.Add(totalHoursLabel)
        headerPanel.Controls.Add(_totalProjectHours)
        headerPanel.Controls.Add(projectSizeLabel)
        headerPanel.Controls.Add(_projectSizeSelector)
        headerPanel.Controls.Add(_includeSaturdays)
        headerPanel.Controls.Add(_summaryTitle)
        headerPanel.Controls.Add(_summaryDates)
        headerPanel.Controls.Add(_summaryResources)
        headerPanel.Controls.Add(_projectDetailsCaptionLabel)
        headerPanel.Controls.Add(_projectDetailsValueLabel)
        headerPanel.Controls.Add(_scheduleProjectButton)
        headerPanel.Dock = DockStyle.Top
        headerPanel.Location = New Point(0, 41)
        headerPanel.Margin = New Padding(3, 4, 3, 4)
        headerPanel.Name = "headerPanel"
        headerPanel.Padding = New Padding(18, 16, 18, 16)
        headerPanel.Size = New Size(1558, 210)
        headerPanel.TabIndex = 1
        ' 
        ' appTitle
        ' 
        appTitle.AutoSize = True
        appTitle.Font = New Font("Segoe UI Semibold", 16F)
        appTitle.ForeColor = Color.FromArgb(CByte(24), CByte(31), CByte(42))
        appTitle.Location = New Point(18, 12)
        appTitle.Name = "appTitle"
        appTitle.Size = New Size(165, 30)
        appTitle.TabIndex = 0
        appTitle.Text = "SMA Scheduler"
        ' 
        ' projectLabel
        ' 
        projectLabel.AutoSize = True
        projectLabel.ForeColor = Color.DimGray
        projectLabel.Location = New Point(22, 54)
        projectLabel.Name = "projectLabel"
        projectLabel.Size = New Size(44, 15)
        projectLabel.TabIndex = 1
        projectLabel.Text = "Project"
        ' 
        ' _projectName
        ' 
        _projectName.BorderStyle = BorderStyle.FixedSingle
        _projectName.Location = New Point(22, 78)
        _projectName.Margin = New Padding(3, 4, 3, 4)
        _projectName.Name = "_projectName"
        _projectName.Size = New Size(300, 23)
        _projectName.TabIndex = 2
        _projectName.Text = "SMA Scheduler"
        ' 
        ' versionLabel
        ' 
        versionLabel.AutoSize = True
        versionLabel.ForeColor = Color.DimGray
        versionLabel.Location = New Point(342, 54)
        versionLabel.Name = "versionLabel"
        versionLabel.Size = New Size(45, 15)
        versionLabel.TabIndex = 15
        versionLabel.Text = "Version"
        ' 
        ' _versionNumber
        ' 
        _versionNumber.BorderStyle = BorderStyle.FixedSingle
        _versionNumber.Location = New Point(342, 78)
        _versionNumber.Margin = New Padding(3, 4, 3, 4)
        _versionNumber.Name = "_versionNumber"
        _versionNumber.Size = New Size(86, 23)
        _versionNumber.TabIndex = 16
        ' 
        ' totalHoursLabel
        ' 
        totalHoursLabel.AutoSize = True
        totalHoursLabel.ForeColor = Color.DimGray
        totalHoursLabel.Location = New Point(452, 54)
        totalHoursLabel.Name = "totalHoursLabel"
        totalHoursLabel.Size = New Size(107, 15)
        totalHoursLabel.TabIndex = 9
        totalHoursLabel.Text = "Total Project Hours"
        ' 
        ' _totalProjectHours
        ' 
        _totalProjectHours.DecimalPlaces = 1
        _totalProjectHours.Location = New Point(452, 78)
        _totalProjectHours.Margin = New Padding(3, 4, 3, 4)
        _totalProjectHours.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        _totalProjectHours.Name = "_totalProjectHours"
        _totalProjectHours.Size = New Size(149, 23)
        _totalProjectHours.TabIndex = 10
        _totalProjectHours.ThousandsSeparator = True
        ' 
        ' projectSizeLabel
        ' 
        projectSizeLabel.AutoSize = True
        projectSizeLabel.ForeColor = Color.DimGray
        projectSizeLabel.Location = New Point(620, 54)
        projectSizeLabel.Name = "projectSizeLabel"
        projectSizeLabel.Size = New Size(67, 15)
        projectSizeLabel.TabIndex = 3
        projectSizeLabel.Text = "Project Size"
        ' 
        ' _projectSizeSelector
        ' 
        _projectSizeSelector.DropDownStyle = ComboBoxStyle.DropDownList
        _projectSizeSelector.FormattingEnabled = True
        _projectSizeSelector.Location = New Point(620, 78)
        _projectSizeSelector.Margin = New Padding(3, 4, 3, 4)
        _projectSizeSelector.Name = "_projectSizeSelector"
        _projectSizeSelector.Size = New Size(150, 23)
        _projectSizeSelector.TabIndex = 4
        ' 
        ' _includeSaturdays
        ' 
        _includeSaturdays.AutoSize = True
        _includeSaturdays.Location = New Point(21, 108)
        _includeSaturdays.Name = "_includeSaturdays"
        _includeSaturdays.Size = New Size(101, 19)
        _includeSaturdays.TabIndex = 20
        _includeSaturdays.Text = "Weekend Plan"
        _includeSaturdays.UseVisualStyleBackColor = True
        ' 
        ' _summaryTitle
        ' 
        _summaryTitle.BackColor = Color.FromArgb(CByte(223), CByte(245), CByte(232))
        _summaryTitle.Font = New Font("Segoe UI Semibold", 10F)
        _summaryTitle.ForeColor = Color.FromArgb(CByte(37), CByte(47), CByte(63))
        _summaryTitle.Location = New Point(28, 135)
        _summaryTitle.Name = "_summaryTitle"
        _summaryTitle.Size = New Size(190, 30)
        _summaryTitle.TabIndex = 5
        _summaryTitle.Text = "0 tasks"
        _summaryTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' _summaryDates
        ' 
        _summaryDates.BackColor = Color.FromArgb(CByte(255), CByte(243), CByte(205))
        _summaryDates.Font = New Font("Segoe UI Semibold", 10F)
        _summaryDates.ForeColor = Color.FromArgb(CByte(37), CByte(47), CByte(63))
        _summaryDates.Location = New Point(230, 135)
        _summaryDates.Name = "_summaryDates"
        _summaryDates.Size = New Size(210, 30)
        _summaryDates.TabIndex = 6
        _summaryDates.Text = "No dates"
        _summaryDates.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' _summaryResources
        ' 
        _summaryResources.BackColor = Color.FromArgb(CByte(248), CByte(222), CByte(234))
        _summaryResources.Font = New Font("Segoe UI Semibold", 10F)
        _summaryResources.ForeColor = Color.FromArgb(CByte(37), CByte(47), CByte(63))
        _summaryResources.Location = New Point(452, 135)
        _summaryResources.Name = "_summaryResources"
        _summaryResources.Size = New Size(230, 30)
        _summaryResources.TabIndex = 8
        _summaryResources.Text = "No resources"
        _summaryResources.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' _projectDetailsCaptionLabel
        ' 
        _projectDetailsCaptionLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _projectDetailsCaptionLabel.AutoSize = True
        _projectDetailsCaptionLabel.BackColor = Color.Transparent
        _projectDetailsCaptionLabel.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        _projectDetailsCaptionLabel.Location = New Point(990, 18)
        _projectDetailsCaptionLabel.Name = "_projectDetailsCaptionLabel"
        _projectDetailsCaptionLabel.Size = New Size(82, 15)
        _projectDetailsCaptionLabel.TabIndex = 21
        _projectDetailsCaptionLabel.Text = "Project Details"
        ' 
        ' _projectDetailsValueLabel
        ' 
        _projectDetailsValueLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _projectDetailsValueLabel.BackColor = Color.White
        _projectDetailsValueLabel.BorderStyle = BorderStyle.FixedSingle
        _projectDetailsValueLabel.Font = New Font("Segoe UI", 8.8F)
        _projectDetailsValueLabel.Location = New Point(990, 42)
        _projectDetailsValueLabel.Name = "_projectDetailsValueLabel"
        _projectDetailsValueLabel.Padding = New Padding(8, 5, 8, 5)
        _projectDetailsValueLabel.Size = New Size(540, 106)
        _projectDetailsValueLabel.TabIndex = 22
        ' 
        ' _scheduleProjectButton
        ' 
        _scheduleProjectButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _scheduleProjectButton.BackColor = Color.FromArgb(CByte(34), CByte(169), CByte(105))
        _scheduleProjectButton.FlatAppearance.BorderSize = 0
        _scheduleProjectButton.FlatStyle = FlatStyle.Flat
        _scheduleProjectButton.ForeColor = Color.White
        _scheduleProjectButton.Location = New Point(1310, 155)
        _scheduleProjectButton.Name = "_scheduleProjectButton"
        _scheduleProjectButton.Size = New Size(220, 40)
        _scheduleProjectButton.TabIndex = 23
        _scheduleProjectButton.Text = "Schedule Project"
        _scheduleProjectButton.UseVisualStyleBackColor = False
        ' 
        ' taskCatalogLabel
        ' 
        taskCatalogLabel.AutoSize = True
        taskCatalogLabel.ForeColor = Color.DimGray
        taskCatalogLabel.Location = New Point(13, 108)
        taskCatalogLabel.Name = "taskCatalogLabel"
        taskCatalogLabel.Size = New Size(147, 20)
        taskCatalogLabel.TabIndex = 1
        taskCatalogLabel.Text = "Database Task Name"
        ' 
        ' _taskCatalogSelector
        ' 
        _taskCatalogSelector.DropDownStyle = ComboBoxStyle.DropDownList
        _taskCatalogSelector.FormattingEnabled = True
        _taskCatalogSelector.Location = New Point(13, 132)
        _taskCatalogSelector.Margin = New Padding(3, 4, 3, 4)
        _taskCatalogSelector.Name = "_taskCatalogSelector"
        _taskCatalogSelector.Size = New Size(427, 23)
        _taskCatalogSelector.TabIndex = 2
        ' 
        ' contentSplit
        ' 
        contentSplit.BackColor = Color.FromArgb(CByte(224), CByte(229), CByte(236))
        contentSplit.Dock = DockStyle.Fill
        contentSplit.Location = New Point(0, 251)
        contentSplit.Name = "contentSplit"
        contentSplit.Orientation = Orientation.Horizontal
        ' 
        ' contentSplit.Panel1
        ' 
        contentSplit.Panel1.Controls.Add(_workspaceTabs)
        contentSplit.Panel1MinSize = 360
        ' 
        ' contentSplit.Panel2
        ' 
        contentSplit.Panel2.Controls.Add(_detailsPanel)
        contentSplit.Panel2Collapsed = True
        contentSplit.Size = New Size(1558, 564)
        contentSplit.SplitterDistance = 360
        contentSplit.TabIndex = 2
        ' 
        ' _workspaceTabs
        ' 
        _workspaceTabs.Controls.Add(taskAllocationTab)
        _workspaceTabs.Controls.Add(taskUsageTab)
        _workspaceTabs.Controls.Add(resourceUsageTab)
        _workspaceTabs.Controls.Add(capacityPlanningTab)
        _workspaceTabs.Controls.Add(resourceUtilizationTab)
        _workspaceTabs.Controls.Add(employeeCapacityTab)
        _workspaceTabs.Dock = DockStyle.Fill
        _workspaceTabs.Location = New Point(0, 0)
        _workspaceTabs.Name = "_workspaceTabs"
        _workspaceTabs.Padding = New Point(18, 6)
        _workspaceTabs.SelectedIndex = 0
        _workspaceTabs.Size = New Size(1558, 564)
        _workspaceTabs.TabIndex = 0
        ' 
        ' taskAllocationTab
        ' 
        taskAllocationTab.BackColor = Color.White
        taskAllocationTab.Controls.Add(mainSplit)
        taskAllocationTab.Location = New Point(4, 30)
        taskAllocationTab.Name = "taskAllocationTab"
        taskAllocationTab.Padding = New Padding(3)
        taskAllocationTab.Size = New Size(1550, 530)
        taskAllocationTab.TabIndex = 0
        taskAllocationTab.Text = "Task Allocation"
        ' 
        ' mainSplit
        ' 
        mainSplit.BackColor = Color.FromArgb(CByte(224), CByte(229), CByte(236))
        mainSplit.Dock = DockStyle.Fill
        mainSplit.Location = New Point(3, 3)
        mainSplit.Margin = New Padding(3, 4, 3, 4)
        mainSplit.Name = "mainSplit"
        ' 
        ' mainSplit.Panel1
        ' 
        mainSplit.Panel1.Controls.Add(_grid)
        mainSplit.Panel1MinSize = 640
        ' 
        ' mainSplit.Panel2
        ' 
        mainSplit.Panel2.Controls.Add(ganttPreviewSplit)
        mainSplit.Panel2MinSize = 320
        mainSplit.Size = New Size(1544, 524)
        mainSplit.SplitterDistance = 1099
        mainSplit.SplitterWidth = 5
        mainSplit.TabIndex = 0
        ' 
        ' _grid
        ' 
        _grid.AllowUserToAddRows = False
        _grid.AllowUserToDeleteRows = False
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        _grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        _grid.BackgroundColor = Color.White
        _grid.BorderStyle = BorderStyle.None
        _grid.ColumnHeadersHeight = 34
        _grid.Dock = DockStyle.Fill
        _grid.EnableHeadersVisualStyles = False
        _grid.GridColor = Color.FromArgb(CByte(232), CByte(236), CByte(242))
        _grid.Location = New Point(0, 0)
        _grid.Margin = New Padding(3, 4, 3, 4)
        _grid.MultiSelect = False
        _grid.Name = "_grid"
        _grid.RowHeadersVisible = False
        _grid.RowHeadersWidth = 51
        _grid.RowTemplate.Height = 30
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        _grid.Size = New Size(1099, 524)
        _grid.TabIndex = 0
        ' 
        ' ganttPreviewSplit
        ' 
        ganttPreviewSplit.BackColor = Color.FromArgb(CByte(224), CByte(229), CByte(236))
        ganttPreviewSplit.Dock = DockStyle.Fill
        ganttPreviewSplit.Location = New Point(0, 0)
        ganttPreviewSplit.Name = "ganttPreviewSplit"
        ganttPreviewSplit.Orientation = Orientation.Horizontal
        ' 
        ' ganttPreviewSplit.Panel1
        ' 
        ganttPreviewSplit.Panel1.Controls.Add(_gantt)
        ganttPreviewSplit.Panel1MinSize = 260
        ' 
        ' ganttPreviewSplit.Panel2
        ' 
        ganttPreviewSplit.Panel2.Controls.Add(allocationPreviewPanel)
        ganttPreviewSplit.Panel2MinSize = 200
        ganttPreviewSplit.Size = New Size(440, 524)
        ganttPreviewSplit.SplitterDistance = 300
        ganttPreviewSplit.SplitterWidth = 5
        ganttPreviewSplit.TabIndex = 0
        ' 
        ' _gantt
        ' 
        _gantt.AutoScroll = True
        _gantt.BackColor = Color.White
        _gantt.Dock = DockStyle.Fill
        _gantt.Location = New Point(0, 0)
        _gantt.Margin = New Padding(3, 4, 3, 4)
        _gantt.Name = "_gantt"
        _gantt.Size = New Size(440, 300)
        _gantt.TabIndex = 0
        ' 
        ' allocationPreviewPanel
        ' 
        allocationPreviewPanel.BackColor = Color.White
        allocationPreviewPanel.Controls.Add(allocationPreviewBodySplit)
        allocationPreviewPanel.Controls.Add(allocationPreviewBadges)
        allocationPreviewPanel.Controls.Add(allocationPreviewTitle)
        allocationPreviewPanel.Dock = DockStyle.Fill
        allocationPreviewPanel.Location = New Point(0, 0)
        allocationPreviewPanel.Name = "allocationPreviewPanel"
        allocationPreviewPanel.Padding = New Padding(8)
        allocationPreviewPanel.Size = New Size(440, 219)
        allocationPreviewPanel.TabIndex = 0
        ' 
        ' allocationPreviewBodySplit
        ' 
        allocationPreviewBodySplit.Dock = DockStyle.Fill
        allocationPreviewBodySplit.Location = New Point(8, 70)
        allocationPreviewBodySplit.Name = "allocationPreviewBodySplit"
        ' 
        ' allocationPreviewBodySplit.Panel1
        ' 
        allocationPreviewBodySplit.Panel1.Controls.Add(allocationPreviewChart)
        ' 
        ' allocationPreviewBodySplit.Panel2
        ' 
        allocationPreviewBodySplit.Panel2.Controls.Add(allocationLegendGrid)
        allocationPreviewBodySplit.Size = New Size(424, 141)
        allocationPreviewBodySplit.SplitterDistance = 212
        allocationPreviewBodySplit.TabIndex = 2
        ' 
        ' allocationPreviewChart
        ' 
        allocationPreviewChart.BackColor = Color.White
        allocationPreviewChart.Dock = DockStyle.Fill
        allocationPreviewChart.Location = New Point(0, 0)
        allocationPreviewChart.Name = "allocationPreviewChart"
        allocationPreviewChart.PreviewMode = PlannerPreviewMode.ResourcesUsed
        allocationPreviewChart.Size = New Size(212, 141)
        allocationPreviewChart.TabIndex = 0
        ' 
        ' allocationLegendGrid
        ' 
        allocationLegendGrid.AllowUserToAddRows = False
        allocationLegendGrid.AllowUserToDeleteRows = False
        allocationLegendGrid.BackgroundColor = Color.White
        allocationLegendGrid.BorderStyle = BorderStyle.None
        allocationLegendGrid.ColumnHeadersHeight = 29
        allocationLegendGrid.Dock = DockStyle.Fill
        allocationLegendGrid.Location = New Point(0, 0)
        allocationLegendGrid.Name = "allocationLegendGrid"
        allocationLegendGrid.ReadOnly = True
        allocationLegendGrid.RowHeadersVisible = False
        allocationLegendGrid.RowHeadersWidth = 51
        allocationLegendGrid.Size = New Size(208, 141)
        allocationLegendGrid.TabIndex = 0
        ' 
        ' allocationPreviewBadges
        ' 
        allocationPreviewBadges.Controls.Add(allocationPrimaryLabel)
        allocationPreviewBadges.Controls.Add(allocationSecondaryLabel)
        allocationPreviewBadges.Dock = DockStyle.Top
        allocationPreviewBadges.Location = New Point(8, 36)
        allocationPreviewBadges.Name = "allocationPreviewBadges"
        allocationPreviewBadges.Padding = New Padding(0, 3, 0, 3)
        allocationPreviewBadges.Size = New Size(424, 34)
        allocationPreviewBadges.TabIndex = 1
        allocationPreviewBadges.WrapContents = False
        ' 
        ' allocationPrimaryLabel
        ' 
        allocationPrimaryLabel.BackColor = Color.FromArgb(CByte(222), CByte(237), CByte(255))
        allocationPrimaryLabel.Location = New Point(3, 3)
        allocationPrimaryLabel.Name = "allocationPrimaryLabel"
        allocationPrimaryLabel.Size = New Size(170, 27)
        allocationPrimaryLabel.TabIndex = 0
        allocationPrimaryLabel.Text = "Resources Selected: 3"
        allocationPrimaryLabel.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' allocationSecondaryLabel
        ' 
        allocationSecondaryLabel.BackColor = Color.FromArgb(CByte(234), CByte(242), CByte(252))
        allocationSecondaryLabel.Location = New Point(179, 3)
        allocationSecondaryLabel.Name = "allocationSecondaryLabel"
        allocationSecondaryLabel.Size = New Size(170, 27)
        allocationSecondaryLabel.TabIndex = 1
        allocationSecondaryLabel.Text = "Assigned Hours: 24 hrs"
        allocationSecondaryLabel.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' allocationPreviewTitle
        ' 
        allocationPreviewTitle.Dock = DockStyle.Top
        allocationPreviewTitle.Font = New Font("Segoe UI Semibold", 11F)
        allocationPreviewTitle.ForeColor = Color.FromArgb(CByte(24), CByte(31), CByte(42))
        allocationPreviewTitle.Location = New Point(8, 8)
        allocationPreviewTitle.Name = "allocationPreviewTitle"
        allocationPreviewTitle.Size = New Size(424, 28)
        allocationPreviewTitle.TabIndex = 0
        allocationPreviewTitle.Text = "Allocation Summary"
        ' 
        ' taskUsageTab
        ' 
        taskUsageTab.BackColor = Color.White
        taskUsageTab.Controls.Add(taskUsageSplit)
        taskUsageTab.Location = New Point(4, 30)
        taskUsageTab.Name = "taskUsageTab"
        taskUsageTab.Padding = New Padding(3)
        taskUsageTab.Size = New Size(1550, 530)
        taskUsageTab.TabIndex = 1
        taskUsageTab.Text = "Task Usage View"
        ' 
        ' taskUsageSplit
        ' 
        taskUsageSplit.Dock = DockStyle.Fill
        taskUsageSplit.Location = New Point(3, 3)
        taskUsageSplit.Name = "taskUsageSplit"
        ' 
        ' taskUsageSplit.Panel1
        ' 
        taskUsageSplit.Panel1.Controls.Add(_taskUsageGrid)
        taskUsageSplit.Panel1MinSize = 680
        ' 
        ' taskUsageSplit.Panel2
        ' 
        taskUsageSplit.Panel2.Controls.Add(taskUsagePreviewPanel)
        taskUsageSplit.Panel2MinSize = 320
        taskUsageSplit.Size = New Size(1544, 524)
        taskUsageSplit.SplitterDistance = 1104
        taskUsageSplit.SplitterWidth = 5
        taskUsageSplit.TabIndex = 0
        ' 
        ' _taskUsageGrid
        ' 
        _taskUsageGrid.AllowUserToAddRows = False
        _taskUsageGrid.AllowUserToDeleteRows = False
        _taskUsageGrid.BackgroundColor = Color.White
        _taskUsageGrid.BorderStyle = BorderStyle.None
        _taskUsageGrid.ColumnHeadersHeight = 32
        _taskUsageGrid.Dock = DockStyle.Fill
        _taskUsageGrid.EnableHeadersVisualStyles = False
        _taskUsageGrid.Location = New Point(0, 0)
        _taskUsageGrid.Name = "_taskUsageGrid"
        _taskUsageGrid.RowHeadersVisible = False
        _taskUsageGrid.RowHeadersWidth = 51
        _taskUsageGrid.SelectionMode = DataGridViewSelectionMode.CellSelect
        _taskUsageGrid.Size = New Size(1104, 524)
        _taskUsageGrid.TabIndex = 0
        ' 
        ' taskUsagePreviewPanel
        ' 
        taskUsagePreviewPanel.BackColor = Color.White
        taskUsagePreviewPanel.Controls.Add(taskUsagePreviewBodySplit)
        taskUsagePreviewPanel.Controls.Add(taskUsagePreviewBadges)
        taskUsagePreviewPanel.Controls.Add(taskUsagePreviewTitle)
        taskUsagePreviewPanel.Dock = DockStyle.Fill
        taskUsagePreviewPanel.Location = New Point(0, 0)
        taskUsagePreviewPanel.Name = "taskUsagePreviewPanel"
        taskUsagePreviewPanel.Padding = New Padding(12)
        taskUsagePreviewPanel.Size = New Size(435, 524)
        taskUsagePreviewPanel.TabIndex = 0
        ' 
        ' taskUsagePreviewBodySplit
        ' 
        taskUsagePreviewBodySplit.Dock = DockStyle.Fill
        taskUsagePreviewBodySplit.Location = New Point(12, 84)
        taskUsagePreviewBodySplit.Name = "taskUsagePreviewBodySplit"
        taskUsagePreviewBodySplit.Orientation = Orientation.Horizontal
        ' 
        ' taskUsagePreviewBodySplit.Panel1
        ' 
        taskUsagePreviewBodySplit.Panel1.Controls.Add(taskUsagePreviewChart)
        ' 
        ' taskUsagePreviewBodySplit.Panel2
        ' 
        taskUsagePreviewBodySplit.Panel2.Controls.Add(taskUsageLegendGrid)
        taskUsagePreviewBodySplit.Size = New Size(411, 428)
        taskUsagePreviewBodySplit.SplitterDistance = 246
        taskUsagePreviewBodySplit.TabIndex = 2
        ' 
        ' taskUsagePreviewChart
        ' 
        taskUsagePreviewChart.BackColor = Color.White
        taskUsagePreviewChart.Dock = DockStyle.Fill
        taskUsagePreviewChart.Location = New Point(0, 0)
        taskUsagePreviewChart.Name = "taskUsagePreviewChart"
        taskUsagePreviewChart.PreviewMode = PlannerPreviewMode.TaskDuration
        taskUsagePreviewChart.Size = New Size(411, 246)
        taskUsagePreviewChart.TabIndex = 0
        ' 
        ' taskUsageLegendGrid
        ' 
        taskUsageLegendGrid.AllowUserToAddRows = False
        taskUsageLegendGrid.AllowUserToDeleteRows = False
        taskUsageLegendGrid.BackgroundColor = Color.White
        taskUsageLegendGrid.BorderStyle = BorderStyle.None
        taskUsageLegendGrid.ColumnHeadersHeight = 29
        taskUsageLegendGrid.Dock = DockStyle.Fill
        taskUsageLegendGrid.Location = New Point(0, 0)
        taskUsageLegendGrid.Name = "taskUsageLegendGrid"
        taskUsageLegendGrid.ReadOnly = True
        taskUsageLegendGrid.RowHeadersVisible = False
        taskUsageLegendGrid.RowHeadersWidth = 51
        taskUsageLegendGrid.Size = New Size(411, 178)
        taskUsageLegendGrid.TabIndex = 0
        ' 
        ' taskUsagePreviewBadges
        ' 
        taskUsagePreviewBadges.Controls.Add(taskUsagePrimaryLabel)
        taskUsagePreviewBadges.Controls.Add(taskUsageSecondaryLabel)
        taskUsagePreviewBadges.Dock = DockStyle.Top
        taskUsagePreviewBadges.Location = New Point(12, 44)
        taskUsagePreviewBadges.Name = "taskUsagePreviewBadges"
        taskUsagePreviewBadges.Padding = New Padding(0, 4, 0, 4)
        taskUsagePreviewBadges.Size = New Size(411, 40)
        taskUsagePreviewBadges.TabIndex = 1
        ' 
        ' taskUsagePrimaryLabel
        ' 
        taskUsagePrimaryLabel.BackColor = Color.FromArgb(CByte(222), CByte(237), CByte(255))
        taskUsagePrimaryLabel.Location = New Point(3, 4)
        taskUsagePrimaryLabel.Name = "taskUsagePrimaryLabel"
        taskUsagePrimaryLabel.Size = New Size(180, 30)
        taskUsagePrimaryLabel.TabIndex = 0
        taskUsagePrimaryLabel.Text = "Scheduled Tasks: 3"
        taskUsagePrimaryLabel.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' taskUsageSecondaryLabel
        ' 
        taskUsageSecondaryLabel.BackColor = Color.FromArgb(CByte(234), CByte(242), CByte(252))
        taskUsageSecondaryLabel.Location = New Point(189, 4)
        taskUsageSecondaryLabel.Name = "taskUsageSecondaryLabel"
        taskUsageSecondaryLabel.Size = New Size(190, 30)
        taskUsageSecondaryLabel.TabIndex = 1
        taskUsageSecondaryLabel.Text = "Project Duration: 3 days"
        taskUsageSecondaryLabel.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' taskUsagePreviewTitle
        ' 
        taskUsagePreviewTitle.Dock = DockStyle.Top
        taskUsagePreviewTitle.Font = New Font("Segoe UI Semibold", 12F)
        taskUsagePreviewTitle.Location = New Point(12, 12)
        taskUsagePreviewTitle.Name = "taskUsagePreviewTitle"
        taskUsagePreviewTitle.Size = New Size(411, 32)
        taskUsagePreviewTitle.TabIndex = 0
        taskUsagePreviewTitle.Text = "Task Load Summary"
        ' 
        ' resourceUsageTab
        ' 
        resourceUsageTab.BackColor = Color.White
        resourceUsageTab.Controls.Add(resourceUsageSplit)
        resourceUsageTab.Location = New Point(4, 30)
        resourceUsageTab.Name = "resourceUsageTab"
        resourceUsageTab.Padding = New Padding(3)
        resourceUsageTab.Size = New Size(1550, 530)
        resourceUsageTab.TabIndex = 2
        resourceUsageTab.Text = "Resource Usage View"
        ' 
        ' resourceUsageSplit
        ' 
        resourceUsageSplit.Dock = DockStyle.Fill
        resourceUsageSplit.Location = New Point(3, 3)
        resourceUsageSplit.Name = "resourceUsageSplit"
        ' 
        ' resourceUsageSplit.Panel1
        ' 
        resourceUsageSplit.Panel1.Controls.Add(_resourceUsageGrid)
        resourceUsageSplit.Panel1MinSize = 680
        ' 
        ' resourceUsageSplit.Panel2
        ' 
        resourceUsageSplit.Panel2.Controls.Add(resourceUsagePreviewPanel)
        resourceUsageSplit.Panel2MinSize = 320
        resourceUsageSplit.Size = New Size(1544, 524)
        resourceUsageSplit.SplitterDistance = 1104
        resourceUsageSplit.SplitterWidth = 5
        resourceUsageSplit.TabIndex = 0
        ' 
        ' _resourceUsageGrid
        ' 
        _resourceUsageGrid.AllowUserToAddRows = False
        _resourceUsageGrid.AllowUserToDeleteRows = False
        _resourceUsageGrid.BackgroundColor = Color.White
        _resourceUsageGrid.BorderStyle = BorderStyle.None
        _resourceUsageGrid.ColumnHeadersHeight = 32
        _resourceUsageGrid.Dock = DockStyle.Fill
        _resourceUsageGrid.EnableHeadersVisualStyles = False
        _resourceUsageGrid.Location = New Point(0, 0)
        _resourceUsageGrid.Name = "_resourceUsageGrid"
        _resourceUsageGrid.RowHeadersVisible = False
        _resourceUsageGrid.RowHeadersWidth = 51
        _resourceUsageGrid.SelectionMode = DataGridViewSelectionMode.CellSelect
        _resourceUsageGrid.Size = New Size(1104, 524)
        _resourceUsageGrid.TabIndex = 0
        ' 
        ' resourceUsagePreviewPanel
        ' 
        resourceUsagePreviewPanel.BackColor = Color.White
        resourceUsagePreviewPanel.Controls.Add(resourceUsagePreviewBodySplit)
        resourceUsagePreviewPanel.Controls.Add(resourceUsagePreviewBadges)
        resourceUsagePreviewPanel.Controls.Add(resourceUsagePreviewTitle)
        resourceUsagePreviewPanel.Dock = DockStyle.Fill
        resourceUsagePreviewPanel.Location = New Point(0, 0)
        resourceUsagePreviewPanel.Name = "resourceUsagePreviewPanel"
        resourceUsagePreviewPanel.Padding = New Padding(12)
        resourceUsagePreviewPanel.Size = New Size(435, 524)
        resourceUsagePreviewPanel.TabIndex = 0
        ' 
        ' resourceUsagePreviewBodySplit
        ' 
        resourceUsagePreviewBodySplit.Dock = DockStyle.Fill
        resourceUsagePreviewBodySplit.Location = New Point(12, 84)
        resourceUsagePreviewBodySplit.Name = "resourceUsagePreviewBodySplit"
        resourceUsagePreviewBodySplit.Orientation = Orientation.Horizontal
        ' 
        ' resourceUsagePreviewBodySplit.Panel1
        ' 
        resourceUsagePreviewBodySplit.Panel1.Controls.Add(resourceUsagePreviewChart)
        ' 
        ' resourceUsagePreviewBodySplit.Panel2
        ' 
        resourceUsagePreviewBodySplit.Panel2.Controls.Add(resourceUsageLegendGrid)
        resourceUsagePreviewBodySplit.Size = New Size(411, 428)
        resourceUsagePreviewBodySplit.SplitterDistance = 246
        resourceUsagePreviewBodySplit.TabIndex = 2
        ' 
        ' resourceUsagePreviewChart
        ' 
        resourceUsagePreviewChart.BackColor = Color.White
        resourceUsagePreviewChart.Dock = DockStyle.Fill
        resourceUsagePreviewChart.Location = New Point(0, 0)
        resourceUsagePreviewChart.Name = "resourceUsagePreviewChart"
        resourceUsagePreviewChart.PreviewMode = PlannerPreviewMode.ResourceContribution
        resourceUsagePreviewChart.Size = New Size(411, 246)
        resourceUsagePreviewChart.TabIndex = 0
        ' 
        ' resourceUsageLegendGrid
        ' 
        resourceUsageLegendGrid.AllowUserToAddRows = False
        resourceUsageLegendGrid.AllowUserToDeleteRows = False
        resourceUsageLegendGrid.BackgroundColor = Color.White
        resourceUsageLegendGrid.BorderStyle = BorderStyle.None
        resourceUsageLegendGrid.ColumnHeadersHeight = 29
        resourceUsageLegendGrid.Dock = DockStyle.Fill
        resourceUsageLegendGrid.Location = New Point(0, 0)
        resourceUsageLegendGrid.Name = "resourceUsageLegendGrid"
        resourceUsageLegendGrid.ReadOnly = True
        resourceUsageLegendGrid.RowHeadersVisible = False
        resourceUsageLegendGrid.RowHeadersWidth = 51
        resourceUsageLegendGrid.Size = New Size(411, 178)
        resourceUsageLegendGrid.TabIndex = 0
        ' 
        ' resourceUsagePreviewBadges
        ' 
        resourceUsagePreviewBadges.Controls.Add(resourceUsagePrimaryLabel)
        resourceUsagePreviewBadges.Controls.Add(resourceUsageSecondaryLabel)
        resourceUsagePreviewBadges.Dock = DockStyle.Top
        resourceUsagePreviewBadges.Location = New Point(12, 44)
        resourceUsagePreviewBadges.Name = "resourceUsagePreviewBadges"
        resourceUsagePreviewBadges.Padding = New Padding(0, 4, 0, 4)
        resourceUsagePreviewBadges.Size = New Size(411, 40)
        resourceUsagePreviewBadges.TabIndex = 1
        ' 
        ' resourceUsagePrimaryLabel
        ' 
        resourceUsagePrimaryLabel.BackColor = Color.FromArgb(CByte(222), CByte(237), CByte(255))
        resourceUsagePrimaryLabel.Location = New Point(3, 4)
        resourceUsagePrimaryLabel.Name = "resourceUsagePrimaryLabel"
        resourceUsagePrimaryLabel.Size = New Size(180, 30)
        resourceUsagePrimaryLabel.TabIndex = 0
        resourceUsagePrimaryLabel.Text = "Contributors: 3"
        resourceUsagePrimaryLabel.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' resourceUsageSecondaryLabel
        ' 
        resourceUsageSecondaryLabel.BackColor = Color.FromArgb(CByte(234), CByte(242), CByte(252))
        resourceUsageSecondaryLabel.Location = New Point(189, 4)
        resourceUsageSecondaryLabel.Name = "resourceUsageSecondaryLabel"
        resourceUsageSecondaryLabel.Size = New Size(190, 30)
        resourceUsageSecondaryLabel.TabIndex = 1
        resourceUsageSecondaryLabel.Text = "Assigned Hours: 24 hrs"
        resourceUsageSecondaryLabel.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' resourceUsagePreviewTitle
        ' 
        resourceUsagePreviewTitle.Dock = DockStyle.Top
        resourceUsagePreviewTitle.Font = New Font("Segoe UI Semibold", 12F)
        resourceUsagePreviewTitle.Location = New Point(12, 12)
        resourceUsagePreviewTitle.Name = "resourceUsagePreviewTitle"
        resourceUsagePreviewTitle.Size = New Size(411, 32)
        resourceUsagePreviewTitle.TabIndex = 0
        resourceUsagePreviewTitle.Text = "Resource Contribution"
        ' 
        ' capacityPlanningTab
        ' 
        capacityPlanningTab.BackColor = Color.White
        capacityPlanningTab.Controls.Add(_capacityGrid)
        capacityPlanningTab.Controls.Add(_capacityFilterPanel)
        capacityPlanningTab.Location = New Point(4, 30)
        capacityPlanningTab.Name = "capacityPlanningTab"
        capacityPlanningTab.Padding = New Padding(3)
        capacityPlanningTab.Size = New Size(1550, 530)
        capacityPlanningTab.TabIndex = 3
        capacityPlanningTab.Text = "Capacity Planning"
        ' 
        ' _capacityGrid
        ' 
        _capacityGrid.AllowUserToAddRows = False
        _capacityGrid.AllowUserToDeleteRows = False
        _capacityGrid.BackgroundColor = Color.White
        _capacityGrid.BorderStyle = BorderStyle.None
        _capacityGrid.ColumnHeadersHeight = 32
        _capacityGrid.Dock = DockStyle.Fill
        _capacityGrid.EnableHeadersVisualStyles = False
        _capacityGrid.Location = New Point(3, 107)
        _capacityGrid.Name = "_capacityGrid"
        _capacityGrid.ReadOnly = True
        _capacityGrid.RowHeadersVisible = False
        _capacityGrid.RowHeadersWidth = 51
        _capacityGrid.Size = New Size(1544, 420)
        _capacityGrid.TabIndex = 1
        ' 
        ' _capacityFilterPanel
        ' 
        _capacityFilterPanel.AutoScroll = True
        _capacityFilterPanel.BackColor = Color.White
        _capacityFilterPanel.Controls.Add(_capacityFromLabel)
        _capacityFilterPanel.Controls.Add(_capacityStartPicker)
        _capacityFilterPanel.Controls.Add(_capacityToLabel)
        _capacityFilterPanel.Controls.Add(_capacityFinishPicker)
        _capacityFilterPanel.Controls.Add(_capacityEmployeesLabel)
        _capacityFilterPanel.Controls.Add(_capacityEmployeeList)
        _capacityFilterPanel.Controls.Add(_capacityApplyButton)
        _capacityFilterPanel.Controls.Add(_capacitySelectAllButton)
        _capacityFilterPanel.Controls.Add(_capacityClearButton)
        _capacityFilterPanel.Controls.Add(_capacityFilterStatusLabel)
        _capacityFilterPanel.Dock = DockStyle.Top
        _capacityFilterPanel.Location = New Point(3, 3)
        _capacityFilterPanel.Name = "_capacityFilterPanel"
        _capacityFilterPanel.Padding = New Padding(10, 8, 10, 6)
        _capacityFilterPanel.Size = New Size(1544, 104)
        _capacityFilterPanel.TabIndex = 0
        _capacityFilterPanel.WrapContents = False
        ' 
        ' _capacityFromLabel
        ' 
        _capacityFromLabel.AutoSize = True
        _capacityFromLabel.ForeColor = Color.FromArgb(CByte(75), CByte(85), CByte(99))
        _capacityFromLabel.Location = New Point(10, 16)
        _capacityFromLabel.Margin = New Padding(0, 8, 6, 0)
        _capacityFromLabel.Name = "_capacityFromLabel"
        _capacityFromLabel.Size = New Size(35, 15)
        _capacityFromLabel.TabIndex = 0
        _capacityFromLabel.Text = "From"
        _capacityFromLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' _capacityStartPicker
        ' 
        _capacityStartPicker.CustomFormat = "dd-MM-yyyy"
        _capacityStartPicker.Format = DateTimePickerFormat.Custom
        _capacityStartPicker.Location = New Point(54, 11)
        _capacityStartPicker.Name = "_capacityStartPicker"
        _capacityStartPicker.Size = New Size(118, 23)
        _capacityStartPicker.TabIndex = 1
        ' 
        ' _capacityToLabel
        ' 
        _capacityToLabel.AutoSize = True
        _capacityToLabel.ForeColor = Color.FromArgb(CByte(75), CByte(85), CByte(99))
        _capacityToLabel.Location = New Point(175, 16)
        _capacityToLabel.Margin = New Padding(0, 8, 6, 0)
        _capacityToLabel.Name = "_capacityToLabel"
        _capacityToLabel.Size = New Size(19, 15)
        _capacityToLabel.TabIndex = 2
        _capacityToLabel.Text = "To"
        _capacityToLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' _capacityFinishPicker
        ' 
        _capacityFinishPicker.CustomFormat = "dd-MM-yyyy"
        _capacityFinishPicker.Format = DateTimePickerFormat.Custom
        _capacityFinishPicker.Location = New Point(203, 11)
        _capacityFinishPicker.Name = "_capacityFinishPicker"
        _capacityFinishPicker.Size = New Size(118, 23)
        _capacityFinishPicker.TabIndex = 3
        ' 
        ' _capacityEmployeesLabel
        ' 
        _capacityEmployeesLabel.AutoSize = True
        _capacityEmployeesLabel.ForeColor = Color.FromArgb(CByte(75), CByte(85), CByte(99))
        _capacityEmployeesLabel.Location = New Point(324, 16)
        _capacityEmployeesLabel.Margin = New Padding(0, 8, 6, 0)
        _capacityEmployeesLabel.Name = "_capacityEmployeesLabel"
        _capacityEmployeesLabel.Size = New Size(64, 15)
        _capacityEmployeesLabel.TabIndex = 4
        _capacityEmployeesLabel.Text = "Employees"
        _capacityEmployeesLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' _capacityEmployeeList
        ' 
        _capacityEmployeeList.CheckOnClick = True
        _capacityEmployeeList.FormattingEnabled = True
        _capacityEmployeeList.IntegralHeight = False
        _capacityEmployeeList.Location = New Point(397, 11)
        _capacityEmployeeList.Name = "_capacityEmployeeList"
        _capacityEmployeeList.Size = New Size(260, 82)
        _capacityEmployeeList.TabIndex = 5
        ' 
        ' _capacityApplyButton
        ' 
        _capacityApplyButton.BackColor = Color.FromArgb(CByte(42), CByte(95), CByte(160))
        _capacityApplyButton.FlatAppearance.BorderSize = 0
        _capacityApplyButton.FlatStyle = FlatStyle.Flat
        _capacityApplyButton.ForeColor = Color.White
        _capacityApplyButton.Location = New Point(668, 13)
        _capacityApplyButton.Margin = New Padding(8, 5, 0, 0)
        _capacityApplyButton.Name = "_capacityApplyButton"
        _capacityApplyButton.Size = New Size(92, 30)
        _capacityApplyButton.TabIndex = 6
        _capacityApplyButton.Text = "Apply"
        _capacityApplyButton.UseVisualStyleBackColor = False
        ' 
        ' _capacitySelectAllButton
        ' 
        _capacitySelectAllButton.BackColor = Color.FromArgb(CByte(35), CByte(46), CByte(66))
        _capacitySelectAllButton.FlatAppearance.BorderSize = 0
        _capacitySelectAllButton.FlatStyle = FlatStyle.Flat
        _capacitySelectAllButton.ForeColor = Color.White
        _capacitySelectAllButton.Location = New Point(768, 13)
        _capacitySelectAllButton.Margin = New Padding(8, 5, 0, 0)
        _capacitySelectAllButton.Name = "_capacitySelectAllButton"
        _capacitySelectAllButton.Size = New Size(92, 30)
        _capacitySelectAllButton.TabIndex = 7
        _capacitySelectAllButton.Text = "Select All"
        _capacitySelectAllButton.UseVisualStyleBackColor = False
        ' 
        ' _capacityClearButton
        ' 
        _capacityClearButton.BackColor = Color.FromArgb(CByte(234), CByte(238), CByte(245))
        _capacityClearButton.FlatAppearance.BorderSize = 0
        _capacityClearButton.FlatStyle = FlatStyle.Flat
        _capacityClearButton.ForeColor = Color.FromArgb(CByte(35), CByte(46), CByte(66))
        _capacityClearButton.Location = New Point(868, 13)
        _capacityClearButton.Margin = New Padding(8, 5, 0, 0)
        _capacityClearButton.Name = "_capacityClearButton"
        _capacityClearButton.Size = New Size(92, 30)
        _capacityClearButton.TabIndex = 8
        _capacityClearButton.Text = "Clear"
        _capacityClearButton.UseVisualStyleBackColor = False
        ' 
        ' _capacityFilterStatusLabel
        ' 
        _capacityFilterStatusLabel.ForeColor = Color.FromArgb(CByte(75), CByte(85), CByte(99))
        _capacityFilterStatusLabel.Location = New Point(963, 8)
        _capacityFilterStatusLabel.Name = "_capacityFilterStatusLabel"
        _capacityFilterStatusLabel.Size = New Size(300, 42)
        _capacityFilterStatusLabel.TabIndex = 9
        _capacityFilterStatusLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' resourceUtilizationTab
        ' 
        resourceUtilizationTab.BackColor = Color.White
        resourceUtilizationTab.Controls.Add(resourceUtilizationHost)
        resourceUtilizationTab.Location = New Point(4, 30)
        resourceUtilizationTab.Name = "resourceUtilizationTab"
        resourceUtilizationTab.Padding = New Padding(3)
        resourceUtilizationTab.Size = New Size(1550, 530)
        resourceUtilizationTab.TabIndex = 4
        resourceUtilizationTab.Text = "Resource Utilization"
        ' 
        ' resourceUtilizationHost
        ' 
        resourceUtilizationHost.Controls.Add(_resourceUtilizationGrid)
        resourceUtilizationHost.Controls.Add(resourceUtilizationToolbar)
        resourceUtilizationHost.Dock = DockStyle.Fill
        resourceUtilizationHost.Location = New Point(3, 3)
        resourceUtilizationHost.Name = "resourceUtilizationHost"
        resourceUtilizationHost.Size = New Size(1544, 524)
        resourceUtilizationHost.TabIndex = 0
        ' 
        ' _resourceUtilizationGrid
        ' 
        _resourceUtilizationGrid.AllowUserToAddRows = False
        _resourceUtilizationGrid.AllowUserToDeleteRows = False
        _resourceUtilizationGrid.BackgroundColor = Color.White
        _resourceUtilizationGrid.BorderStyle = BorderStyle.None
        _resourceUtilizationGrid.ColumnHeadersHeight = 32
        _resourceUtilizationGrid.Dock = DockStyle.Fill
        _resourceUtilizationGrid.EnableHeadersVisualStyles = False
        _resourceUtilizationGrid.Location = New Point(0, 48)
        _resourceUtilizationGrid.Name = "_resourceUtilizationGrid"
        _resourceUtilizationGrid.RowHeadersVisible = False
        _resourceUtilizationGrid.RowHeadersWidth = 51
        _resourceUtilizationGrid.SelectionMode = DataGridViewSelectionMode.CellSelect
        _resourceUtilizationGrid.Size = New Size(1544, 476)
        _resourceUtilizationGrid.TabIndex = 1
        ' 
        ' resourceUtilizationToolbar
        ' 
        resourceUtilizationToolbar.BackColor = Color.White
        resourceUtilizationToolbar.Controls.Add(_resourceUtilizationRefreshButton)
        resourceUtilizationToolbar.Controls.Add(_resourceUtilizationColorSelector)
        resourceUtilizationToolbar.Controls.Add(_resourceUtilizationApplyButton)
        resourceUtilizationToolbar.Controls.Add(_resourceUtilizationClearButton)
        resourceUtilizationToolbar.Controls.Add(_resourceUtilizationMailButton)
        resourceUtilizationToolbar.Dock = DockStyle.Top
        resourceUtilizationToolbar.Location = New Point(0, 0)
        resourceUtilizationToolbar.Name = "resourceUtilizationToolbar"
        resourceUtilizationToolbar.Padding = New Padding(10, 8, 10, 6)
        resourceUtilizationToolbar.Size = New Size(1544, 48)
        resourceUtilizationToolbar.TabIndex = 0
        resourceUtilizationToolbar.WrapContents = False
        ' 
        ' _resourceUtilizationRefreshButton
        ' 
        _resourceUtilizationRefreshButton.BackColor = Color.FromArgb(CByte(42), CByte(95), CByte(160))
        _resourceUtilizationRefreshButton.FlatStyle = FlatStyle.Flat
        _resourceUtilizationRefreshButton.ForeColor = Color.White
        _resourceUtilizationRefreshButton.Location = New Point(13, 11)
        _resourceUtilizationRefreshButton.Name = "_resourceUtilizationRefreshButton"
        _resourceUtilizationRefreshButton.Size = New Size(130, 28)
        _resourceUtilizationRefreshButton.TabIndex = 0
        _resourceUtilizationRefreshButton.Text = "Refresh SQL Hours"
        _resourceUtilizationRefreshButton.UseVisualStyleBackColor = False
        ' 
        ' _resourceUtilizationColorSelector
        ' 
        _resourceUtilizationColorSelector.DropDownStyle = ComboBoxStyle.DropDownList
        _resourceUtilizationColorSelector.FormattingEnabled = True
        _resourceUtilizationColorSelector.Items.AddRange(New Object() {"Blue - Planned Leave", "Dark Blue - Unplanned Leave", "Yellow - Training", "Green - Weekend Work", "Orange - Pending Work", "Red - Unassigned Hours"})
        _resourceUtilizationColorSelector.Location = New Point(149, 11)
        _resourceUtilizationColorSelector.Name = "_resourceUtilizationColorSelector"
        _resourceUtilizationColorSelector.Size = New Size(180, 23)
        _resourceUtilizationColorSelector.TabIndex = 1
        ' 
        ' _resourceUtilizationApplyButton
        ' 
        _resourceUtilizationApplyButton.FlatStyle = FlatStyle.Flat
        _resourceUtilizationApplyButton.Location = New Point(335, 11)
        _resourceUtilizationApplyButton.Name = "_resourceUtilizationApplyButton"
        _resourceUtilizationApplyButton.Size = New Size(120, 28)
        _resourceUtilizationApplyButton.TabIndex = 2
        _resourceUtilizationApplyButton.Text = "Apply Highlight"
        ' 
        ' _resourceUtilizationClearButton
        ' 
        _resourceUtilizationClearButton.FlatStyle = FlatStyle.Flat
        _resourceUtilizationClearButton.Location = New Point(461, 11)
        _resourceUtilizationClearButton.Name = "_resourceUtilizationClearButton"
        _resourceUtilizationClearButton.Size = New Size(120, 28)
        _resourceUtilizationClearButton.TabIndex = 3
        _resourceUtilizationClearButton.Text = "Clear Highlight"
        ' 
        ' _resourceUtilizationMailButton
        ' 
        _resourceUtilizationMailButton.BackColor = Color.FromArgb(CByte(34), CByte(169), CByte(105))
        _resourceUtilizationMailButton.FlatStyle = FlatStyle.Flat
        _resourceUtilizationMailButton.ForeColor = Color.White
        _resourceUtilizationMailButton.Location = New Point(587, 11)
        _resourceUtilizationMailButton.Name = "_resourceUtilizationMailButton"
        _resourceUtilizationMailButton.Size = New Size(170, 28)
        _resourceUtilizationMailButton.TabIndex = 4
        _resourceUtilizationMailButton.Text = "Send Availability Snip"
        _resourceUtilizationMailButton.UseVisualStyleBackColor = False
        ' 
        ' employeeCapacityTab
        ' 
        employeeCapacityTab.BackColor = Color.White
        employeeCapacityTab.Controls.Add(employeeCapacityHost)
        employeeCapacityTab.Location = New Point(4, 30)
        employeeCapacityTab.Name = "employeeCapacityTab"
        employeeCapacityTab.Padding = New Padding(3)
        employeeCapacityTab.Size = New Size(1550, 530)
        employeeCapacityTab.TabIndex = 5
        employeeCapacityTab.Text = "Employee Capacity"
        ' 
        ' employeeCapacityHost
        ' 
        employeeCapacityHost.Controls.Add(_employeeCapacityGrid)
        employeeCapacityHost.Controls.Add(employeeCapacityToolbar)
        employeeCapacityHost.Dock = DockStyle.Fill
        employeeCapacityHost.Location = New Point(3, 3)
        employeeCapacityHost.Name = "employeeCapacityHost"
        employeeCapacityHost.Size = New Size(1544, 524)
        employeeCapacityHost.TabIndex = 0
        ' 
        ' _employeeCapacityGrid
        ' 
        _employeeCapacityGrid.AllowUserToAddRows = False
        _employeeCapacityGrid.AllowUserToDeleteRows = False
        _employeeCapacityGrid.BackgroundColor = Color.White
        _employeeCapacityGrid.BorderStyle = BorderStyle.None
        _employeeCapacityGrid.ColumnHeadersHeight = 32
        _employeeCapacityGrid.Dock = DockStyle.Fill
        _employeeCapacityGrid.EnableHeadersVisualStyles = False
        _employeeCapacityGrid.Location = New Point(0, 48)
        _employeeCapacityGrid.Name = "_employeeCapacityGrid"
        _employeeCapacityGrid.RowHeadersVisible = False
        _employeeCapacityGrid.RowHeadersWidth = 51
        _employeeCapacityGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        _employeeCapacityGrid.Size = New Size(1544, 476)
        _employeeCapacityGrid.TabIndex = 1
        ' 
        ' employeeCapacityToolbar
        ' 
        employeeCapacityToolbar.BackColor = Color.White
        employeeCapacityToolbar.Controls.Add(_employeeCapacityAddButton)
        employeeCapacityToolbar.Controls.Add(_employeeCapacityDeleteButton)
        employeeCapacityToolbar.Controls.Add(_employeeCapacityRefreshButton)
        employeeCapacityToolbar.Dock = DockStyle.Top
        employeeCapacityToolbar.Location = New Point(0, 0)
        employeeCapacityToolbar.Name = "employeeCapacityToolbar"
        employeeCapacityToolbar.Padding = New Padding(10, 8, 10, 6)
        employeeCapacityToolbar.Size = New Size(1544, 48)
        employeeCapacityToolbar.TabIndex = 0
        employeeCapacityToolbar.WrapContents = False
        ' 
        ' _employeeCapacityAddButton
        ' 
        _employeeCapacityAddButton.BackColor = Color.FromArgb(CByte(34), CByte(169), CByte(105))
        _employeeCapacityAddButton.FlatStyle = FlatStyle.Flat
        _employeeCapacityAddButton.ForeColor = Color.White
        _employeeCapacityAddButton.Location = New Point(13, 11)
        _employeeCapacityAddButton.Name = "_employeeCapacityAddButton"
        _employeeCapacityAddButton.Size = New Size(130, 28)
        _employeeCapacityAddButton.TabIndex = 0
        _employeeCapacityAddButton.Text = "Add Capacity"
        _employeeCapacityAddButton.UseVisualStyleBackColor = False
        ' 
        ' _employeeCapacityDeleteButton
        ' 
        _employeeCapacityDeleteButton.BackColor = Color.FromArgb(CByte(35), CByte(46), CByte(66))
        _employeeCapacityDeleteButton.FlatStyle = FlatStyle.Flat
        _employeeCapacityDeleteButton.ForeColor = Color.White
        _employeeCapacityDeleteButton.Location = New Point(149, 11)
        _employeeCapacityDeleteButton.Name = "_employeeCapacityDeleteButton"
        _employeeCapacityDeleteButton.Size = New Size(130, 28)
        _employeeCapacityDeleteButton.TabIndex = 1
        _employeeCapacityDeleteButton.Text = "Delete Entry"
        _employeeCapacityDeleteButton.UseVisualStyleBackColor = False
        ' 
        ' _employeeCapacityRefreshButton
        ' 
        _employeeCapacityRefreshButton.BackColor = Color.FromArgb(CByte(42), CByte(95), CByte(160))
        _employeeCapacityRefreshButton.FlatStyle = FlatStyle.Flat
        _employeeCapacityRefreshButton.ForeColor = Color.White
        _employeeCapacityRefreshButton.Location = New Point(285, 11)
        _employeeCapacityRefreshButton.Name = "_employeeCapacityRefreshButton"
        _employeeCapacityRefreshButton.Size = New Size(150, 28)
        _employeeCapacityRefreshButton.TabIndex = 2
        _employeeCapacityRefreshButton.Text = "Refresh Capacity"
        _employeeCapacityRefreshButton.UseVisualStyleBackColor = False
        ' 
        ' _detailsPanel
        ' 
        _detailsPanel.BackColor = Color.White
        _detailsPanel.Controls.Add(taskWorkspaceTitle)
        _detailsPanel.Dock = DockStyle.Fill
        _detailsPanel.Location = New Point(0, 0)
        _detailsPanel.Margin = New Padding(3, 4, 3, 4)
        _detailsPanel.Name = "_detailsPanel"
        _detailsPanel.Padding = New Padding(16, 19, 16, 19)
        _detailsPanel.Size = New Size(150, 46)
        _detailsPanel.TabIndex = 0
        ' 
        ' taskWorkspaceTitle
        ' 
        taskWorkspaceTitle.Dock = DockStyle.Top
        taskWorkspaceTitle.Font = New Font("Segoe UI Semibold", 10.5F)
        taskWorkspaceTitle.ForeColor = Color.FromArgb(CByte(24), CByte(31), CByte(42))
        taskWorkspaceTitle.Location = New Point(16, 19)
        taskWorkspaceTitle.Name = "taskWorkspaceTitle"
        taskWorkspaceTitle.Size = New Size(118, 37)
        taskWorkspaceTitle.TabIndex = 0
        taskWorkspaceTitle.Text = "Task allocation"
        ' 
        ' statusBar
        ' 
        statusBar.BackColor = Color.White
        statusBar.ImageScalingSize = New Size(20, 20)
        statusBar.Items.AddRange(New ToolStripItem() {_status})
        statusBar.Location = New Point(0, 815)
        statusBar.Name = "statusBar"
        statusBar.Padding = New Padding(1, 0, 16, 0)
        statusBar.Size = New Size(1558, 22)
        statusBar.TabIndex = 3
        ' 
        ' _status
        ' 
        _status.Name = "_status"
        _status.Size = New Size(39, 17)
        _status.Text = "Ready"
        ' 
        ' SMASchedulerForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(249))
        ClientSize = New Size(1558, 837)
        Controls.Add(contentSplit)
        Controls.Add(statusBar)
        Controls.Add(headerPanel)
        Controls.Add(commandBar)
        Font = New Font("Segoe UI", 9F)
        MinimumSize = New Size(1122, 580)
        Name = "SMASchedulerForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SMA Scheduler"
        commandBar.ResumeLayout(False)
        commandBar.PerformLayout()
        headerPanel.ResumeLayout(False)
        headerPanel.PerformLayout()
        CType(_totalProjectHours, ComponentModel.ISupportInitialize).EndInit()
        contentSplit.Panel1.ResumeLayout(False)
        contentSplit.Panel2.ResumeLayout(False)
        CType(contentSplit, ComponentModel.ISupportInitialize).EndInit()
        contentSplit.ResumeLayout(False)
        _workspaceTabs.ResumeLayout(False)
        taskAllocationTab.ResumeLayout(False)
        mainSplit.Panel1.ResumeLayout(False)
        mainSplit.Panel2.ResumeLayout(False)
        CType(mainSplit, ComponentModel.ISupportInitialize).EndInit()
        mainSplit.ResumeLayout(False)
        CType(_grid, ComponentModel.ISupportInitialize).EndInit()
        ganttPreviewSplit.Panel1.ResumeLayout(False)
        ganttPreviewSplit.Panel2.ResumeLayout(False)
        CType(ganttPreviewSplit, ComponentModel.ISupportInitialize).EndInit()
        ganttPreviewSplit.ResumeLayout(False)
        allocationPreviewPanel.ResumeLayout(False)
        allocationPreviewBodySplit.Panel1.ResumeLayout(False)
        allocationPreviewBodySplit.Panel2.ResumeLayout(False)
        CType(allocationPreviewBodySplit, ComponentModel.ISupportInitialize).EndInit()
        allocationPreviewBodySplit.ResumeLayout(False)
        CType(allocationLegendGrid, ComponentModel.ISupportInitialize).EndInit()
        allocationPreviewBadges.ResumeLayout(False)
        taskUsageTab.ResumeLayout(False)
        taskUsageSplit.Panel1.ResumeLayout(False)
        taskUsageSplit.Panel2.ResumeLayout(False)
        CType(taskUsageSplit, ComponentModel.ISupportInitialize).EndInit()
        taskUsageSplit.ResumeLayout(False)
        CType(_taskUsageGrid, ComponentModel.ISupportInitialize).EndInit()
        taskUsagePreviewPanel.ResumeLayout(False)
        taskUsagePreviewBodySplit.Panel1.ResumeLayout(False)
        taskUsagePreviewBodySplit.Panel2.ResumeLayout(False)
        CType(taskUsagePreviewBodySplit, ComponentModel.ISupportInitialize).EndInit()
        taskUsagePreviewBodySplit.ResumeLayout(False)
        CType(taskUsageLegendGrid, ComponentModel.ISupportInitialize).EndInit()
        taskUsagePreviewBadges.ResumeLayout(False)
        resourceUsageTab.ResumeLayout(False)
        resourceUsageSplit.Panel1.ResumeLayout(False)
        resourceUsageSplit.Panel2.ResumeLayout(False)
        CType(resourceUsageSplit, ComponentModel.ISupportInitialize).EndInit()
        resourceUsageSplit.ResumeLayout(False)
        CType(_resourceUsageGrid, ComponentModel.ISupportInitialize).EndInit()
        resourceUsagePreviewPanel.ResumeLayout(False)
        resourceUsagePreviewBodySplit.Panel1.ResumeLayout(False)
        resourceUsagePreviewBodySplit.Panel2.ResumeLayout(False)
        CType(resourceUsagePreviewBodySplit, ComponentModel.ISupportInitialize).EndInit()
        resourceUsagePreviewBodySplit.ResumeLayout(False)
        CType(resourceUsageLegendGrid, ComponentModel.ISupportInitialize).EndInit()
        resourceUsagePreviewBadges.ResumeLayout(False)
        capacityPlanningTab.ResumeLayout(False)
        CType(_capacityGrid, ComponentModel.ISupportInitialize).EndInit()
        _capacityFilterPanel.ResumeLayout(False)
        _capacityFilterPanel.PerformLayout()
        resourceUtilizationTab.ResumeLayout(False)
        resourceUtilizationHost.ResumeLayout(False)
        CType(_resourceUtilizationGrid, ComponentModel.ISupportInitialize).EndInit()
        resourceUtilizationToolbar.ResumeLayout(False)
        employeeCapacityTab.ResumeLayout(False)
        employeeCapacityHost.ResumeLayout(False)
        CType(_employeeCapacityGrid, ComponentModel.ISupportInitialize).EndInit()
        employeeCapacityToolbar.ResumeLayout(False)
        _detailsPanel.ResumeLayout(False)
        statusBar.ResumeLayout(False)
        statusBar.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Private commandBar As ToolStrip
    Private WithEvents btnSave As ToolStripButton
    Private WithEvents btnRefreshCapacity As ToolStripButton
    Private sepFile As ToolStripSeparator
    Private WithEvents btnAddTask As ToolStripButton
    Private WithEvents btnDelete As ToolStripButton
    Private WithEvents btnMoveUp As ToolStripButton
    Private WithEvents btnMoveDown As ToolStripButton
    Private sepTasks As ToolStripSeparator
    Private WithEvents btnLink As ToolStripButton
    Private WithEvents btnUnlink As ToolStripButton
    Private WithEvents btnMilestone As ToolStripButton
    Private WithEvents headerPanel As Panel
    Private appTitle As Label
    Private projectLabel As Label
    Private versionLabel As Label
    Private totalHoursLabel As Label
    Private contentSplit As SplitContainer
    Private _workspaceTabs As TabControl
    Private taskAllocationTab As TabPage
    Private taskUsageTab As TabPage
    Private resourceUsageTab As TabPage
    Private capacityPlanningTab As TabPage
    Private resourceUtilizationTab As TabPage
    Private employeeCapacityTab As TabPage
    Private WithEvents mainSplit As SplitContainer
    Private ganttPreviewSplit As SplitContainer
    Private allocationPreviewPanel As Panel
    Private allocationPreviewTitle As Label
    Private allocationPreviewBadges As FlowLayoutPanel
    Private allocationPrimaryLabel As Label
    Private allocationSecondaryLabel As Label
    Private allocationPreviewBodySplit As SplitContainer
    Private allocationPreviewChart As PlannerPieChartPanel
    Private allocationLegendGrid As DataGridView
    Private WithEvents taskUsageSplit As SplitContainer
    Private WithEvents _taskUsageGrid As DataGridView
    Private taskUsagePreviewPanel As Panel
    Private taskUsagePreviewTitle As Label
    Private taskUsagePreviewBadges As FlowLayoutPanel
    Private taskUsagePrimaryLabel As Label
    Private taskUsageSecondaryLabel As Label
    Private taskUsagePreviewBodySplit As SplitContainer
    Private taskUsagePreviewChart As PlannerPieChartPanel
    Private taskUsageLegendGrid As DataGridView
    Private WithEvents resourceUsageSplit As SplitContainer
    Private WithEvents _resourceUsageGrid As DataGridView
    Private resourceUsagePreviewPanel As Panel
    Private resourceUsagePreviewTitle As Label
    Private resourceUsagePreviewBadges As FlowLayoutPanel
    Private resourceUsagePrimaryLabel As Label
    Private resourceUsageSecondaryLabel As Label
    Private resourceUsagePreviewBodySplit As SplitContainer
    Private resourceUsagePreviewChart As PlannerPieChartPanel
    Private resourceUsageLegendGrid As DataGridView
    Private _capacityFilterPanel As FlowLayoutPanel
    Private _capacityFromLabel As Label
    Private WithEvents _capacityStartPicker As DateTimePicker
    Private _capacityToLabel As Label
    Private WithEvents _capacityFinishPicker As DateTimePicker
    Private _capacityEmployeesLabel As Label
    Private _capacityEmployeeList As CheckedListBox
    Private WithEvents _capacityApplyButton As Button
    Private WithEvents _capacitySelectAllButton As Button
    Private WithEvents _capacityClearButton As Button
    Private _capacityFilterStatusLabel As Label
    Private _capacityGrid As DataGridView
    Private resourceUtilizationHost As Panel
    Private resourceUtilizationToolbar As FlowLayoutPanel
    Private WithEvents _resourceUtilizationRefreshButton As Button
    Private _resourceUtilizationColorSelector As ComboBox
    Private WithEvents _resourceUtilizationApplyButton As Button
    Private WithEvents _resourceUtilizationClearButton As Button
    Private WithEvents _resourceUtilizationMailButton As Button
    Private WithEvents _resourceUtilizationGrid As DataGridView
    Private employeeCapacityHost As Panel
    Private employeeCapacityToolbar As FlowLayoutPanel
    Private WithEvents _employeeCapacityAddButton As Button
    Private WithEvents _employeeCapacityDeleteButton As Button
    Private WithEvents _employeeCapacityRefreshButton As Button
    Private WithEvents _employeeCapacityGrid As DataGridView
    Private projectSizeLabel As Label
    Private taskCatalogLabel As Label
    Private taskWorkspaceTitle As Label
    Private statusBar As StatusStrip
    Private _projectName As TextBox
    Private _versionNumber As TextBox
    Private WithEvents _totalProjectHours As BlankNumericUpDown
    Private WithEvents _taskCatalogSelector As ComboBox
    Private WithEvents _projectSizeSelector As ComboBox
    Private WithEvents _includeSaturdays As CheckBox
    Private _summaryTitle As Label
    Private _summaryDates As Label
    Private _summaryResources As Label
    Private _projectDetailsCaptionLabel As Label
    Private _projectDetailsValueLabel As Label
    Private WithEvents _scheduleProjectButton As Button
    Private WithEvents _grid As DataGridView
    Private _gantt As GanttPanel
    Private _detailsPanel As Panel
    Private _status As ToolStripStatusLabel
End Class
