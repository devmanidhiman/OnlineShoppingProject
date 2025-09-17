# Copilot Instructions for Online Shopping Project

## Project Overview
This is a C#-based Online Shopping System. The workspace currently contains documentation (`Online Shopping System Project Guide.docx`). Source code files and implementation details may be added in the future.

## Architecture & Components
- Expect a multi-layered architecture typical for C# projects: Presentation (UI), Business Logic, Data Access, and Models.
- Key files and folders will likely include:
  - `Models/` for data entities
  - `Services/` or `BusinessLogic/` for core operations
  - `Data/` or `Repositories/` for database access
  - `UI/` or `Views/` for user interface components
- Data flow: UI → Business Logic → Data Access → Database

## Developer Workflows
- Build: Use Visual Studio or `dotnet build` from the command line.
- Run: Use Visual Studio's debugger or `dotnet run`.
- Tests: If present, run with `dotnet test`.
- Debugging: Prefer Visual Studio's integrated debugger for step-through and breakpoints.

## Project-Specific Conventions
- Naming: Classes and files use PascalCase. Methods use PascalCase. Variables use camelCase.
- Folder structure follows C# conventions (see above).
- Documentation is maintained in `.docx` format; keep technical docs in Markdown for code-related guidance.

## Integration & Dependencies
- External dependencies managed via NuGet (`.csproj` files).
- Database integration may use Entity Framework or ADO.NET.
- Cross-component communication via method calls and dependency injection.

## Examples
- For a new model, add to `Models/` and follow PascalCase naming.
- For a new service, add to `Services/` and inject dependencies via constructor.
- For database changes, update `Data/` or `Repositories/` and ensure migrations are handled if using Entity Framework.

## Key Files
- `Online Shopping System Project Guide.docx`: High-level requirements and design.
- `.github/copilot-instructions.md`: AI agent guidance (this file).

## AI Agent Guidance
- Always check for updates in the `.docx` guide before making architectural changes.
- Follow C# and .NET best practices unless project documentation specifies otherwise.
- If source code is missing, prompt the user to add or specify files for implementation.

---
*Update this file as the project evolves. Reference new patterns, workflows, or conventions as they are introduced.*
