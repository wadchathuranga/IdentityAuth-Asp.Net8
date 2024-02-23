using IdentityAuth.Data;
using IdentityAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Define repository interfaces bellow

// Add Athentication
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

// Add Authorization
builder.Services.AddAuthorizationBuilder();

// Register database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString: "DataSource=appdata.db"));

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();


app.MapIdentityApi<AppUser>();

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
