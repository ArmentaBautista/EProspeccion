using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entidades.Generales;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Implementaciones
{
	public class PersonaRepository:RepositoryBase<Persona>, IPersonaRepository
	{
		public PersonaRepository(EPDbContext context) 
			: base(context)
		{
		}

		public async Task<ICollection<Persona>> ListMinimalAsync()
		{ 
			return await Context.Set<Persona>()
				.Where(p => p.IdEstatus == 1)
				.AsNoTracking()
				.Select(x => new Persona 
				{
					Id = x.Id,
					Nombre = $"{x.Nombre} {x.ApellidoPaterno} {x.ApellidoMaterno}",
					FechaNacimiento=x.FechaNacimiento,
					Rfc=x.Rfc
				})
				.ToListAsync();
		}
	}
}
