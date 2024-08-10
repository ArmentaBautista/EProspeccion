using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Request;
public class LoginDtoRequest
{
	[Required(ErrorMessage = Constantes.CampoRequerido)]
	public string Usuario { get; set; } = default!;

	[Required(ErrorMessage = Constantes.CampoRequerido)]
	public string Password { get; set; } = default!;
}
