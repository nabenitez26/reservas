using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.Domain.Entities
{
    public class Reserva
    {
        public int reservaid { get; set; }
        public int usuarioid { get; set; }
        public DateTime fechareserva { get; set; }
        public string estado { get; set; }
        public DateTime fechacreacion { get; set; } = DateTime.UtcNow;
        public DateTime fechamodificacion { get; set; } = DateTime.UtcNow;
    }
}
