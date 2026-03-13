# PrepMaster

![C#](https://img.shields.io/badge/backend-C%23-239120?style=flat-square&logo=csharp)
![JavaScript](https://img.shields.io/badge/frontend-JavaScript-F7DF1E?style=flat-square&logo=javascript)
![Razor](https://img.shields.io/badge/views-Razor%20HTML-512BD4?style=flat-square&logo=dotnet)
![ASP.NET MVC](https://img.shields.io/badge/framework-ASP.NET%20MVC%205-512BD4?style=flat-square&logo=dotnet)
![SQL Server](https://img.shields.io/badge/database-SQL%20Server-CC2927?style=flat-square&logo=microsoft-sql-server)
[![.NET CI](https://github.com/soumyodipsays/PrepMaster/actions/workflows/dotnet.yml/badge.svg)](https://github.com/soumyodipsays/PrepMaster/actions/workflows/dotnet.yml)

## Overview

**PrepMaster** is a comprehensive online exam preparation and test management platform designed to streamline the learning experience for students and provide analytics tools for educators. Built with ASP.NET MVC and modern web technologies, the platform features role-based user interfaces, exam proctoring capabilities, and detailed performance analytics.

### Key Features

- **Student & Teacher Onboarding**: Seamless onboarding experience for new students & teachers
- **Test Management**: Create, manage, and take proctored exams with full-screen enforcement
- **Performance Analytics**: Real-time analytics and graphical performance insights
- **Role-Based UI**: Differentiated interfaces for students and teachers
- **Auto-Save**: Automatic progress saving during test-taking sessions
- **Result Analysis**: Comprehensive student result pages with subject-wise performance tracking
- **Teacher Analytics**: Advanced analytics dashboard for educators to monitor class performance
- **Global Error Logging**: Centralized error tracking and logging system

## Technology Stack

### Backend
- **C#** - Primary backend language
- **ASP.NET MVC 5** - Web framework
- **.NET Framework 4.8** - Runtime environment
- **SQL Server** - Database management system

### Frontend
- **JavaScript** - Client-side scripting
- **Razor HTML** - View templating engine
- **jQuery** - DOM manipulation and AJAX
- **Bootstrap 5** - Responsive UI framework
- **CSS3** - Styling and animations

### Database & Stored Procedures
- **SQL Server Database** - Data persistence layer
- **Stored Procedures** - Business logic automation
  - User Authentication (LogIn, SignUp)
  - Test Management (CreateNewTest, GetAllQuestionsByTestID)
  - Student Features (StudentOnboarding, GetTestsDetailsByStudentId)
  - Teacher Features (GetTeacherDashboardDetails, GetTestAnalytics)
  - Error Logging (LogError)
  - Test Analytics (GetTestAnalytics, SubmitTest)

## Project Structure

```
├── App_Start/
│   ├── BundleConfig.cs
│   ├── BundleConfig2.cs
│   ├── FilterConfig.cs
│   └── RouteConfig.cs
├── Content/
│   ├── bootstrap-grid.css
│   ├── bootstrap-grid.css.map
│   ├── bootstrap-grid.min.css
│   ├── bootstrap-grid.min.css.map
│   ├── bootstrap-grid.rtl.css
│   ├── bootstrap-grid.rtl.css.map
│   ├── bootstrap-grid.rtl.min.css
│   ├── bootstrap-grid.rtl.min.css.map
│   ├── bootstrap-reboot.css
│   ├── bootstrap-reboot.css.map
│   ├── bootstrap-reboot.min.css
│   ├── bootstrap-reboot.min.css.map
│   ├── bootstrap-reboot.rtl.css
│   ├── bootstrap-reboot.rtl.css.map
│   ├── bootstrap-reboot.rtl.min.css
│   ├── bootstrap-reboot.rtl.min.css.map
│   ├── bootstrap-utilities.css
│   ├── bootstrap-utilities.css.map
│   ├── bootstrap-utilities.min.css
│   ├── bootstrap-utilities.min.css.map
│   ├── bootstrap-utilities.rtl.css
│   ├── bootstrap-utilities.rtl.css.map
│   ├── bootstrap-utilities.rtl.min.css
│   ├── bootstrap-utilities.rtl.min.css.map
│   ├── bootstrap.css
│   ├── bootstrap.css.map
│   ├── bootstrap.min.css
│   ├── bootstrap.min.css.map
│   ├── bootstrap.rtl.css
│   ├── bootstrap.rtl.css.map
│   ├── bootstrap.rtl.min.css
│   ├── bootstrap.rtl.min.css.map
│   └── Site.css
├── Controllers/
│   ├── AuthController.cs
│   ├── ErrorController.cs
│   ├── HomeController.cs
│   ├── StudentController.cs
│   ├── TeacherController.cs
│   └── TestController.cs
├── Models/
│   ├── DapperConn.cs
│   ├── TeacherModel.cs
│   ├── TestModel.cs
│   └── UserModel.cs
├── Properties/
│   └── AssemblyInfo.cs
├── Scripts/
│   ├── auth.js
│   ├── bootstrap.bundle.js
│   ├── bootstrap.bundle.js.map
│   ├── bootstrap.bundle.min.js
│   ├── bootstrap.bundle.min.js.map
│   ├── bootstrap.esm.js
│   ├── bootstrap.esm.js.map
│   ├── bootstrap.esm.min.js
│   ├── bootstrap.esm.min.js.map
│   ├── bootstrap.js
│   ├── bootstrap.js.map
│   ├── bootstrap.min.js
│   ├── bootstrap.min.js.map
│   ├── jquery-3.7.0-vsdoc.js
│   ├── jquery-3.7.0.intellisense.js
│   ├── jquery-3.7.0.js
│   ├── jquery-3.7.0.min.js
│   ├── jquery-3.7.0.min.map
│   ├── jquery-3.7.0.slim.js
│   ├── jquery-3.7.0.slim.min.js
│   ├── jquery-3.7.0.slim.min.map
│   ├── jquery.validate-vsdoc.js
│   ├── jquery.validate.js
│   ├── jquery.validate.min.js
│   ├── jquery.validate.unobtrusive.js
│   ├── jquery.validate.unobtrusive.min.js
│   └── modernizr-2.8.3.js
├── SP/
│   ├── sp_AddTeacherSpecialization.txt
│   ├── sp_CreateNewTest.txt
│   ├── sp_GetAllQuestionsByTestID.txt
│   ├── sp_GetSubjectsAndClasses.txt
│   ├── sp_getTeacherDashboardDetails.txt
│   ├── sp_GetTeacherSpecialization.txt
│   ├── sp_GetTestAnalytics.txt
│   ├── sp_GetTestsDetailsByStudentId.txt
│   ├── sp_GetTestsDetailsByTeacherId.txt
│   ├── sp_GetUserByEmail.txt
│   ├── sp_LogError.txt
│   ├── sp_LogIn.txt
│   ├── sp_ReturnStartTestFailure.txt
│   ├── sp_SignUp.txt
│   ├── sp_StartTest.txt
│   ├── sp_StudentOnboarding.txt
│   ├── sp_SubmitTest.txt
│   ├── Tables-Schema.txt
│   └── ViewTestForStudent.txt
├── Views/
│   ├── Auth/
│   │   ├── LogIn.cshtml
│   │   └── SignUp.cshtml
│   ├── Error/
│   │   ├── Index.cshtml
│   │   └── NotFound.cshtml
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Shared/
│   │   ├── _Card.cshtml
│   │   ├── _Layout.cshtml
│   │   ├── _Navbar.cshtml
│   │   ├── _UnauthorizedAccess.cshtml
│   │   └── Error.cshtml
│   ├── Student/
│   │   ├── Index.cshtml
│   │   └── Onboard.cshtml
│   ├── Teacher/
│   │   ├── Analytics.cshtml
│   │   ├── Index.cshtml
│   │   └── Onboarding.cshtml
│   ├── Test/
│   │   ├── Create.cshtml
│   │   └── StartTest.cshtml
│   ├── _ViewStart.cshtml
│   └── Web.config
├── .gitignore
├── favicon.ico
├── Global.asax
├── Global.asax.cs
├── packages.config
├── PrepMaster.csproj
├── PrepMaster.sln
├── UpgradeLog.htm
├── UpgradeLog2.htm
├── Web.config
├── Web.Debug.config
└── Web.Release.config
```

## Collaborators

The PrepMaster project is built through active collaboration of three dedicated developers:

### Core Team

<div align="center">

| Contributor | Role | Contributions | Key Contributions |
|:---:|:---:|:---:|:---|
| [![SubhradeepBasu18](https://avatars.githubusercontent.com/u/111586851?s=50&v=4)](https://github.com/SubhradeepBasu18) | Frontend Developer | 33 | Test interface, Analytics graphs, Auto-save feature |
| [![Sayan0361](https://avatars.githubusercontent.com/u/122393497?s=50&v=4)](https://github.com/Sayan0361) | Backend Developer | 33 | Student onboarding, Result pages, Error handling, Global logging |
| [![soumyodipsays](https://avatars.githubusercontent.com/u/151426026?s=50&v=4)](https://github.com/soumyodipsays) | Project Lead | 20 | UI refinement, Exam proctoring, Feature integration, Navbar fixes |

</div>

**Detailed Breakdown:**

- **[@SubhradeepBasu18](https://github.com/SubhradeepBasu18)** - 33 commits
  - Frontend Development & UI Components
  - Test Taking Interface
  - Analytics Client Logic
  - Performance Visualization

- **[@Sayan0361](https://github.com/Sayan0361)** - 33 commits
  - Backend API Development
  - Student Onboarding System
  - Result Page Implementation
  - Global Error Logging Framework

- **[@soumyodipsays](https://github.com/soumyodipsays)** - 20 commits
  - Project Architecture & Leadership
  - Exam Proctoring Features
  - UI/UX Refinement
  - Feature Integration & Testing

## Getting Started

### Prerequisites

- Visual Studio 2019+ or Visual Studio Code
- .NET Framework 4.8
- SQL Server 2016+
- IIS 7.5+

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/soumyodipsays/PrepMaster.git
   cd PrepMaster
   ```

2. **Open in Visual Studio**
   - Open `PrepMaster.sln` in Visual Studio
   - Restore NuGet packages

3. **Configure Database**
   - Update connection strings in `Web.config`
   - Run SQL scripts from `SP/` folder to create stored procedures
   - Update tables schema using `SP/Tables-Schema.txt`

4. **Run the Application**
   - Press `F5` or click "Start" in Visual Studio
   - Application runs on `https://localhost`

## Architecture Overview

```
┌─────────────────────────────────────────────┐
│         Browser / Client Layer              │
│  (JavaScript, HTML, CSS, Razor Templates)   │
└──────────────┬──────────────────────────────┘
               │
┌──────────────▼──────────────────────────────┐
│      ASP.NET MVC 5 Controllers              │
│  (User, Test, Teacher, Student, Auth)       │
└──────────────┬──────────────────────────────┘
               │
┌──────────────▼──────────────────────────────┐
│     Business Logic & Services Layer         │
│  (Error Handling, Analytics, Proctoring)    │
└──────────────┬──────────────────────────────┘
               │
┌──────────────▼──────────────────────────────┐
│  SQL Server Database & Stored Procedures    │
│  (User Auth, Tests, Results, Analytics)     │
└─────────────────────────────────────────────┘
```

## API Endpoints

### Authentication
- `POST /Auth/SignUp` - Register new user
- `POST /Auth/LogIn` - User login

### Student
- `GET /Student/Index` - Student dashboard
- `GET /Student/Onboard` - Student onboarding
- `GET /Test/StartTest/{testId}` - Start test
- `POST /Test/SubmitTest` - Submit answers

### Teacher
- `GET /Teacher/Index` - Teacher analytics dashboard
- `GET /Teacher/Onboarding` - Teacher onboarding
- `POST /Test/Create` - Create new test

## Contributing

We welcome contributions from the community! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes with clear messages (`git commit -m 'Add: amazing feature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request with a detailed description

### Commit Message Format
- `Feature:` New functionality
- `Fix:` Bug fixes
- `Refactor:` Code improvements
- `Docs:` Documentation updates
- `Enhancement:` Feature enhancements
