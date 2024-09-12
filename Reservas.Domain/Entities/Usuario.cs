using System.ComponentModel.DataAnnotations;

namespace Reservas.Domain.Entities
{
    public class Usuario
    {

        public int usuarioid { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string nombre { get; set; }
        public string apellido { get; set; }
        [EmailAddress]
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string email { get; set; }
        [Required]
        public string contrasenahash { get; set; }
        public DateTime fechacreacion { get; set; } = DateTime.UtcNow;
        public bool activo { get; set; } = true;
        [Required]
        [Range(1, 2, ErrorMessage = "El rol debe estar entre 1 y 2.")]
        public int rolid { get; set; }
    }

}
