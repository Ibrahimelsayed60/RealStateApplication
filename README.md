# VillaEase - Booking & Management System

## Table of Contents
- [Overview](#overview)
- [Architecture](#architecture)
- [Modules](#modules)
- [Features](#features)
- [Technologies](#technologies)
- [Setup and Installation](#setup-and-installation)
- [Running the Application](#running-the-application)

## Overview
The Villa Booking & Management System is a full-stack ASP.NET Core MVC application designed to facilitate villa and property booking, user authentication, and online payments. This project provides a seamless experience for users to browse available properties, make bookings, and process payments, while enabling admins to manage listings, amenities, and reservations efficiently.

## Architecture
 The Villa Booking & Management System follows a multi-layered architecture based on Clean Architecture and Domain-Driven Design (DDD) principles. This structure ensures scalability, maintainability, and separation of concerns, making the system modular and easy to extend.
 - **Presentation Layer (UI & API)**: 
    - **Technology: ASP.NET Core MVC** (Razor Views) + jQuery + Bootstrap.
    - Handles user interactions, authentication, and API requests.
    - Uses Controllers and Views to manage HTTP requests and responses
 - **Application Layer (Business Logic & Services)**:
    - Contains the business logic, use cases, and application services.
    - Uses Service classes to handle complex operations like bookings and payments.
    - Implements DTOs (Data Transfer Objects) to pass data efficiently between layers.
 - **Domain Layer (Core Business Models & Rules)**:
    - Defines entities, aggregates, and domain logic (e.g., Villa, Booking, User).
    - Contains interfaces for repositories, ensuring loose coupling.
    - Implements validation rules and domain events where applicable.
 - **Infrastructure Layer (Data Access & External Services)**:
    - Uses Entity Framework Core (EF Core) for database interactions.
    - Implements repository pattern for data persistence (SQL Server).
    - Handles Stripe API integration for payments.
    - Manages JWT authentication & authorization logic.

## Modules
 - **Authentication & Authorization Module**:
    - Implements JWT-based authentication for secure user login.
    - Role-based access control (RBAC) to differentiate Admin and User permissions.
    - Secure token management for API requests.
 - **Property & Villa Management Module**:
    - Admins can add, update, and delete villas or properties.
    - Users can browse properties with real-time availability status.
    - Properties include details like images, pricing, location, and amenities.
 - **Booking & Reservation Module**:
    - Users can check availability and book a villa.
    - Admins can view, approve, or cancel bookings.
    - Booking details include dates, user info, property info, and payment status.
 - **Payment Module (Stripe Integration)**:
    - Users can pay for bookings via Stripe API.
    - Secure credit card processing with real-time transaction tracking.
    - Payment status updates after successful transactions.
 - **Admin Dashboard Module**:
    - Provides an intuitive UI for managing users, bookings, properties, and payments.
    - Displays analytics and reports on booking trends, revenue, and users.
 - **Search & Filtering Module**:
    - Allows users to search villas by location, price, availability, and amenities.
    - Optimized for quick query execution with EF Core & LINQ.
 - **Infrastructure & Security Module**:
    - Uses Clean Architecture (DDD) for scalable code organization.
    - Implements multi-database handling for authentication & application data.
    - Data encryption and secure API endpoints to protect sensitive information.

## Features
- **User Authentication & Security**: Implements JWT-based authentication with role-based access control (Admin, User).

- **Property Listings & Booking System**: Users can search, filter, and book villas with real-time availability.
- **Payment Integration**: Secure online transactions using Stripe API for hassle-free payments.

- **Admin Dashboard**: Admins can manage users, bookings, payments, and property details.

- **Multi-Database Handling**: Separate databases for authentication (Identity) and application data.

- **Optimized Performance**: Uses EF Core with LINQ queries for efficient database operations.

- **Clean Architecture & DDD**: Follows Domain-Driven Design (DDD) for scalability and maintainability.

## Technologies
 - **ASP.NET Core MVC** for backend development
 - **Entity Framework Core (Code-First Approach)** for database management
 - **JWT Authentication & Role-Based Authorization** for security
 - **SQL Server** as the primary database
 - **Stripe API** for online payment processing
 - **Bootstrap, jQuery, and AJAX** for frontend interactivity
 - **Git & GitHub** for version control and collaboration

## Setup and Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Ibrahimelsayed60/RealStateApplication.git
   cd RealStateApplication
2. Restore the .NET dependencies:
    ```bash
    dotnet restore
3. Configure the database connection string in appsettings.json:
    ```bash
        "ConnectionStrings": {
            "DefaultConnection": "Server = .; Database = RealState.villa; Trusted_Connection = true; encrypt = false; MultipleActiveResultSets = true",
            "IdentityConnection": "Server = .; Database = RealState.villa.Identity; Trusted_Connection = true; encrypt = false; MultipleActiveResultSets = true"
        },
  

## Running the Application
- To run the application locally:
    ```bash
    dotnet run
