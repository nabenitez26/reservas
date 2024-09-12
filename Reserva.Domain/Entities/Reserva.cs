using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Domain.Entities
{
    public class Reserva
    {
        public int reservaid { get; set; }
        [Required]
        public int usuarioid { get; set; }
        [Required]
        public DateTime fechareserva { get; set; }
        public string estado { get; set; } = "Neeva";
        public DateTime fechacreacion { get; set; } = DateTime.UtcNow;
        public DateTime fechamodificacion { get; set; } = DateTime.UtcNow;
        public ICollection<DetalleReserva> DetallesReserva { get; set; } = new List<DetalleReserva>();
    }
}
