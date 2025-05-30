var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BookCatalog_ApiService>("apiservice");

builder.AddProject<Projects.BookCatalog_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
