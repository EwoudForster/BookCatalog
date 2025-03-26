var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BookCatalog>("bookcatalog");

builder.Build().Run();
