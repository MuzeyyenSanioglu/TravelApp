using Microsoft.OpenApi.Models;
using TravelApp.API.Helper;
using TravelApp.Infrastructure;
using TravelApp.Infrastructure.Concrete;
using TravelApp.Infrastructure.Concrete.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddAuthConfiguration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TraveltAPI", Version = "v1" });
});
builder.Services.AddSwaggerConfiguration(builder.Configuration);
builder.Services.AddScoped<ITokenHandler, JWTHandler>();
var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(url: String.Format(builder.Configuration.GetSection("Swagger:UseSwaggerUI:SwaggerEndpoint").Value, builder.Configuration.GetSection("Swagger:SwaggerName").Value),
            name: "Version CoreSwaggerWebAPI-1");

    });
}
app.MapControllers();

app.Run();
