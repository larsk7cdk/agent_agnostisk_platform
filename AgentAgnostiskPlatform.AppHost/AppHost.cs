var builder = DistributedApplication.CreateBuilder(args);

// Add SQL Server password parameter
var sqlPassword = builder.AddParameter("sql-password", secret: true);

// Read from configuration
var sqlContainerName = builder.Configuration["Parameters:sql-container-name"]!;
var sqlDataVolume = builder.Configuration["Parameters:sql-data-volume"]!;
var sqlAppDbName = builder.Configuration["Parameters:sql-app-db-name"]!;

// Add SQL Server container with persistent volume and fixed port
var sqlServer = builder.AddSqlServer("agent-sql", password: sqlPassword)
    .WithContainerName(sqlContainerName)
    .WithDataVolume(sqlDataVolume) // Persist data between restarts
    .WithHostPort(1433) // Fix to host port 1433
    .WithEndpointProxySupport(false)
    .AddDatabase(sqlAppDbName);

var apiService = builder.AddProject<Projects.AgentAgnostiskPlatform_API>("apiservice")
    .WithReference(sqlServer)
    .WithHttpHealthCheck("/health")
    .WaitFor(sqlServer);

builder.Build().Run();