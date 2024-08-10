using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Request
{
	public class RegistrarUsuarioDto
	{
		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "Nombre")]
		public string Nombre { get; set; } = default!;

		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "Apellido Paterno")]
		public string ApellidoPaterno { get; set; } = default!;

		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "Apellido Materno")]
		public string ApellidoMaterno { get; set; } = default!;


		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "Nombre de usuario")]
		public string NombreUsuario { get; set; } = default!;

		[EmailAddress]
		[Required(ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "Email")]
		public string Email { get; set; } = default!;

		
		[Required(ErrorMessage = Constantes.CampoRequerido)]
		public string Password { get; set; } = default!;

		
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; } = default!;
	}
}
