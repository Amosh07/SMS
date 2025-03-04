using SMS.API.Middleware;
using SMS.Identity.Dependency;
using SMS.Infrastructure.Dependency;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configurations = builder.Configuration;

builder.AddConfigurations();

services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddEndpointsApiExplorer();

services.AddDependencyServices();

services.AddIdentityServices(configurations);

services.AddInfrastructureService(configurations);

await services.AddDataSeedMigrationService();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.DocExpansion(DocExpansion.None);
        options.DefaultModelsExpandDepth(-1);
        options.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
