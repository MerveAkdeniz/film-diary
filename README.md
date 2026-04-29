# 🎬 Film Diary API

![ASP.NET Core Web API](https://img.shields.io/badge/ASP.NET_Core-blueviolet?style=for-the-badge&logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-green?style=for-the-badge&logo=nuget)
![SQL Server](https://img.shields.io/badge/SQL_Server-red?style=for-the-badge&logo=microsoft-sql-server)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger)

> A robust movie discovery backend system providing advanced filtering, actor-based recommendations, and spoiler-free community engagement.

## 🚀 The Problem

Movie lovers often struggle to decide what to watch next due to overwhelming options, fragmented data, and a lack of structured filtering capabilities on many existing platforms.

## 💡 The Solution

Film Diary API is a comprehensive backend service engineered to solve these problems. It empowers users to:
* **Discover movies** intuitively through deep metadata like actors and genres.
* **Filter content** efficiently using multiple complex parameters simultaneously.
* **Engage with the community** safely without the fear of spoilers, thanks to built-in spoiler detection parsing.
* **Receive personalized recommendations** driven by their unique viewing habits and favorite genres.

---

## 🌟 Core Features & Technical Highlights

### 🔐 Authentication & Authorization
* Secure JWT-based authentication system.
* User registration and login endpoints.
* Protected routes using role-based authorization.
* Ensures user-specific data isolation (e.g., favorites per user).

### 🎭 Actor-Based Discovery
Moving beyond standard title and genre searches, the API allows users to:
* Fetch the entire filmography of a specific actor.
* **Intersection Search**: Find specific movies where **two selected actors appear together**.

### 🔍 Advanced Search & Filtering
A highly flexible search endpoint designed to handle complex queries:
* Filter by genre, rating, release status, or user favorites.
* Combine multiple query parameters dynamically in a single API request for granular results.

### ⭐ User-Based Favorite System
* Each user maintains an independent favorite list.
* Favorites are securely linked to authenticated users via JWT identity mapping.
* Forms the backbone of the personalized recommendation engine.

### 💬 Intelligent Comment System
* Add structured comments to movies.
* **Automatic Spoiler Management**: The system segregates comments into spoiler / non-spoiler categories, protecting the user experience during content browsing.

### 🧠 Recommendation Engine
* Delivers dynamic movie suggestions based on the user's aggregated favorite genres.
* **Explainable AI approach**: Provides transparency on *why* a specific movie is being recommended.

### 📦 Bulk Data Processing (CSV)
* Efficiently supports importing large movie datasets via CSV using the `CsvHelper` library.

---

## ⚙️ Tech Stack & Architecture

Built with modern backend development best practices, ensuring scalability, maintainability, and top-tier performance.

* **Framework**: ASP.NET Core Web API
* **Authentication**: JWT (JSON Web Token)
* **ORM & Database**: Entity Framework Core (Code-First Approach), Microsoft SQL Server
* **Data Parsing**: `CsvHelper` for bulk data ingestion and processing
* **Documentation**: Swagger / OpenAPI integration for interactive API testing
* **Architecture Principles**: DTO (Data Transfer Object) implementations, strict validation rules, and RESTful design principles.

### 🧱 Architecture Overview
* Layered architecture design: Controllers → Services → Data (DbContext)
* Separation of concerns ensures maintainability and scalability.

---

## 📌 Example Endpoints Overview

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET`  | `/api/Films/advanced-search` | Multi-parameter search and filtering |
| `GET`  | `/api/Films/by-two-actors` | Identifies common films for a given pair of actors |
| `GET`  | `/api/Films/recommendations`| Retrieves tailored movie suggestions |
| `GET`  | `/api/Comments/film/{id}`   | Fetches spoiler-managed comments for a specific movie |
| `POST` | `/api/Films/import`         | Bulk processes movie data from CSV records |

### 📷 API Documentation
* Swagger UI available for interactive testing.
* *(Deployed version may be temporarily unavailable due to free-tier cold start on Render)*

---

## 🛠️ Local Setup & Installation

Follow these steps to run the project locally on your machine:

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (Compatible version)
* SQL Server (LocalDB or full instance)

### Installation Guide

1. **Clone the repository:**
   ```bash
   git clone https://github.com/MerveAkdeniz/film-diary.git
   cd film-diary
   ```

2. **Configure the Database:**
   Navigate to the backend project and update the `DefaultConnection` string in `appsettings.Development.json` to point to your local SQL Server instance.

3. **Apply EF Core Migrations:**
   From your terminal within the API project directory, run:
   ```bash
   dotnet ef database update
   ```

4. **Launch the Application:**
   ```bash
   dotnet run
   ```

5. **Explore the API:**
   Navigate to `https://localhost:<port>/swagger` in your browser to access the Swagger UI and test the endpoints interactively.

---

## 🎯 Impact & Business Value

This system significantly enhances the content discovery lifecycle. By combining **actor-based tracking**, **tailored algorithmic recommendations**, and **moderated community feedback**, it creates a highly engaging and personalized platform infrastructure, ready for integration with any modern front-end framework.

---

## 📡 Project Status

**Backend feature-complete (v1)**. The API is thoroughly tested via Swagger and fully ready for frontend UI consumption.

---

## 💼 Resume Highlight
*Designed and implemented a scalable multi-user backend system with JWT-based authentication, user-specific data isolation, and dynamic recommendation logic.*

---
*Developed by [Merve AKDENİZ](https://github.com/MerveAkdeniz)*
