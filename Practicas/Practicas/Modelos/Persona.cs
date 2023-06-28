using System.ComponentModel.DataAnnotations;

namespace Practicas.Modelos
{
    public class Persona
    {
        public int id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string name { get; set; }

        [Required]
        [MaxLength(30)]
        public string apellidoPaterno { get; set; }

        [Required]
        [MaxLength(30)]
        public string apellidoMaterno { get; set; }
        public int edad { get; set; }
        public double salario { get; set; }
    }
}
