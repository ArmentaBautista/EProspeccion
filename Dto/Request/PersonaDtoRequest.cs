using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
	public class PersonaDtoRequest
	{
		[Required(ErrorMessage =Constantes.CampoRequerido)]
		[StringLength(20,ErrorMessage =Constantes.CampoLargoValidacion)]
		public string Nombre { get; set; }
		
		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[StringLength(20, ErrorMessage = Constantes.CampoLargoValidacion)]
		public string ApellidoPaterno { get; set; }

		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[StringLength(20, ErrorMessage = Constantes.CampoLargoValidacion)]
		public string ApellidoMaterno { get; set; }

		[Required(ErrorMessage = Constantes.CampoRequerido)]
		public DateOnly FechaNacimiento { get; set; }

		public string? Rfc { get; set; } = string.Empty;

		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[StringLength(150, ErrorMessage = Constantes.CampoLargoValidacion)]
		public string DireccionDomicilio { get; set; }=string.Empty;
		
		public string? DireccionTrabajo { get; set; }

		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[StringLength(10, ErrorMessage = Constantes.CampoLargoValidacion)]
		public string TelefonoCelular { get; set; }
		
		public string? TelefonoCasa { get; set; } = string.Empty;
		public string? TelefonoTrabajo { get; set; } = string.Empty;
		public string? TelefonoContacto { get; set; } = string.Empty;

	}
}
