﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entidades.Generales;
using Entidades.Infos;
using Repositories.Interfaces;
using Dto.Request;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Dto.Response;
using Dto;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonasController : ControllerBase
	{
		private readonly IPersonaRepository _repository;
		private readonly ILogger<PersonasController> _logger;

		public PersonasController(IPersonaRepository repository, ILogger<PersonasController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		// GET: api/Personas
		[HttpGet]
        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> Get()
		{
			var personas = await _repository.ListMinimalAsync();
			var personasDto = personas.Select(x => new PersonaDTOResponse
			{
				Id = x.Id,
				Nombre = x.Nombre,
				ApellidoPaterno = x.ApellidoPaterno,
				ApellidoMaterno = x.ApellidoMaterno,
				DireccionDomicilio = x.DireccionDomicilio,
				TelefonoCelular = x.TelefonoCelular
			});

            _logger.LogInformation("Personas cargadas");
			foreach (var persona in personasDto)
			{
				_logger.LogInformation($"{persona.Id} {persona.Nombre}");
			}

			return Ok(personasDto);
		}

		[HttpGet("{id:int}")]
        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> Get(int id)
		{
			var entity = await _repository.FindByIdAsync(id);

			if (entity is null)
			{
				return NotFound();
			}

			return Ok(entity);
		}

		[HttpPost]
		[Authorize(Roles = "Administrador")]
		public async Task<IActionResult> Post(PersonaDtoRequest request)
		{
			var persona = new Persona
			{
				Nombre = request.Nombre,
				ApellidoPaterno = request.ApellidoPaterno,
				ApellidoMaterno = request.ApellidoMaterno,
				FechaNacimiento = request.FechaNacimiento,
				Rfc = request.Rfc??string.Empty,
				DireccionDomicilio=request.DireccionDomicilio,
				DireccionTrabajo=request.DireccionTrabajo??string.Empty,
				TelefonoCelular=request.TelefonoCelular,
				TelefonoCasa=request.TelefonoCasa ?? string.Empty,
				TelefonoTrabajo=request.TelefonoTrabajo??string.Empty,
				TelefonoContacto=request.TelefonoContacto??string.Empty
			};

			var id = await _repository.AddAsync(persona);

			return CreatedAtAction(nameof(Get), new { id }, persona);
		}

		[HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [AllowAnonymous]
        public async Task<IActionResult> Put(int id, PersonaDtoRequest request)
		{
			var entity = await _repository.FindByIdAsync(id);
			if (entity is null)
			{
				return NotFound();
			}

			entity.Nombre = request.Nombre;
			entity.ApellidoPaterno = request.ApellidoPaterno;
			entity.ApellidoMaterno = request.ApellidoMaterno;
			entity.FechaNacimiento = request.FechaNacimiento;
			entity.Rfc = request.Rfc ?? string.Empty;
			entity.DireccionDomicilio = request.DireccionDomicilio;
			entity.DireccionTrabajo = request.DireccionTrabajo ?? string.Empty;
			entity.TelefonoCelular = request.TelefonoCelular;
			entity.TelefonoCasa = request.TelefonoCasa ?? string.Empty;
			entity.TelefonoTrabajo = request.TelefonoTrabajo ?? string.Empty;
			entity.TelefonoContacto = request.TelefonoContacto ?? string.Empty;

			await _repository.UpdateAsync();

			return Ok();
		}

		[HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
		{
            _logger.LogInformation($"Persona {id}");
            await _repository.DeleteAsync(id);
            _logger.LogInformation($"Persona {id} BORRADA");

            return Ok();
		}

	}
}
