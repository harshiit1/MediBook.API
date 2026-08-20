# MediBook.API

> **Multi-tenant SaaS hospital management backend** 

## What is MediBook?

MediBook is a **production-grade, multi-tenant hospital management API** that powers scheduling, patient management, lab results, pharmacy orders, and real-time notifications across isolated hospital tenants — all from a single deployed backend.

Built to handle the correctness requirements of healthcare data: every query is tenant-scoped, every auth flow is claim-guarded, and every sensitive operation goes through stored procedures with explicit TRY/CATCH error boundaries.

---

## Architecture

```
┌─────────────────────────────────────────────────────────┐
│                     Medicare.API                        │
│   Controllers · Middleware · JWT Auth · SignalR Hub     │
│              · Hangfire Dashboard                       │
└───────────────────────┬─────────────────────────────────┘
                        │ MediatR (CQRS)
┌───────────────────────▼─────────────────────────────────┐
│                 Medicare.Application                    │
│   Commands · Queries · Handlers · DTOs · Interfaces    │
└───────────────────────┬─────────────────────────────────┘
                        │ Repository Pattern
┌───────────────────────▼─────────────────────────────────┐
│                   Medicare.DAL                          │
│       Dapper · SQL Server · Stored Procedures          │
└───────────────────────┬─────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────┐
│                  Medicare.Domain                        │
│    Entities · Enums · Exceptions · Value Objects       │
│              (zero external dependencies)              │
└─────────────────────────────────────────────────────────┘
```

**Layer responsibilities:**

| Layer | Responsibility |
|---|---|
| `Medicare.API` | HTTP entry point — routing, middleware pipeline, JWT validation, SignalR hub registration, Hangfire background job scheduler |
| `Medicare.Application` | All business logic — CQRS command/query handlers via MediatR, DTOs, service interfaces |
| `Medicare.DAL` | Data persistence — Dapper ORM, SQL Server stored procedures, repository implementations |
| `Medicare.Domain` | Core domain — entities, enums, domain exceptions; no external dependencies |

---

## Key Design Decisions

### 1. Multi-Tenant Isolation via `TenantId`
Every database query is scoped to a `TenantId` derived from JWT claims — never from client-supplied request parameters. Tenants are registered in `HospitalMaster`; all schema tables carry a `TenantId` foreign key enforced at the stored procedure level.

**Why:** Prevents cross-tenant data leakage at the query layer, not just the application layer. Even a misconfigured handler cannot leak another tenant's data if the stored procedure enforces the scope.

### 2. Three-Table Patient Identity Model
```
PatientAccount       → login credentials (hashed password, refresh token)
PatientLoginMaster   → session and OTP state
PatientProfile       → medical identity (demographics, history)
```
Unified with a `Users` table that serves both `Patient` and `Associate` user types via a `UserType` discriminator — single login endpoint, no duplicated auth logic.

**Why:** Separates authentication state from medical identity. A patient's profile can be updated without touching credentials, and credential resets don't risk corrupting clinical data.

### 3. Claims-Based RBAC Over Role Strings
Roles and tenant context are embedded in JWT claims at token generation. Associate registration is validated against `TenantId` from the token, not from the request body.

**Why:** Closes IDOR vulnerabilities — a malicious client cannot register an associate under a different tenant by supplying a forged `TenantId` in the payload.

### 4. Purpose-Claim-Guarded OTP Flows
Password reset tokens carry a `purpose` claim (`password-reset`). The reset endpoint validates both the token signature **and** the purpose claim before allowing a credential update.

**Why:** Prevents token reuse attacks — a valid access token cannot be replayed against the password reset endpoint.

### 5. Stored Procedures for All Write Operations
All mutations (appointments, lab results, pharmacy orders, user registration) go through SQL Server stored procedures with `BEGIN TRY / CATCH` and explicit response models.

**Why:** Atomic operations with database-level error handling. Rollback logic lives in the database, not scattered across application code.

---

## Features

### Authentication & Security
- JWT access + refresh token flow (separate `AssociateToken` / `PatientToken` tables)
- BCrypt password hashing
- Purpose-claim-guarded OTP password reset for both Patient and Associate
- Tenant-scoped claims — no client-supplied `TenantId` accepted on write operations
- IDOR prevention enforced at stored procedure level

### Scheduling & Appointments
- Slot generation and availability management
- Appointment lifecycle (book → confirm → complete → cancel)
- Hangfire background jobs for confirmation emails and scheduled reminders
- Automatic slot release on cancellation/timeout

### Patient Management
- Three-table patient model (Account / LoginMaster / Profile)
- Unified `Users` table supporting Patient and Associate login
- GUID primary keys across all identity tables

### Lab Results
- Lab profile and test result management with `ProfileId` architecture
- Result upload and retrieval by patient/tenant scope

### Pharmacy & Prescriptions
- Prescription order model with multi-drug line items
- Appointment linking for prescription traceability

### Real-Time Notifications
- SignalR hub for push notifications to connected clients
- `NotificationMaster` persistence layer — notifications survive client reconnects
- JWT claim alignment for hub authentication

---

## Tech Stack

| Category | Technology |
|---|---|
| Runtime | .NET 8, ASP.NET Core |
| Architecture | Clean Architecture, CQRS via MediatR |
| ORM | Dapper |
| Database | SQL Server (stored procedures, TRY/CATCH) |
| Auth | JWT Bearer, BCrypt.Net |
| Real-Time | SignalR |
| Background Jobs | Hangfire |
| Containerisation | Docker, Docker Compose |
| CI/CD | Jenkins (Git-triggered pipelines) |

---

## Project Structure

```
MediBook.API/
├── Medicare.API/               # Entry point
│   ├── Controllers/            # BaseApiController + domain controllers
│   ├── Middleware/             # Exception handling, tenant resolution
│   ├── Hubs/                   # SignalR NotificationHub
│   └── Program.cs              # DI, middleware pipeline, Hangfire registration
│
├── Medicare.Application/       # Business logic
│   ├── Commands/               # Write operations (CQRS)
│   ├── Queries/                # Read operations (CQRS)
│   ├── Handlers/               # MediatR handlers
│   ├── DTOs/                   # Request/response models
│   └── Interfaces/             # Repository and service contracts
│
├── Medicare.DAL/               # Data access
│   ├── Repositories/           # Dapper repository implementations
│   └── StoredProcedures/       # SQL SP references
│
└── Medicare.Domain/            # Core domain (no external deps)
    ├── Entities/
    ├── Enums/
    └── Exceptions/
```

---

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (local or remote)
- Docker (optional, for containerised run)

### Local Setup

```bash
# Clone the repository
git clone https://github.com/harshiit1/MediBook.API.git
cd MediBook.API
```

1. Open `Medicare.API.slnx` in Visual Studio or Rider.
2. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MediBookDB;Trusted_Connection=True;"
  }
}
```
3. Run the database scripts from `/Medicare.DAL/Scripts/` against your SQL Server instance.
4. Set `Medicare.API` as the startup project and press `F5`.
5. Swagger UI will open at `https://localhost:{port}/swagger`.

### Docker

```bash
docker-compose up --build
```

---

| Module | Base Route |
|---|---|
| Auth (Patient) | `/api/patient/auth` |
| Auth (Associate) | `/api/associate/auth` |
| Appointments | `/api/appointments` |
| Lab Results | `/api/lab` |
| Pharmacy | `/api/pharmacy` |
| Notifications | `/api/notifications` |
| Hangfire Dashboard | `/hangfire` |

---
## License

MIT — see [LICENSE](LICENSE) for details.
