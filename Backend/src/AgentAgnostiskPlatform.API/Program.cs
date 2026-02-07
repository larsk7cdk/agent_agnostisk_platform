using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi;
using Serilog;
using AgentAgnostiskPlatform.API.Controllers.Shared;
using AgentAgnostiskPlatform.API.Middlewares;
using AgentAgnostiskPlatform.Application;
using AgentAgnostiskPlatform.Infrastructure;
using AgentAgnostiskPlatform.Persistence;

using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Starting Agent Agnostisk Platform API");

var builder = WebApplication.CreateBuilder(args);

// Load and bind configuration
var configuration = builder.Configuration;

// Set Danish culture globally
var cultureInfo = new CultureInfo("da-DK");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


// Global Exception Handler
builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context => { context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier; };
});

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger());

// Add Application services to the container.
builder.Services
    .AddApplication(configuration)
    .AddInfrastructure(configuration)
    .AddPersistence(configuration);

builder.Services.AddHttpContextAccessor();

// Add Swagger
builder.Services
    .AddOpenApi()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Agent Agnostisk Platform API",
            Description = "ASP.NET Core Web API for Agent Agnostic Platform",
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        options.IncludeXmlComments(xmlPath);
    });

// Add Controllers
builder.Services.AddControllers(options => { options.Conventions.Add(new RouteTokenTransformerConvention(new LowerCaseParameterTransformer())); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Add Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Use HTTP Strict Transport Security Protocol
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Enable routing, needed when using controllers
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// CORS configuration
app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE");
    x.WithOrigins("http://localhost:4200");
    x.AllowCredentials();
});

// Use controllers as endpoints
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


// Run the application
try
{
    app.Run();
}
catch (Exception e)
{
    Log.Error("Application error: {Message}", e.Message);
}
finally
{
    Log.CloseAndFlush();
}