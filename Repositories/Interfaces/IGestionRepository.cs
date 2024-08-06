using Entidades.Generales;
using Entidades.Infos.Generales;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface IGestionRepository:IRepositoryBase<Gestion>
	{
		Task<ICollection<GestionInfo>> ListAsync(string filtro);
	}
}
