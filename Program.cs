using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderManagementAPI.Infra;
using OrderManagementAPI.Repositories;
using OrderManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar a autenticação antes de construir o app
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                // Validar o emissor (quem gerou o token)
            ValidateAudience = true,              // Validar o público (quem pode consumir o token)
            ValidateLifetime = true,              // Validar se o token não expirou
            ValidIssuer = builder.Configuration["Jwt:Issuer"],   // Emissor do token (quem gerou)
            ValidAudience = builder.Configuration["Jwt:Audience"], // Público (quem pode usar)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])) // A chave usada para assinar o token
        };
    });

// Adicionar o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar o repositório
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar o UserService
builder.Services.AddScoped<UserService>();  // Registre o UserService aqui

// Adicionar os serviços de controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); // Criar o app

// Configurar o Swagger e a autorização
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Habilitar a autenticação
app.UseAuthorization();  // Habilitar a autorização

app.MapControllers(); // Mapear os controllers

app.Run(); // Rodar a aplicação
