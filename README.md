# Uptime Monitor REST API

A full-stack, asynchronous website monitoring application. The backend runs a continuous background worker to ping registered websites in parallel, while the REST API serves real-time status data to a lightweight vanilla JavaScript dashboard.

Tech Stack

 Category & Technologies 

Backend C#, ASP.NET Core 8.0, Web API 
Database MS SQL Server, Entity Framework Core 
Infrastructure Docker, Docker Compose 
Frontend HTML5, CSS3, Vanilla JavaScript (Fetch API) 

 Key Features

Asynchronous Background Processing: Uses `IHostedService` to run a continuous, non-blocking background loop.
Parallel Execution: Implements `Task.WhenAll` to ping multiple websites concurrently, ensuring optimal performance and no thread blocking.
Containerized Database: MS SQL Server runs locally via Docker Compose for a clean, reproducible development environment.
Automated Migrations: The application automatically creates the database and required tables upon startup using EF Core's `EnsureCreatedAsync()`.
CORS Configuration: Fully configured Cross-Origin Resource Sharing to seamlessly serve the frontend dashboard.
Responsive Dashboard: A clean, dependency-free UI that polls the API and updates website statuses in real-time.
