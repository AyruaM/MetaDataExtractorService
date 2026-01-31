📁 Folder Structure
document-metadata-extractor-api
│
├── MetaDataExtractorService
│   ├── Controllers
│   ├── Data
│   ├── DataFiles
│   ├── Migrations
│   ├── Model
│   ├── Services
│   ├── tessdata
│   ├── appsettings.json
│   ├── Program.cs
│   └── MetaDataExtractorService.csproj
│
├── .gitignore
├── README.md
└── MetaDataExtractorService.sln

🧩 Steps to Create Backend Repo
1. Create a new folder
mkdir document-metadata-extractor-api
cd document-metadata-extractor-api

2. Move backend files

Move only your .NET files into this folder:

mv MetaDataExtractorService MetaDataExtractorService.sln document-metadata-extractor-api/

3. Initialize Git
git init
git add .
git commit -m "Initial commit: .NET 10 Document Metadata Extractor API"

4. Add .gitignore

Use Microsoft’s official template:

dotnet new gitignore
