using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
	public class EntidadBase
	{
		[Key]
		public int Id { get; set; }
		public DateOnly Fecha { get; set; }
		public TimeOnly Hora { get; set; }
		public DateTime Alta {  get; set; }= DateTime.Now;
		[Required]
		public short IdEstatus { get; set; }= 1;
	}
}
