using Entidades.Generales;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.RegularExpressions;
using Entidades;

namespace DataAccess
{
	public class EPDbContext : IdentityDbContext<UsuarioIdentity>
	{

		public EPDbContext(DbContextOptions<EPDbContext> options)
	   : base(options)
		{
		}

		public DbSet<Persona> Personas { get; set; } = default!;
		public DbSet<Gestion> Gestiones { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// aplicamos una configuracion personalizada de entidades
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			base.ConfigureConventions(configurationBuilder);

			// Aqui le digo que todas las propiedades de entidades que sean string solo deben
			// tener 100 caracteres como maximo.
			//configurationBuilder.Properties<string>().HaveMaxLength(100);
		}

	}
}
