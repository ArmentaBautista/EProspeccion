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
		public string Nombre { get; set; } = string.Empty; 
		public string Actividad {  get; set; } = string.Empty;
		public string Resultado { get; set; } = string.Empty;
		public DateOnly Fecha { get; set; }
		public TimeOnly Hora {  get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
