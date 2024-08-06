using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entidades.Generales;
using Repositories.Interfaces;
using Entidades;

namespace Repositories.Implementaciones
{
	public class RepositoryBase<TEntidad>:IRepositoryBase<TEntidad>
		where TEntidad : EntidadBase
	{

		protected readonly DbContext Context;

		public RepositoryBase(DbContext context) 
		{
			Context=context;
		}

		public virtual async Task<ICollection<TEntidad>> ListAsync()
		{
			return await Context
				.Set<TEntidad>()
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<ICollection<TEntidad>> ListAsync(Func<TEntidad, bool> predicado)
		{
			var collection = await Context.Set<TEntidad>()
				.Where(predicado)
				.AsQueryable()
				.AsNoTracking()
				.ToListAsync();

			return collection;
		}

		public async Task<TEntidad?> FindByIdAsync(int id)
		{
			return await Context.Set<TEntidad>()
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public virtual async Task<int> AddAsync(TEntidad entidad)
		{
			await Context.Set<TEntidad>().AddAsync(entidad); 
			await Context.SaveChangesAsync();

			return entidad.Id;
		}

		public async Task UpdateAsync()
		{
			await Context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entidadExistente = await FindByIdAsync(id);
			if (entidadExistente is not null)
			{
				entidadExistente.IdEstatus = 2;
				await Context.SaveChangesAsync();
			}
		}
	}
}
