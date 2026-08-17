using Medicare_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegistrarServices(typeof(Program));

var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));

app.Run();
