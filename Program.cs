using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderManagementAPI.Infra;
using OrderManagementAPI.Repositories;
using OrderManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar a autentica��o antes de construir o app
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                // Validar o emissor (quem gerou o token)
            ValidateAudience = true,              // Validar o p�blico (quem pode consumir o token)
            ValidateLifetime = true,              // Validar se o token n�o expirou
            ValidIssuer = builder.Configuration["Jwt:Issuer"],   // Emissor do token (quem gerou)
            ValidAudience = builder.Configuration["Jwt:Audience"], // P�blico (quem pode usar)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])) // A chave usada para assinar o token
        };
    });

// Adicionar o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar o reposit�rio
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar o UserService
builder.Services.AddScoped<UserService>();  // Registre o UserService aqui

// Adicionar os servi�os de controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); // Criar o app

// Configurar o Swagger e a autoriza��o
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Habilitar a autentica��o
app.UseAuthorization();  // Habilitar a autoriza��o

app.MapControllers(); // Mapear os controllers

app.Run(); // Rodar a aplica��o
