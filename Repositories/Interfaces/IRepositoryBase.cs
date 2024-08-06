using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface IRepositoryBase<TEntidad> 
		where TEntidad : EntidadBase
	{
		Task<ICollection<TEntidad>> ListAsync();
		Task<ICollection<TEntidad>> ListAsync(Func<TEntidad, bool> predicate);
		Task<TEntidad?> FindByIdAsync(int id);
		Task<int> AddAsync(TEntidad entidad);

		Task UpdateAsync();

		Task DeleteAsync(int id);
	}
}
