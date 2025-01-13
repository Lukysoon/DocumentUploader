# Document Uploader

A full-stack application for uploading and managing documents with tagging capabilities.

## Prerequisites

Before running this project, make sure you have the following installed:

- SQL Server
- .NET 6.0 or later
- Node.js and npm

## Setup Instructions

### Backend (.NET)
1. Add appsettings.json to the project:
```
"ConnectionStrings": {
  "DocumentContext": "Server=localhost\\sqlexpress;Database=DocumentService;TrustServerCertificate=True;Trusted_Connection=True"
}
```

2. Navigate to the backend directory:
```bash
cd DocumentService
```

3. Install Entity Framework CLI tools (if not already installed):
```bash
dotnet tool install --global dotnet-ef
```

4. Restore dependencies:
```bash
dotnet restore
```

5. Update database with migrations:
```bash
dotnet ef database update
```

6. Run the backend server:
```bash
dotnet run
```

The backend API will be available at http://localhost:5229 with Swagger UI at http://localhost:5229/swagger

### Frontend (React)

1. Navigate to the frontend directory:
```bash
cd document-uploader-client
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The frontend application will be available at http://localhost:3000

## Running the Application

1. Start the backend server first (follow steps in Backend section)
2. In a new terminal, start the frontend server (follow steps in Frontend section)
3. Access the application through http://localhost:3000 in your web browser

Note: Keep both the backend and frontend servers running simultaneously for the application to work properly.
