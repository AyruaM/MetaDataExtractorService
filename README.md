📄 Document Metadata Extractor API

ASP.NET Core (.NET 10) | EF Core | OCR

Overview

Document Metadata Extractor API is a backend service built with ASP.NET Core (.NET 10) and Entity Framework Core.
It is responsible for extracting, processing, and persisting metadata from documents, including OCR-based text extraction.

This API is designed to work as a standalone service and integrates seamlessly with an Angular frontend.

🧱 Tech Stack

.NET 10

ASP.NET Core Web API

Entity Framework Core

OCR (Tesseract)

Swagger / OpenAPI

SQL Server  (configurable)

🚀 Getting Started
1️⃣ Prerequisites

.NET SDK 10.x

SQL Server or SQLite

Git

(Optional) EF Core CLI tools

dotnet tool install --global dotnet-ef

2️⃣ Clone the Repository
git clone https://github.com/<your-username>/document-metadata-extractor-api.git
cd document-metadata-extractor-api

3️⃣ Restore & Build
dotnet restore
dotnet build

4️⃣ Database Setup

Apply EF Core migrations:

dotnet ef database update --project MetaDataExtractorService

5️⃣ Run the API
dotnet run --project MetaDataExtractorService


The API will start at:

https://localhost:5001


Swagger UI:

https://localhost:5001/swagger

🔌 API Endpoints (Sample)
Method	Endpoint	Description
GET	/api/ping	Health check
POST	/api/extractor/upload	Upload document
GET	/api/documents	Retrieve extracted metadata
🧠 OCR Configuration

OCR uses Tesseract.

Language data stored in:

/MetaDataExtractorService/tessdata


Default language:

eng.traineddata


You can add more languages by placing additional .traineddata files in this folder.


⚙️ Configuration
appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MetaDataExtractorDb;Trusted_Connection=True;"
  },
  "OCR": {
    "TessDataPath": "tessdata"
  }
}

🧪 Development Notes

WeatherForecastController is a template controller and can be removed safely

DataFiles is used for temporary or persisted document storage

Business logic is isolated in the Services layer
