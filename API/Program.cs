using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
   opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
//imas varijanti transient, scoped, singleton gi koristis
//bazirano na potrebata na tipot na instanca
builder.Services.AddScoped<ITokenService, TokenService>();
   builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(Options =>
   {
      var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("Token key not found = Program.cs");
      Options.TokenValidationParameters = new TokenValidationParameters{
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
         ValidateIssuer = false,
         ValidateAudience = false
      };
   });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//This part is for middleware. Middleware modifies http requests and responses
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

//who are you? answers to this
app.UseAuthentication();
//once we know who you are what are you allowed to do? 
app.UseAuthorization();

app.MapControllers();

app.Run();
