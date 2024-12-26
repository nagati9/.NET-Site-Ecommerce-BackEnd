using System;
using System.Text;
using Gestionnaire2.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var key = Encoding.UTF8.GetBytes("VotreCleSecretePourJWT1234567890!"); // Assurez-vous que la clé fait au moins 32 caractères

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31))));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Si vous testez uniquement le back, mettez "*" pour tout autoriser temporairement
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // Autorise l'envoi des cookies de session
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7249",
        ValidAudience = "https://localhost:7249",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddDistributedMemoryCache(); // Pour stocker les sessions en mémoire
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Temps d'inactivité avant expiration
    options.Cookie.HttpOnly = true; // Plus sécurisé
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Utilisez la politique CORS
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthentication(); // Important : Active l'authentification
app.UseAuthorization();

app.MapControllers();

app.Run();
