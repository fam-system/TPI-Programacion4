using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Proceso
    {
        [Key]
        public int Id { get; set; }

        
        public string Nombre { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int CantidadProducto { get; set; }

        
        public string EstadoProceso { get; set; }

        
        public DateTime FechaEntrega { get; set; }

        
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }
    }
}