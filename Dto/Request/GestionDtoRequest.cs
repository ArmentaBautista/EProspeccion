using Dto.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Request
{
	public class GestionDtoRequest
	{
		public int Id { get; set; }
		[Required(ErrorMessage = Constantes.CampoRequerido)]
		public int IdPersona { get; set; }
		[Required(ErrorMessage = Constantes.CampoRequerido)]
		public string Actividad { get; set; }=string.Empty;
		[Required(ErrorMessage = Constantes.CampoRequerido)]
		public string Resultado { get; set; }=string.Empty;
		public DateOnly Fecha { get; set; }
		public TimeOnly Hora { get; set; }
	}
}
