



using Event_planner.Data;
using Event_planner.Repositories;
using Event_planner.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using EventPlanner.Domain.Validation;

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



void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ISingletonSecretsManagerService, SecretsManagerService>();
    services.AddScoped<IEventPlannerRepository, EventPlannerRepository>();
    services.AddScoped<IEventPlannerService, EventPlannerService>();
    services.AddDbContext<EventPlannerContext>();
    services.AddAutoMapper(typeof(Program).Assembly);
    services.AddMvc();
    services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EventDTOValidator>());
}