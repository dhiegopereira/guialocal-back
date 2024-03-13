using Microsoft.OpenApi.Models;
using guialocal.Data;
using guialocal.Services;
using guialocal.Middlewares;
using guialocal.Middlewares.Schemas;
using guialocal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureCors(builder);

ConfigureDB(builder);

ConfigureSwagger(builder);

builder.Services.AddControllers();

builder.Services.AddScoped<CustomerService>();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Guia Local API v1");
    //options.RoutePrefix = "";
});

app.MapControllers();

app.Run();

void ConfigureDB(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(options => {
        
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Guia Local API",
            Version = "v1",
            Description = "A simple API for managing tasks",
            Contact = new OpenApiContact
            {
                Name = "Diego Pereira",
                Email = "dhiegopereira@devcollege.com.br",
                Url = new Uri("https://devcollegeacademy.com.br/")
            }
        });
    });
}

void ConfigureCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options => {
        options.AddPolicy("AllowAllOrigins", builder => {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });
}