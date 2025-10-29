using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProcesoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int CantidadProducto { get; set; }
        public string EstadoProceso { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int ProductoId { get; set; }
    }
}
