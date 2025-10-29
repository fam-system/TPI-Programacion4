using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProcesoCreateDTO
    {
        public string Nombre { get; set; }
        
        public int CantidadProducto { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int ProductoId { get; set; }
    }
}
