using InvoiceApp.Core.Entities;
using InvoiceApp.Core.Interfaces;
using InvoiceApp.Infrastructure.Data;
using InvoiceApp.Infrastructure.Repositories;
using InvoiceApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
//using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

// JWT Authentication Configuration
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ValidateLifetime = true
                    };
                });

// Authorization Policy Example (optional)
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

// CORS Policy
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Swagger + JWT Auth UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Invoice API",
        Version = "v1"
    });

    //option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.Http,
    //    Scheme = "Bearer",
    //    BearerFormat = "JWT",
    //    In = ParameterLocation.Header,
    //    Description = "Enter JWT token in format: Bearer {token}"
    //});

    //option.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        Array.Empty<string>()
    //    }
    //});
});

// register repos and services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();


var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await SeedInitialData(app);

app.Run();

static async Task<int> SeedInitialData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (! await db.Database.CanConnectAsync())
        await db.Database.MigrateAsync();

    if (!db.Users.Any())
    {
        CreatePassword("Admin@CM", out byte[] hash, out byte[] salt);
        db.Users.Add(new User { Username = "admin", PasswordHash = hash, PasswordSalt = salt, Role = "Admin" });
    }

    if (!db.Stores.Any())
    {
        db.Stores.AddRange(
            new Store { Name = "Main Store" },
            new Store { Name = "Online Store" }
        );
    }

    if (!db.Units.Any())
    {
        db.Units.AddRange(
            new Unit { Name = "Piece" },
            new Unit { Name = "Kg" },
            new Unit { Name = "Box" }
        );
    }

    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Name = "Laptop"},
            new Product { Name = "Keyboard"}
        );
    }

    return await db.SaveChangesAsync();
}

static void CreatePassword(string password, out byte[] hash, out byte[] salt)
{
    using var hmac = new HMACSHA512();
    salt = hmac.Key;
    hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
}
