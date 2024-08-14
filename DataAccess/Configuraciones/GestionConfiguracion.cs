using Entidades.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuraciones
{
	public class GestionConfiguracion : IEntityTypeConfiguration<Gestion>
	{
		public void Configure(EntityTypeBuilder<Gestion> builder)
		{
			builder.HasOne<Persona>(p => p.Persona).WithOne().HasForeignKey<Persona>(p => p.Id);
			builder.Property(p => p.Actividad)
				.HasMaxLength(100);
			builder.Property(p => p.Resultado)
				.HasMaxLength(100);
		}
	}
}
