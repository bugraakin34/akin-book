# 📚 AkinBook

AkinBook is a full-stack bookstore-style application built with **.NET (ASP.NET Core)** and **PostgreSQL**, featuring a **React frontend**.

This project focuses on real-world backend practices such as **JWT authentication**, **role-based authorization**, **pagination**, **search**, **validation**, and **multi-language (i18n) support**.

---

## 🚀 Tech Stack

### Backend
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- PostgreSQL (Npgsql)
- JWT Authentication
- FluentValidation

### Frontend
- React + TypeScript
- Ant Design
- i18next (multi-language support)

### DevOps
- Docker + Docker Compose

---

## ✨ Features

### 🔐 Authentication & Authorization
- Register / Login with JWT
- Protected endpoint: `/api/auth/me`
- Role-based authorization (Admin / User)

### 📚 Books Management
- Full CRUD (Admin only for create/update/delete)
- Pagination & search support
- Multi-language fields:
  - Title (TR / EN)
  - Description (TR / EN)

### 🌍 Internationalization (i18n)
- UI language switching (TR / EN)
- Dynamic content rendering based on selected language

### ⚙️ System Features
- FluentValidation for input validation
- Standardized error response format
- Axios interceptor for automatic logout on token expiration

---

## 🐳 Getting Started

### 🔧 Requirements

Make sure you have installed:

- Docker
- Docker Compose
- Node.js (v18+)
- .NET SDK (10)

---

### 1️⃣ Clone the repository

```bash
git clone https://github.com/your-username/akin-book.git
cd akin-book