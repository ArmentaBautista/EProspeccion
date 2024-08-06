using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Sistema
{
	public static class InicializadorUsuarios
	{
		public static async Task Crear(IServiceProvider service)
		{
			// UserManager (repositorio de usuarios)
			var userManager = service.GetRequiredService<UserManager<EIdentity>>();

			// RoleManager (repositorio de roles)
			var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

			// Crear roles
			var adminRole = new IdentityRole("Administrador");
			var clienteRole = new IdentityRole("Gestor");

			await roleManager.CreateAsync(adminRole);
			await roleManager.CreateAsync(clienteRole);

			// Usuario Administrador
			var adminUser = new EIdentity
			{
				NombreCompleto = "Administrador del Sistema",
				UserName = "admin",
				Email = "carlos.armenta@intelix.mx",
				EmailConfirmed = true
			};

			var result = await userManager.CreateAsync(adminUser, "qqQQ11!!");
			if (result.Succeeded)
			{
				// Si se crea correctamente el usuario, asignamos el rol de administrador
				await userManager.AddToRoleAsync(adminUser, "Administrador");
			}
		}
	}
}
