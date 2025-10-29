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

        [Required(ErrorMessage = "El nombre del proceso es obligatorio")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int CantidadProducto { get; set; }

        [Required]
        [MaxLength(50)]
        public string EstadoProceso { get; set; }

        [Required]
        public DateTime FechaEntrega { get; set; }

        // FK a Producto
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }
    }
}