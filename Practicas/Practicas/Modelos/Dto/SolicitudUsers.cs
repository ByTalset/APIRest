using System.ComponentModel.DataAnnotations;

namespace Practicas.Modelos.Dto
{
    public class SolicitudUsers
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
        
        [Required]
        public string roll { get; set; }
    }
}
