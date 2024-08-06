using Microsoft.AspNetCore.Identity;

namespace DataAccess
{
	public class EIdentity:IdentityUser
	{
		public string NombreCompleto { get; set; } = default!;
	}
}
