# SMA Planning Engine - .NET 8

Smart Planning system designed for Optimized Resource Management.

SMA Planning Engine is a VB.NET Windows Forms planning tool migrated from the SMA Scheduler .NET Framework 4.8 project to a cleaner .NET 8 project layout.

## Included

- SMA Planning Engine start form with Project ID search, schedule-project flow, recent schedules, and 20th-to-20th project counters.
- SMA Scheduler form with editable task grid and custom Gantt timeline.
- Existing code-behind handlers and designer layouts preserved for easier debugging.
- Motion transitions between the planning and scheduling forms through `FormTransitionService`.
- FS, SS, FF, and SF task dependencies.
- Editable task dates, decimal durations, percent complete, multiple resources, assignment dates, and decimal resource hours.
- Weekend Plan option for scheduling Saturdays and Sundays.
- SQL-backed Capacity Planning with date range and employee filters.
- Persistent color themes shared by SMA Planning Engine and SMA Scheduler.
- SQL-backed project lookup, task loading, capacity loading, and schedule saving.

## Requirements

- Windows 10 or Windows 11.
- Visual Studio 2022 with the .NET desktop development workload.
- .NET 8 SDK.

## Open And Build

Open `SMA Planning Engine.sln` in Visual Studio 2022 and select **Build > Build Solution**.

Command-line build:

```powershell
dotnet build ".\SMA Planning Engine.sln"
```

The Debug executable is written to:

```text
bin\Debug\net8.0-windows\SMA Planning Engine.exe
```

## Frontend And Code-Behind

WinForms frontend controls are kept in the `.Designer.vb` files, while button behavior lives in the matching `.vb` code-behind files. See `BUTTON_ACTIONS.md` for the button-to-handler map.

## Planning Workflow

Project ID planning uses SQL project tracking as the source of truth. The user enters a Project ID, SQL returns project name and project size, and the Scheduler loads task resource hours based on that project size. See `WORKFLOW.md` for the full flow.

The Capacity Planning tab also supports SQL-backed filtering by date range and active employees. It shows available hours, planned hours, project names, and task-level planned hours for the selected employees and dates.

## SQL Server

SQL is now the intended source of truth. Add the production connection string in `App.config`, and keep table mapping changes inside `SqlProjectRepository.vb`. The planned schema is available in `schema.sql`.
