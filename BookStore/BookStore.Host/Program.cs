using BookStore.Host.Extensions;
using BookStore.Host.HealthChecks;
using FluentValidation;
using FluentValidation.AspNetCore;
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
builder.Services.AddSwaggerGen();

//health checks
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>(name: "New Custom Check")
    .AddUrlGroup(new Uri("https://google.bg"), name: "My Service")
    .AddCheck<MySqlHealthCheck>(name: "SQL Server");

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

app.MapControllers();

app.RegisterHealthChecks();


app.Run();


//public partial class Program { }