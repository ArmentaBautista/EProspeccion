using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entidades.Generales;
using Entidades.Infos;
using Repositories.Interfaces;
using Dto.Request;
using Dto.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Dto;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GestionesController : Controller
	{
		private readonly IGestionRepository _repository;
		private readonly ILogger<GestionesController> _logger;


        public GestionesController(IGestionRepository repository,
            ILogger<GestionesController> logger)
        {
			_repository = repository;
			_logger = logger;

		}

		[HttpGet]
        [Authorize(Roles = "Administrador,Usuario")]
        public async Task<IActionResult> Get(string? filtro)
		{
			var coleccion = await _repository.ListAsync(filtro ?? string.Empty);

			var response = coleccion.Select(p => new GestionDtoResponse
			{
				Id = p.Id,
				Nombre = p.Nombre,
				Actividad = p.Actividad,
				Resultado = p.Resultado,
				Fecha = p.Fecha,
				Hora = p.Hora
			}).ToList();

			return Ok(response);
		}

		[HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador,Usuario")]
        public async Task<IActionResult> Get(int id)
		{
			var entity = await _repository.FindByIdAsync(id);

			if (entity is null)
				return NotFound();

			return Ok(new GestionDtoResponse
			{
				Id = entity.Id,
				Nombre = $"{entity.Persona.Nombre} {entity.Persona.ApellidoPaterno} {entity.Persona.ApellidoMaterno}",
				Actividad = entity.Actividad,
				Resultado = entity.Resultado,
				Fecha = entity.Fecha,
				Hora = entity.Hora
			});
		}

		[HttpPost]
        [Authorize(Roles = Constantes.RolUsuario)]
        public async Task<IActionResult> Post(GestionDtoRequest request)
		{
			var entity = new Gestion
			{
				Id = request.Id,
				IdPersona=request.IdPersona ,
				Actividad = request.Actividad,
				Resultado = request.Resultado,
				Fecha = request.Fecha,
				Hora = request.Hora
			};

			await _repository.AddAsync(entity);

			return Created($"api/gestiones/{entity.Id}", entity);
		}

		[HttpPut("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Put(int id, GestionDtoRequest request)
		{
			var entity = await _repository.FindByIdAsync(id);
			if (entity is null)
				return NotFound();

			entity.Id = request.Id;
			entity.Persona = new Persona() { Id = request.IdPersona };
			entity.Actividad = request.Actividad;
			entity.Resultado = request.Resultado;
			entity.Fecha = request.Fecha;
			entity.Hora = request.Hora;

			await _repository.UpdateAsync();

			return Ok();
		}

		[HttpDelete("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Delete(int id)
		{
			await _repository.DeleteAsync(id);

			return Ok();
		}

	}
}
