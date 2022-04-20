



using Event_planner.Data;
using Event_planner.Repositories;
using Event_planner.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using EventPlanner.Domain.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MemberJWTDemo v1"));
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();



void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ISingletonSecretsManagerService, SecretsManagerService>();
    services.AddScoped<IEventPlannerRepository, EventPlannerRepository>();
    services.AddScoped<IEventPlannerService, EventPlannerService>();
    services.AddScoped<ICalendarService, CalendarService>();
    services.AddScoped<ICalendarRepository, CalendarRepository>();

    services.AddDbContext<EventPlannerContext>();
    services.AddAutoMapper(typeof(Program).Assembly);
    services.AddMvc();
    services.AddControllers().AddNewtonsoftJson();
    services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EventDTOValidator>());

    services.AddControllers();
    var key = "This is my first Test Key";
    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
        };
    });

    services.AddSingleton<IJwtAuth>(new Auth(key));
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MemberJWTDemo", Version = "v1" });
    });
}