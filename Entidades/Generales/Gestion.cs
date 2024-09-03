using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Generales
{
	public class Gestion:EntidadBase
	{
		
		public int IdPersona {  get; set; }
		public Persona Persona {  get; set; }
		public string Actividad {  get; set; }
		public string Resultado { get; set; }
		public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
