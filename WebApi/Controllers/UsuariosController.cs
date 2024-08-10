using DataAccess;
using Dto;
using Dto.Request;
using Dto.Response;
using Entidades;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using Entidades.Sistema;

namespace WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UsuariosController : ControllerBase
	{
		private readonly UserManager<UsuarioIdentity> _userManager;
		private readonly IConfiguration _configuration;
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly ILogger<UsuariosController> _logger;

		public UsuariosController(UserManager<UsuarioIdentity> userManager, IConfiguration configuration, IUsuarioRepository usuarioRepository, ILogger<UsuariosController> logger)
		{
			_userManager = userManager;
			_configuration = configuration;
			_usuarioRepository = usuarioRepository;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDtoRequest request)
		{
			var response = new LoginDtoResponse();

			try
			{
				var identity = await _userManager.FindByNameAsync(request.Usuario);
				if (identity is null)
					throw new SecurityException("Usuario no existe");

				if (!await _userManager.CheckPasswordAsync(identity, request.Password))
				{
					throw new SecurityException($"Clave incorrecta para el usuario {identity.UserName}");
				}

				var roles = await _userManager.GetRolesAsync(identity);

				var fechaExpiracion = DateTime.Now.AddHours(1);

				// Vamos a crear los claims
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, identity.Nombre),
					new Claim(ClaimTypes.Email, identity.Email!),
					new Claim(ClaimTypes.Expiration, fechaExpiracion.ToLongDateString())
				};

				claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
				response.Roles = roles;

				// Creamos el JWT
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
				var credenciales =
					new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Algoritmo de encriptación

				var header = new JwtHeader(credenciales);

				var payload = new JwtPayload(
					_configuration["Jwt:Emisor"],
					_configuration["Jwt:Audiencia"],
					claims,
					DateTime.Now,
					fechaExpiracion
				);

				var token = new JwtSecurityToken(header, payload);
				response.Token = new JwtSecurityTokenHandler().WriteToken(token);
				response.NombreCompleto = $"{identity.Nombre} {identity.ApellidoPaterno} {identity.ApellidoMaterno}";
				response.Exito = true;

				_logger.LogInformation("Se creó el JWT correctamente");
			}
			catch (SecurityException ex)
			{
				response.MensajeError = ex.Message;
				_logger.LogCritical(ex.Message);
			}
			catch (Exception ex)
			{
				response.MensajeError = ex.Message;
				_logger.LogWarning("Se intento hacer un login sin exito");
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegistrarUsuarioDto request)
		{
			var response = new BaseResponse();

			try
			{
				var identity = new UsuarioIdentity
				{
					UserName = request.NombreUsuario,
					Email = request.Email,
					Nombre = request.Nombre,
					ApellidoPaterno = request.ApellidoPaterno,
					ApellidoMaterno = request.ApellidoMaterno,
					EmailConfirmed = true
				};

				var result = await _userManager.CreateAsync(identity, request.Password);

				if (!result.Succeeded)
				{
					var sb = new StringBuilder();
					foreach (var identityError in result.Errors)
					{
						sb.AppendFormat("{0} ", identityError.Description);
					}

					response.MensajeError = sb.ToString();
					sb.Clear();
					return BadRequest(response);
				}

				await _userManager.AddToRoleAsync(identity, Constantes.RolUsuario);

				var usuario = new Usuario
				{
					NombreUsuario = request.NombreUsuario,
					Email = request.Email
				};

				await _usuarioRepository.AddAsync(usuario);

				response.Exito = true;
			}
			catch (Exception ex)
			{
				response.MensajeError = ex.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
