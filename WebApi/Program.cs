using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Events;
using Repositories.Interfaces;
using Repositories.Implementaciones;
using DataAccess.Sistema;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
	.WriteTo.Console(LogEventLevel.Warning)
	.CreateLogger();

//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<EPDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("eprospeccion"));
});
builder.Services.AddTransient<IPersonaRepository,PersonaRepository>();
builder.Services.AddTransient<IGestionRepository,GestionRepository>();

// Configuramos ASP.NET Core Identity
builder.Services.AddIdentity<EIdentity, IdentityRole>(policies =>
{
	// Politicas de clave
	policies.Password.RequireDigit = true;
	policies.Password.RequireLowercase = true;
	policies.Password.RequireUppercase = true;
	policies.Password.RequireNonAlphanumeric = true;
	policies.Password.RequiredLength = 8;

	// Politicas de Usuario
	policies.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<EPDbContext>()
	.AddDefaultTokenProviders();

// Configuramos el contexto de seguridad del API
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
										   throw new InvalidOperationException("No se configuro el SecretKey"));

	// Con estos parametros validamos la autenticidad del token
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Emisor"],
		ValidAudience = builder.Configuration["Jwt:Audiencia"],
		IssuerSigningKey = new SymmetricSecurityKey(secretKey)
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
//app.UseBlazorFrameworkFiles();
//app.UseStaticFiles();
app.UseRouting();
// Autenticacion (Validacion de identidad)
app.UseAuthentication();
// Autorizacion (Validacion de permisos)
app.UseAuthorization();
app.MapControllers();

await using var scope = app.Services.CreateAsyncScope();
{
	await InicializadorUsuarios.Crear(scope.ServiceProvider);
}

app.Run();
