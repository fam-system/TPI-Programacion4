using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Middlewares;
using System.Text;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);


//CONFIGURACIÓN DE CONEXIÓN A BASE DE DATOS

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar el DbContext con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



//INYECCIÓN DE REPOSITORIOS (CAPA DE ACCESO A DATOS)

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProcesoRepository, ProcesoRepository>();
builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>();



//INYECCIÓN DE SERVICIOS (CAPA DE LÓGICA DE NEGOCIO)

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProcesoService, ProcesoService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();



//MIDDLEWARE PERSONALIZADO

builder.Services.AddScoped<CustomExceptionHandlingMiddleware>();



//CONFIGURACIÓN DE AUTENTICACIÓN JWT

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
            ),
            RoleClaimType = "rol"
        };
    });

if (!builder.Environment.IsDevelopment())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://fam-api-kv.vault.azure.net/"),
        new DefaultAzureCredential()
    );
}


//CONFIGURACIÓN DE SWAGGER 

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApibearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Pegar aquí el token generado al loguearse"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApibearerAuth"
                }
            },
            new List<string>()
        }
    });
});

//CONFIGURACIÓN DE CORS (allowall para usarlo sin configuracion especifica)

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


//REGISTRO DE CLIENTES HTTP (configuracion de https) 
builder.Services.AddHttpClient<IJokeService, JokeApiClient>(client =>
{
    client.BaseAddress = new Uri("https://official-joke-api.appspot.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

//CONFIGURACIÓN DE CONTROLADORES Y ENDPOINTS

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlingMiddleware>();

app.UseCors("AllowAll");


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
