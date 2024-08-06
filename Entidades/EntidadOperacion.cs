using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Entidades
{
	public class EntidadOperacion
	{
		public int IdTipoOperacion { get; set; }
		[Required]
		public string Concepto { get; set; } = string.Empty;

	}
}
