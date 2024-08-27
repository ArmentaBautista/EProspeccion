using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Entidades.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuraciones
{
	public class PersonaConfiguracion : IEntityTypeConfiguration<Persona>
	{
		public void Configure(EntityTypeBuilder<Persona> builder)
		{
			builder.HasMany<Gestion>()
				.WithOne("Persona")
				.HasForeignKey(x=>x.IdPersona);
			builder.Property(p => p.Rfc)
				.HasMaxLength(13);
		}
	}
}
