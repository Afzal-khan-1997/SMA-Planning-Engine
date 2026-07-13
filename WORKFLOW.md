# SMA Planning Workflow

## Project ID Search To Scheduler

1. The user enters a Project ID in the SMA Planning Engine search box.
2. When **Schedule Project** is clicked, `SMAPlannerForm.btnScheduleProject_Click` queries SQL using that Project ID.
3. SQL project details are read from `Version_Table`, with project size joined from `Table_Project_Tracking`.
4. The app builds a `LiveProjectItem` using SQL values as the source of truth:
   - Project ID
   - Project name
   - Version
   - Project type/report type
   - Project size
   - Planning flags and project details
5. `SMASchedulerForm.LoadLiveProjectTemplate` receives that `LiveProjectItem`.
6. The Scheduler form selects the matching project size in `_projectSizeSelector`.
7. `TaskCatalogService.LoadTemplateTasks` loads the task template.
8. Each task uses `TaskCatalogItem.HoursForSize(projectSize)` to choose the correct resource hours:
   - `SmallHours`
   - `MediumHours`
   - `LargeHours`
   - `VeryLargeHours`

## Important Rule

The Scheduler form has `Small` as the UI default only so the form has a valid initial value. For real Project ID planning, the project size should come from SQL `Table_Project_Tracking.[Project Size]`, and that size controls which resource-hour column is used in the scheduler.

## Code Locations

| Workflow step | File/method |
| --- | --- |
| Project ID button action | `SMAPlannerForm.btnScheduleProject_Click` |
| SQL project lookup | `SqlProjectRepository.GetProjectPlanningInfo` |
| SQL project-size conversion | `SqlProjectRepository.ProjectSizeName` |
| Scheduler handoff | `SMASchedulerForm.LoadLiveProjectTemplate` |
| Template task loading | `TaskCatalogService.LoadTemplateTasks` |
| Size-based hours selection | `TaskCatalogItem.HoursForSize` |
