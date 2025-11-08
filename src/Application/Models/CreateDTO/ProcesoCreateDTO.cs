using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CreateDTO
{
    public class ProcesoCreateDTO
    {
        [Required(ErrorMessage = "El nombre del proceso es obligatorio")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        public int CantidadProducto { get; set; }

        [Required]
        public DateTime? FechaEntrega { get; set; }
        public int ProductoId { get; set; }
    }
}
