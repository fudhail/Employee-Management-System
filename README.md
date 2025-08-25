# Employee Management System (ASP.NET MVC)

A web-based **Employee Management System** built using **ASP.NET MVC**
framework.\
It helps manage employee records, track leaves, and generate reports
through **Crystal Reports integration**.

------------------------------------------------------------------------

## 📌 Features

-   👨‍💼 **Employee Management** -- Add, update, view, and delete employee
    details.\
-   📅 **Leave Management** -- Record and manage employee leave
    applications.\
-   📊 **Crystal Reports Integration** -- Generate employee and leave
    reports in `.rpt` format.\
-   🎨 **Razor Views** -- Clean and modular UI using MVC views
    (`.cshtml`).\
-   ⚙️ **Configurable Settings** -- Centralized settings in `Web.config`
    and `Global.asax`.\
-   📦 **NuGet Dependency Management** -- Packages handled via
    `packages.config`.

------------------------------------------------------------------------

## 🗂️ Project Structure

    Employee/
    │── App_Data/        # Local database files (if any)
    │── App_Start/       # App settings & configuration
    │── Controllers/     # Handles application logic
    │── Models/          # Defines data structures
    │── Views/           # Razor view templates (UI)
    │── Reports/         # Crystal Reports (.rpt files)
    │── ReportView/      # Report viewer ASPX pages
    │── Content/         # CSS, images, static assets
    │── Scripts/         # JavaScript files
    │── Global.asax      # Application startup logic
    │── Web.config       # Application configuration
    │── packages.config  # NuGet package references
    │── Employee.sln     # Visual Studio solution file

------------------------------------------------------------------------

## 🚀 Getting Started

### Prerequisites

-   [Visual Studio](https://visualstudio.microsoft.com/) (2019 or
    later)\
-   .NET Framework (4.x)\
-   [Crystal Reports for Visual
    Studio](https://www.sap.com/community/topic/crystal-reports.html)

### Setup

1.  Clone the repository:

    ``` bash
    git clone https://github.com/your-username/employee-management-system.git
    ```

2.  Open the solution in **Visual Studio**.\

3.  Restore NuGet packages:

    ``` bash
    Update-Package -reinstall
    ```

4.  Build and run the project (`Ctrl + F5`).

------------------------------------------------------------------------

## 📊 Reports

-   Employee List Report (`EmployeeReport.rpt`)\
-   Leave Records Report (`LeaveReport.rpt`)

Reports can be viewed via the built-in **Crystal Report Viewer**.

------------------------------------------------------------------------

## 📷 Screenshots *(optional)*

*Add screenshots of your app UI here once available.*

------------------------------------------------------------------------

## 🤝 Contributing

Contributions are welcome! Please fork the repo and create a pull
request.

------------------------------------------------------------------------

## 📜 License

This project is licensed under the MIT License.
