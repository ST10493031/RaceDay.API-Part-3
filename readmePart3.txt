# RaceDay Event Management System - Part 3

**Author:** Seaney Maseko  
**Module:** Programming 2B (PROG6212)  
**Project:** Part 3 - MVC Web Application, Azure Blob Storage & Docker  

## Project Overview
RaceDay is a full-stack event management system for the South African running, walking, and cycling community. This final part of the POE introduces the **ASP.NET Core MVC web application** that consumes the Part 2 RESTful API. 

The application features role-based navigation, session management, cloud image storage via **Azure Blob Storage**, and is fully containerized using **Docker**.

## Architecture
This solution consists of three main projects:
1. **`RaceDay.API`**: The backend REST API and database (Entity Framework Core).
2. **`RaceDay.API.Tests`**: xUnit tests for API authentication and business logic.
3. **`RaceDay.Web`**: The frontend MVC application that consumes the API via `HttpClient`.

---

## Key Features (Part 3)

### Organiser Interface
- Dashboard showing a summary of created events.
- Create new events (including uploading a banner image to Azure Blob Storage).
- Add and manage event categories (distance, age, entry fee).
- View participant enrolments for their events.
- Capture and publish race results.

### Participant Interface
- Browse upcoming events.
- View event details and available categories.
- Enrol in events (assigns a unique race number).
- View personal enrolments and statuses.
- View the public results board with finish times and positions.

---

## 🐳 Running with Docker

The MVC web application has been fully containerized. To build and run the application using Docker, ensure **Docker Desktop** is running on your machine, then execute the following commands in the root directory of the solution:

**1. Build the Docker image:**
```bash
docker build -t raceday-web -f RaceDay.Web/Dockerfile .s