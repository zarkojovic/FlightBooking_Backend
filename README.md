# Flight Booking App - Backend

## Overview
Flight reservation application built using C# with a clean architecture approach. The application leverages CQRS, Entity Framework, Dependency Injection, and SignalR. It also implements JWT for authentication and granular authorization based on roles and use case IDs.

## Features
- Flight management (create, retrieve, delete flights)
- Reservation management (create, retrieve, approve reservations)
- User management (create user, login)
- Real-time updates with SignalR
- Secure authentication with JWT
- Granular authorization based on roles and use case IDs

## Technologies Used
- C#
- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- Entity Framework
- Dependency Injection
- SignalR
- JWT (JSON Web Tokens)

## API Endpoints

### Flight
- **POST /Flight**: Create a new flight
- **GET /Flight**: Retrieve all flights
- **DELETE /Flight/{id}**: Delete a flight by ID

### Reservation
- **POST /Reservation**: Create a new reservation
- **GET /Reservation**: Retrieve all reservations
- **PATCH /Reservation/{id}/approve**: Approve a reservation by ID

### User
- **POST /User**: Create a new user
- **POST /User/login**: User login

## Getting Started

### Prerequisites
- .NET SDK
- SQL Server

### Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/zarkojovic/FlightBooking_Backend.git
   ```
2. Navigate to the project directory:
   ```sh
   cd FlightBooking_Backend
   ```
3. Restore the dependencies:
   ```sh
   dotnet restore
   ```
4. Update the database:
   ```sh
   dotnet ef database update
   ```

### Running the Application
1. Build the project:
   ```sh
   dotnet build
   ```
2. Run the project:
   ```sh
   dotnet run
   ```

## Contributing
Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License
This project is licensed under the MIT License.
