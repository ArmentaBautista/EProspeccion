using DataAccess;
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
			.Where(p => p.Actividad.Contains(filtro) && p.IdEstatus==1)
			.Select(x => new GestionInfo
			{
				Id = x.Id,
				Nombre = $"{x.Persona.Nombre} {x.Persona.ApellidoPaterno} {x.Persona.ApellidoMaterno}",
				Actividad = x.Actividad,
				Resultado = x.Resultado,
				Fecha = x.Fecha,
				Hora = x.Hora,
				Latitud = x.Latitud,
				Longitud =  x.Longitud
			})
			.AsNoTracking()
			.AsQueryable();

			return await gestiones.ToListAsync();
		}
	}
}
