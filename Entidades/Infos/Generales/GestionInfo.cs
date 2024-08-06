using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Infos.Generales
{
	public class GestionInfo
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Actividad {  get; set; }
		public string Respuesta { get; set; }
		public DateOnly Fecha { get; set; }
		public TimeOnly Hora {  get; set; } 
	}
}
