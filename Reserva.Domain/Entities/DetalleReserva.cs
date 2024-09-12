using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Domain.Entities
{
    public class DetalleReserva
    {
        public int detallereservaid { get; set; }
        public int reservaid { get; set; }
        [Required]
        public string servicio { get; set; }
        public decimal precio { get; set; }
        [Required]
        public int cantidad { get; set; }
    }
}
