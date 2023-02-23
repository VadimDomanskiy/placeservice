using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PlaceService.Api.Middleware;
using PlaceService.Application.Extensions;
using PlaceService.Application.Interfaces.Repositories;
using PlaceService.Application.Options;
using PlaceService.Infrastructure.Persistance.Context;
using PlaceService.Infrastructure.Persistance.Seeds;
using PlaceService.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddNewtonsoftJson();

var apiKey = builder.Configuration.GetSection("ApiKey").Get<ApiKey>();

var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddTransient<IContractRepository, ContractRepository>();
builder.Services.AddTransient<IEquipmentTypeRepository, EquipmentTypeRepository>();
builder.Services.AddTransient<IProductionRoomRepository, ProductionRoomRepository>();

var paths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*Application.dll").ToList();
paths.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*Infrastructure.dll"));

Assembly[] assemblies = paths.Select(path => Assembly.Load(AssemblyName.GetAssemblyName(path))).ToArray();

builder.Services.AddAutoMapper(assemblies);
builder.Services.AddMediatR(assemblies);

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    app.EnsureContextMigrated<ApplicationDbContext>();
    SeedData.EnsureDataSeeded(scope.ServiceProvider).ConfigureAwait(false).GetAwaiter().GetResult();
}

    app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseMiddleware<ApiKeyMiddleware>(apiKey.Key);
app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseAuthorization();

app.MapControllers();

app.Run();
