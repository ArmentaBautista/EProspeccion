using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entidades.Sistema;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Implementaciones
{
	public class UsuarioRepository(EPDbContext context) : RepositoryBase<Usuario>(context), IUsuarioRepository
	{
		public async Task<Usuario?> BuscarPorEmailAsync(string email)
		{
			return await Context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}
