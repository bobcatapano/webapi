using Microsoft.EntityFrameworkCore;
using WebApi3_BAL.Services;
using WebApi3_DAL.Contracts;
using WebApi3_DAL.Data;
using WebApi3_DAL.Model;
//using Npgsql.EntityFrameworkCore.PostgreSQL;
using WebApi3_DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connectionString));

//options.UseNpgsql(connectionString));

//Add Service Injection
builder.Services.AddSingleton<IRepository<AppUser>, RepositoryAppUser>();
builder.Services.AddSingleton<ServiceAppUser, ServiceAppUser>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTToken_Auth_API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {

    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey =
        bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateIssuerSigningKey"]),
        IssuerSigningKey = new
        SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JsonWebTokenKeys:IssuerSigningKey"])),
        ValidateIssuer = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateIssuer"]),
        ValidAudience = builder.Configuration["JsonWebTokenKeys:ValidAudience"],
        ValidIssuer = builder.Configuration["JsonWebTokenKeys:ValidIssuer"],
        ValidateAudience = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateAudience"]),
        RequireExpirationTime = bool.Parse(builder.Configuration["JsonWebTokenKeys:RequiredExpirationTime"]),
        ValidateLifetime = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateLifetime"])
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
