


using Amazon.Extensions.NETCore.Setup;
using Amazon.SecretsManager;
using Event_planner.Data;
using Event_planner.Models;
using Event_planner.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigureServices(builder.Services);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



void ConfigureServices(IServiceCollection services) {
    services.AddSingleton<ISingletonSecretsManagerService, SecretsManagerService>();
    services.AddDbContext<EventPlannerContext>();
}