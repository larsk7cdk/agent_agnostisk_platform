var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AgentAgnostiskPlatform_API>("apiservice")
    .WithHttpHealthCheck("/health");

builder.Build().Run();
