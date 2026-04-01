<div align="center">

# ⚖️ Quantity Measurement App

### A full-stack enterprise-grade application built progressively over 21 use cases

[![Live Demo](https://img.shields.io/badge/🚀_Live_Demo-qmabydevmalu.vercel.app-6C3CE1?style=for-the-badge)](https://qmabydevmalu.vercel.app)
[![Backend](https://img.shields.io/badge/Backend-.NET_10_API-512BD4?style=for-the-badge&logo=dotnet)](https://monsterasp.net)
[![Frontend](https://img.shields.io/badge/Frontend-Angular_17-DD0031?style=for-the-badge&logo=angular)](https://qmabydevmalu.vercel.app)

*Built from scratch · March 2026*

</div>

---

## 📌 About

The Quantity Measurement App is a production-deployed, full-stack application that performs unit conversions and arithmetic operations across **Length, Weight, Volume, and Temperature**. Built progressively through 21 use cases over two months, the project covers the complete software development lifecycle — from a simple console app to a microservices architecture with a deployed Angular frontend.

---

## 🌐 Live Application

| Layer | URL | Platform |
|-------|-----|----------|
| **Frontend** | [qmabydevmalu.vercel.app](https://qmabydevmalu.vercel.app) | Vercel |
| **Backend API** | MonsterASP.NET | MonsterASP |

---

## 🗂️ Use Case Timeline

### 📅 February 2026

---

#### UC1 — UC6 · Console App Foundation
`Feb 27 – March 1, 2026`

> Built the core domain model and business logic from scratch.

- Modelled `QuantityDTO` and measurement enums — `LengthUnit`, `WeightUnit`, `VolumeUnit`, `TemperatureUnit`
- Implemented unit conversion logic using **extension methods** on enums
- Built arithmetic operations: **Add, Subtract, Divide, Compare, Convert**
- Implemented custom exception — `UnsupportedOperationException` for temperature arithmetic
- Built an interactive **console menu** for user input
- Applied **N-Tier Architecture** from the start: ModelLayer → BusinessLayer → ConsoleApp

**🧠 Learned:** Extension methods, enum design, exception handling, N-tier separation of concerns

---

#### UC7 — UC10 · Unit Testing
`Feb 1 – Feb 5, 2026`

> Added NUnit test coverage for all operations and edge cases.

- Set up `QuantityMeasurementApp.Tests` project with **NUnit**
- Wrote test cases for all length, weight, volume, and temperature operations
- Covered edge cases: division by zero, unsupported units, cross-category operations
- Applied **TDD principles** — test first, implement second

**🧠 Learned:** NUnit, test structure, assertion patterns, TDD mindset

---

#### UC11 — UC14 · LINQ & EF Core Concepts
`March 5 – March 10, 2026`

> Deep dive into data querying and ORM patterns.

- Studied **LINQ** operators: `Where`, `Select`, `GroupBy`, `OrderBy`, `Join`, `Aggregate`
- Understood `IQueryable` vs `IEnumerable` — deferred vs immediate execution
- Explored **Entity Framework Core** — DbContext, DbSet, migrations, relationships
- Studied loading strategies: Eager (`Include`), Lazy, Explicit
- Understood `SaveChanges()`, change tracking, and transaction handling

**🧠 Learned:** LINQ, EF Core, migrations, ORM vs raw SQL, loading strategies

---

#### UC15 · N-Tier Architecture — Clean Rebuild
`March 10 – March 15, 2026`

> Restructured the entire solution into a clean multi-project N-tier architecture.

- Split into separate .NET projects: `ModelLayer`, `RepositoryLayer`, `BusinessLayer`, `ConsoleApp`
- Implemented `IQuantityMeasurementRepository` and `IQuantityMeasurementService` interfaces
- Applied **Dependency Inversion Principle** throughout
- Wired dependencies using `IServiceCollection` with constructor injection
```
ModelLayer → RepositoryLayer → BusinessLayer → ConsoleApp
```

**🧠 Learned:** Multi-project solutions, interface-driven design, DI containers, clean architecture

---

#### UC16 · ADO.NET — SQL Server Integration
`March 15 – March 20, 2026`

> Added persistent SQL Server storage using raw ADO.NET.

- Connected to **SQL Server** using `Microsoft.Data.SqlClient`
- Created `QuantityMeasurementSqlRepository` with parameterized queries
- Implemented `INSERT` and `SELECT` manually with `SqlConnection`, `SqlCommand`, `SqlDataReader`
- Registered connection string via `IConfiguration` and `appsettings.json`
- Added **dual storage**: in-memory cache AND SQL on every operation

**🧠 Learned:** ADO.NET, parameterized queries, SQL injection prevention, IConfiguration

---

### 📅 March 2026

---

#### UC17 · ASP.NET Core Web API + Redis + EF Core ORM
`March 20 – March 23, 2026`

> Transformed the app into a REST API with caching and ORM.

- Created `QuantityMeasurementAPI` — ASP.NET Core Web API project
- Built controllers with `[ApiController]`, `[Route]`, `[HttpPost]`, `[HttpGet]`
- Integrated **Redis** via `IDistributedCache` — implemented **Cache-Aside pattern**:
  - `Save()` invalidates the cache
  - `GetAll()` checks Redis first, falls back to DB on cache miss
- Replaced `QuantityMeasurementCacheRepository` (List-based) with `QuantityMeasurementRedisRepository`
- Added **Entity Framework Core** — `QuantityMeasurementDbContext`, migrations, `ToList()` LINQ queries
- Configured **Swagger UI** with JWT support
- Registered all services in `Program.cs` with scoped lifetimes

**Endpoints:**
```
POST /api/quantity/add
POST /api/quantity/subtract
POST /api/quantity/divide
POST /api/quantity/compare
POST /api/quantity/convert
GET  /api/quantity/history/redis
GET  /api/quantity/history/ef
GET  /api/quantity/history/sql
```

**🧠 Learned:** ASP.NET Core Web API, middleware pipeline, DI lifetimes, Redis Cache-Aside, EF Core ORM, Swagger

---

#### UC18 · JWT Authentication + ASP.NET Identity
`March 23 – March 25, 2026`

> Secured the API with full user authentication.

- Integrated **ASP.NET Core Identity** — `IdentityDbContext`, `UserManager`, `IdentityUser`
- Implemented `AuthService` with `RegisterAsync()` and `LoginAsync()`
- Password hashing using **PBKDF2** (automatic via Identity)
- JWT token generation with **HMAC-SHA256** signature
  - Claims: `NameIdentifier`, `Email`, `Jti`, `Sub`
- Configured `AddAuthentication().AddJwtBearer()` with token validation parameters
- Protected all quantity endpoints with `[Authorize]`
- Auth endpoints (`/api/auth/register`, `/api/auth/login`) remain public
- Configured Swagger with **Bearer token** authorization button

**Identity Tables Created:**
| Table | Purpose |
|-------|---------|
| `AspNetUsers` | Registered users with hashed passwords |
| `AspNetRoles` | Role definitions |
| `AspNetUserRoles` | User-role mappings |
| `AspNetUserClaims` | Per-user custom claims |
| `AspNetUserTokens` | Password reset / 2FA tokens |

**🧠 Learned:** JWT, HMAC-SHA256, PBKDF2, ASP.NET Identity, OAuth2 concepts, token validation

---

#### UC19 · JS Frontend
`March 25 – March 26, 2026`

> Built a browser-based frontend using pure HTML, CSS, and JavaScript.

- Semantic **HTML5** structure — `<header>`, `<main>`, `<section>`, `<article>`, `<footer>`
- **CSS Grid** for page layout and operation cards
- **Flexbox** for header, history, and button groups
- **Float** for tab strip layout
- Pseudo-classes: `:hover`, `:focus`, `:active`, `:nth-child`, `::placeholder`
- **Responsive design** with `@media (max-width: 700px)`
- ES6 `ApiClient` class with **private fields** (`#token`)
- **async/await** for all API calls — `fetch()` AJAX
- **Callback pattern** for history loading
- `Object.freeze()` for unit group constants (ES9)
- **DOM manipulation**, event handling, dynamic UI rendering
- JWT token stored in memory (not localStorage)
- Enabled **CORS** in the backend for cross-origin requests

**🧠 Learned:** HTML5, CSS3, Flexbox, Grid, Vanilla JS, AJAX, async/await, Promises, Callbacks, DOM, ES6+ features

---

#### UC20 · Angular Frontend
`March 26 – March 28, 2026`

> Rebuilt the frontend as a full Angular 17 single-page application.

- **Standalone components** — `AuthComponent`, `DashboardComponent`
- **Angular Router** with lazy loading and route guards
  - `AuthGuard` — redirects to `/auth` if not logged in
  - `GuestGuard` — redirects to `/dashboard` if already logged in
- **AuthService** with `BehaviorSubject` for reactive auth state
- **QuantityService** — all API calls via `HttpClient`
- **JWT Interceptor** — auto-attaches `Bearer` token to every request
- **Template-driven forms** with `NgModel`, validation, and error messages
- **Conditional rendering** with `*ngIf`, dynamic class binding with `[class.active]`
- **`*ngFor`** with `keyvalue` pipe for unit dropdowns
- **SCSS** — global variables, `@keyframes` animations, responsive breakpoints
- `ngOnInit` lifecycle hook for initial data load
- Session management via `sessionStorage`

**🧠 Learned:** Angular components, routing, guards, interceptors, HttpClient, BehaviorSubject, SCSS, lifecycle hooks, lazy loading

---

#### UC21 · Microservices + Ocelot API Gateway
`March 28 - March 30, 2026`

> Split the monolith into independent microservices behind an API Gateway.

**Architecture:**
```
Angular (4200)
     │
     ▼
QMAGateway — Ocelot (5173)
  ┌──────┬──────┐
  ▼      ▼      ▼
Auth  Operation History
(6001)  (6002)  (6003)
  │       │       │
 DB     DB+Redis DB+Redis
```

- **AuthService** (port 6001) — register, login, JWT generation
- **OperationService** (port 6002) — arithmetic + conversion + saves to DB + Redis invalidation
- **HistoryService** (port 6003) — Redis Cache-Aside reads + DB fallback
- **QMAGateway** — Ocelot routes all 11 endpoints to the correct service
- Each service independently validates JWT using the same key/issuer/audience
- `ocelot.json` defines upstream → downstream route mappings
- Angular frontend completely unchanged — still calls port 5173

**🧠 Learned:** Microservices architecture, Ocelot API Gateway, service isolation, independent deployment, fault isolation


#### Deployment
`March 31, 2026`

> Deployed both frontend and backend to production.

- **Frontend** deployed to **Vercel** — automatic deploy on GitHub push
  - `vercel.json` rewrite rule for Angular SPA routing
  - `environment.prod.ts` with production API URL
- **Backend** deployed to **MonsterASP.NET**
  - `appsettings.Production.json` with production connection string
  - Redis replaced with in-memory fallback (`AddDistributedMemoryCache`)
  - `db.Database.Migrate()` for automatic table creation on first boot
  - `web.config` configured for IIS with `ASPNETCORE_ENVIRONMENT=Production`

**🧠 Learned:** Production deployment, environment configuration, IIS hosting, Vercel SPA deployment, CI/CD basics

---

## 🏗️ Architecture
```
┌─────────────────────────────────────────┐
│           Angular Frontend              │
│         qmabydevmalu.vercel.app         │
└──────────────────┬──────────────────────┘
                   │ HTTPS
┌──────────────────▼──────────────────────┐
│         MonsterASP.NET Backend          │
│  ┌─────────────────────────────────┐   │
│  │  ASP.NET Core Web API (UC18)    │   │
│  │  • JWT Authentication           │   │
│  │  • Redis Cache-Aside            │   │
│  │  • Entity Framework Core        │   │
│  │  • Swagger UI                   │   │
│  └────────────┬────────────────────┘   │
│               │                        │
│  ┌────────────▼──────────┐             │
│  │     SQL Server        │             │
│  │  QuantityMeasurements │             │
│  │  AspNetUsers (Identity│             │
│  └───────────────────────┘             │
└─────────────────────────────────────────┘
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Angular 17, TypeScript, SCSS |
| Backend | ASP.NET Core Web API, .NET 10 |
| Auth | ASP.NET Identity, JWT (HMAC-SHA256) |
| ORM | Entity Framework Core 10 |
| Cache | Redis (Cache-Aside pattern) |
| Database | SQL Server, MSSQL |
| Gateway | Ocelot API Gateway |
| Testing | NUnit |
| Hosting | Vercel (frontend), MonsterASP (backend) |

---

## 📁 Project Structure
```
QuantityMeasurementApp/
├── QuantityMeasurementModelLayer/       # DTOs, Entities, Enums, Extensions
├── QuantityMeasurementRepositoryLayer/  # EF DbContext, Redis & SQL Repos
├── QuantityMeasurementBusinessLayer/    # Service interfaces & implementations
├── QuantityMeasurementAPI/              # ASP.NET Core Web API (deployed)
├── QuantityMeasurementConsoleApp/       # Terminal UI
└── QuantityMeasurementApp.Tests/        # NUnit test suite

QuantityMeasurementAngular/              # Angular 17 frontend (deployed)
├── src/app/
│   ├── components/auth/
│   ├── components/dashboard/
│   ├── services/
│   ├── guards/
│   └── interceptors/

QMAMicroservices/                        # UC21 microservices solution
├── AuthService/        (port 6001)
├── OperationService/   (port 6002)
├── HistoryService/     (port 6003)
└── QMAGateway/         (port 5173)
```

---

## 🚀 Getting Started (Local)

### Backend
```bash
# Clone the backend
git clone https://github.com/yourusername/QuantityMeasurementApp.git
cd QuantityMeasurementApp

# Start Redis
docker run -d -p 6379:6379 --name redis redis

# Run migrations
dotnet ef database update --project QuantityMeasurementRepositoryLayer --startup-project QuantityMeasurementAPI

# Run the API
dotnet run --project QuantityMeasurementAPI
# API available at http://localhost:5173
# Swagger at http://localhost:5173/swagger
```

### Frontend
```bash
git clone https://github.com/yourusername/QuantityMeasurementAngular.git
cd QuantityMeasurementAngular
npm install
ng serve
# Open http://localhost:4200
```

---

## 📄 License

This project was built as part of a structured learning curriculum. All code is original.

---

<div align="center">

**Built with 💜 over a month · March 2026**

[![Live Demo](https://img.shields.io/badge/Try_It_Live-qmabydevmalu.vercel.app-6C3CE1?style=for-the-badge)](https://qmabydevmalu.vercel.app)

</div>
