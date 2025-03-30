using Microsoft.EntityFrameworkCore;
using NationsBenefits.Infrastructure.Persistence;
using NationsBenefits.Application;
using NationsBenefits.Infrastructure;
using NationsBenefits.API.Middleware;
using NationsBenefits.Application.Cache;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
    new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Nations Benefit API",
        Version = "v1",
        Description = "Web API to manage SubCatagories and Products"
    });
});

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
    );

var app = builder.Build();

ConnectionHelper.AppSettingsConfigure(app.Services.GetRequiredService<IConfiguration>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<NationsBenefitsDbContext>();
        await context.Database.MigrateAsync();
        await NationBenefitsDbContextSeed.SeedAsync(context, loggerFactory);

        var contextIdentity = service.GetRequiredService<NationsBenefitsDbContext>();
        await contextIdentity.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Migration Error");
    }
}


app.Run();
