using backend;
using backend.Infrastructure;
using backend.IServices;
using backend.RepositoryInterfaces;
using backend.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//dependency injection
builder.Services.Configure();


//add db
var Configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsHistoryTable("_EfMigrations", Configuration.GetSection("Schema").GetSection("public").Value)));



var frontEndOrigin = "frontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: frontEndOrigin,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(frontEndOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
