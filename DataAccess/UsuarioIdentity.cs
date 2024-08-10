using Dto;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
	public class UsuarioIdentity:IdentityUser
	{
		public string Nombre { get; set; } = default!;

		public string ApellidoPaterno { get; set; } = default!;

		public string ApellidoMaterno { get; set; } = default!;

	}
}
