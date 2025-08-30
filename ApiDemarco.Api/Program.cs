using System.Net;
using ApiDemarco.Api.Extensions;
using ApiDemarco.Api.Middlewares;
using ApiDemarco.Application.Logging;
using ApiDemarco.Application.Validations.Clientes;
using ApiDemarco.Infrastructure.Data;
using ApiDemarco.Infrastructure.Logging;
using ApiDemarco.Infrastructure.Migrations;
using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Configuração de URLs
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 5000); 
});

var mongoConn = builder.Configuration["MongoSettings:ConnectionString"]; 

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MongoDBBson(mongoConn, collectionName: "logsApplication")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(MongoLoggerAdapter<>));
builder.Services.AddDependencyInjection();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoConnection = builder.Configuration["MongoSettings:ConnectionString"];
    return new MongoClient(mongoConnection);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Description = @"API-DEMARCO",
    });
});

// Configurando FluentMigrator
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ScanIn(typeof(CreateSchemas).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole()); // logging para monitorar as migrações

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.WriteIndented = true; });

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<ClienteCreateValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => corsPolicyBuilder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiDemarco API v1");
        c.RoutePrefix = "swagger";
        c.InjectStylesheet("/swagger-custom/custom-swagger.css");
        c.DefaultModelsExpandDepth(-1);
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var migrator = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    migrator.MigrateUp();
    //migrator.MigrateDown(0);
}

app.Run();
