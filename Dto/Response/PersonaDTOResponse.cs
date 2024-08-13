using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Response
{
    public class PersonaDTOResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }=string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public string DireccionDomicilio { get; set; } = string.Empty;
        public string TelefonoCelular { get; set; } = string.Empty;
    }
}
