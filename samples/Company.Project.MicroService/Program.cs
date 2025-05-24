using Company.MicroChassis.AspNetCore;
using Company.MicroModules.Redis;
using MicroChassis.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddMicroChassis<CompanyWebApplicationBuilderMicroChassis>()
       .Mount<Redis>();

var app = builder.Build();

app.UseMicroChassis<CompanyWebApplicationMicroChassis>();
app.MapGet("/", () => "Hello World!");

app.Run();