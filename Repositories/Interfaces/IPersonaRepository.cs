using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPersonaRepository:IRepositoryBase<Persona>
    {
        Task<ICollection<Persona>> ListMinimalAsync();
    }
}
