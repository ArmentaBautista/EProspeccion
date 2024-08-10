using Entidades.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface IUsuarioRepository : IRepositoryBase<Usuario>
	{
		Task<Usuario?> BuscarPorEmailAsync(string email);
	}
}
