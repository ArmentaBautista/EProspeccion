using System.ComponentModel.DataAnnotations;

namespace Entidades.Generales
{
    public class Persona:EntidadBase
    {
		[Required]
        public string Nombre { get; set; }
		[Required]
        public string ApellidoPaterno { get; set; }
		[Required]
		public string ApellidoMaterno { get; set; }
		[Required]
		public DateOnly FechaNacimiento {  get; set; }
		public string Rfc { get; set; } = string.Empty;
		[Required]
        public string DireccionDomicilio { get; set; }
		public string DireccionTrabajo { get; set; } = string.Empty ;
		[Required]
		public string TelefonoCelular { get; set; }
		public string TelefonoCasa {  get; set; } = string.Empty;
		public string TelefonoTrabajo { get; set; } = string.Empty;		
		public string TelefonoContacto { get; set; } = string.Empty;
	}
}
