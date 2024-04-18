using Motorcycle.App.Api;
using Motorcycle.App.Api.Blob;
using Motorcycle.App.Api.Cache;
using Motorcycle.App.Api.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services
            .AddDbConfiuration(builder.Configuration)
            .AddCache(builder.Configuration)
            .AddBlob(builder.Configuration);

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection();

app.AddRoutes();
app.Run();