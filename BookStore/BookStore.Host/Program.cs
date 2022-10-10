using System.Text;
using BookStore.BL.CommandHandlers;
using BookStore.DL.Repositories.SQLRepositories;
using BookStore.Host.Extensions;
using BookStore.Host.HealthChecks;
using BookStore.Host.Middleware;
using BookStore.Host.Test;
using BookStore.Models.Models.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services
    .RegisterRepositories()
    .RegisterServices()
    .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));//

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token in the text box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>()}
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("View", policy =>
    {
        policy.RequireClaim("View");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = 
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//health checks
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>(name: "New Custom Check")
    .AddUrlGroup(new Uri("https://google.bg"), name: "My Service")
    .AddCheck<MySqlHealthCheck>(name: "SQL Server");

builder.Services.AddMediatR(typeof(GetAllBooksCommandHandler).Assembly);

builder.Services.AddIdentity<UserInfo, UserRole>()
    .AddUserStore<UserInfoStore>()
    .AddRoleStore<UserRoleStore>();

//App Builder below
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.RegisterHealthChecks();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();


//public partial class Program { }