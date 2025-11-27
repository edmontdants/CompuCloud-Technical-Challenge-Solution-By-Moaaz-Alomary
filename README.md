# CompuMedical-Technical-Challenge-Solution-By-Moaaz-Alomary
Simple ASP.NET Core 10 MVC for Managing Manual Invoices Entry and Calculating Taxes Embedded with a Clean Architecture ASP.NET Core API Project By Moaaz Alomary

This is the Challenge Solution for .NET Backend Developer Position at Compu Medical

Basic ASP.NET Core 10 MVC for Managing Invoices and Invoice Lines.

## Run

1. `dotnet tool install --global dotnet-ef`
2. `dotnet ef database update --project InvoiceApp.Infrastructure --startup-project InvoiceApp.Api`
3. `dotnet restore`
4. `dotnet run`

5. Open  `http://localhost:44388/swagger` to access Swagger UI on iis express profile or `http://localhost:5047/swagger` to access on http profile or `http://localhost:7005/swagger` if https profile.
