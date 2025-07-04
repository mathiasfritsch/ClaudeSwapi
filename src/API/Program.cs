using Application.Interfaces;
using Application.Services;
using Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SWAPI Integration API", Version = "v1" });
});

// Add application services
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IPlanetService, PlanetService>();
builder.Services.AddScoped<ISpeciesService, SpeciesService>();
builder.Services.AddScoped<IStarshipService, StarshipService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

// Add infrastructure services (HTTP clients, etc.)
builder.Services.AddInfrastructure();

// Add CORS for frontend integration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SWAPI Integration API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();