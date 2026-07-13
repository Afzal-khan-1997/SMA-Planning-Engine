# Button Action Map

This project keeps the WinForms frontend controls in the `.Designer.vb` files and the button behavior in the matching form `.vb` code-behind files.

## SMA Planning Engine Start Form

| Frontend control | Code-behind handler | Backend action |
| --- | --- | --- |
| `btnScheduleProject` | `btnScheduleProject_Click` in `SMAPlannerForm.vb` | Validates the selected live project/template and opens `SMASchedulerForm`. |

## SMA Scheduler Toolbar

| Frontend control | Code-behind handler | Backend action |
| --- | --- | --- |
| `btnSave` | `btnSave_Click` | Validates task availability and saves the project through `SaveProjectToSql`. |
| `btnRefreshCapacity` | `btnRefreshCapacity_Click` | Commits grid edits, recalculates capacity, saves SQL, and refreshes workspace tabs. |
| `btnAddTask` | `btnAddTask_Click` | Adds a new schedule task and recalculates the plan. |
| `btnDelete` | `btnDelete_Click` | Deletes the selected task, remaps predecessors, renumbers tasks, and recalculates. |
| `btnMoveUp` | `btnMoveUp_Click` | Moves the selected task up. |
| `btnMoveDown` | `btnMoveDown_Click` | Moves the selected task down. |
| `btnLink` | `btnLink_Click` | Links the selected task to the previous task with an FS dependency. |
| `btnUnlink` | `btnUnlink_Click` | Clears dependencies from the selected task. |
| `btnMilestone` | `btnMilestone_Click` | Toggles the selected task into a milestone-style task. |

## SMA Scheduler Header

| Frontend control | Code-behind handler | Backend action |
| --- | --- | --- |
| `_scheduleProjectButton` | `_scheduleProjectButton_Click` | Calls `ScheduleProjectFromHeader`, validates assigned tasks, and saves the project. |

## Resource Utilization Tab

| Frontend control | Code-behind handler | Backend action |
| --- | --- | --- |
| `_resourceUtilizationRefreshButton` | `_resourceUtilizationRefreshButton_Click` | Calls `RefreshResourceUtilizationTab` to reload resource availability and refresh the grid. |
| `_resourceUtilizationApplyButton` | `_resourceUtilizationApplyButton_Click` | Calls `ApplyResourceUtilizationHighlight` to apply the selected highlight color. |
| `_resourceUtilizationClearButton` | `_resourceUtilizationClearButton_Click` | Calls `ClearResourceUtilizationHighlight` to remove highlights from selected cells. |
| `_resourceUtilizationMailButton` | `_resourceUtilizationMailButton_Click` | Calls `SendResourceUtilizationSnip` to capture and email/copy the availability snip. |

## Employee Capacity Tab

| Frontend control | Code-behind handler | Backend action |
| --- | --- | --- |
| `_employeeCapacityAddButton` | `_employeeCapacityAddButton_Click` | Calls `AddEmployeeCapacityEntry` to add a leave/training/meeting capacity entry. |
| `_employeeCapacityDeleteButton` | `_employeeCapacityDeleteButton_Click` | Calls `DeleteEmployeeCapacityEntry` to remove the selected capacity entry. |
| `_employeeCapacityRefreshButton` | `_employeeCapacityRefreshButton_Click` | Calls `RefreshEmployeeCapacityView` to refresh employee capacity data. |
