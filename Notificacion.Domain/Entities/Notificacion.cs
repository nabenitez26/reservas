using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notificacion.Domain.Entities
{
    public class Notificacion
    {
        public int notificacionid { get; set; }
        public int usuarioid { get; set; }
        public string tipo { get; set; }
        public string mensaje { get; set; }
        public DateTime fechaenvio { get; set; }
        public bool enviado { get; set; } = false;
        public DateTime fechacreacion { get; set; } = DateTime.UtcNow;
    }
}
