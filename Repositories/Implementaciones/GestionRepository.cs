﻿using DataAccess;
using Entidades.Generales;
using Entidades.Infos.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementaciones
{
	public class GestionRepository:RepositoryBase<Gestion>, IGestionRepository
	{
		public GestionRepository(EPDbContext context)
			: base(context) 
		{ 
		}
	

		public async Task<ICollection<GestionInfo>> ListAsync(string filtro)
		{
			var gestiones = Context.Set<Gestion>()
			.Where(p => p.Persona.Nombre.Contains(filtro))
			.Select(x => new GestionInfo
			{
				Nombre = $"{x.Persona.Nombre} {x.Persona.ApellidoPaterno} {x.Persona.ApellidoMaterno}",
				Actividad = x.Actividad,
				Respuesta = x.Resultado,
				Fecha = x.Fecha,
				Hora = x.Hora
			})
			.AsNoTracking()
			.AsQueryable();

			return await gestiones.ToListAsync();
		}
	}
}
