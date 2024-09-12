using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.Domain.Entities
{
    public class DetalleReserva
    {
        public int detallereservaid { get; set; }
        public int reservaid { get; set; }
        public string servicio { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
    }
}
