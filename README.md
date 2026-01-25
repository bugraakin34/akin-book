# AkinBook 📚

AkinBook is a bookstore-style REST API built with **.NET (ASP.NET Core)** and **PostgreSQL**, with a **React** frontend planned.
This project focuses on clean, real-world backend features: **JWT authentication**, **role-based authorization**, **pagination/search**, **validation**, and **standardized error responses**.

## Tech Stack
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core + PostgreSQL (Npgsql)
- JWT Authentication
- FluentValidation
- Docker + Docker Compose

## Features
- ✅ Auth: Register / Login (JWT)
- ✅ Protected endpoint: `/api/auth/me`
- ✅ Role-based authorization (Admin-only book create/update/delete)
- ✅ Books CRUD (Admin manages books)
- ✅ List books with **pagination + search**
- ✅ FluentValidation for book create/update
- ✅ Standard error response format (validation + exceptions)

---

## Getting Started

### 1) Run with Docker
From repository root:

```bash
docker compose up --build -d
