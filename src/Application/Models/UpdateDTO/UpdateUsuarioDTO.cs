using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UpdateUsuarioDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 7)]
        public string Dni { get; set; }
        [MaxLength(100)]
        public string Direccion { get; set; }
        [Phone]
        public string Telefono { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Rol { get; set; }
        public DateTime FechaIngreso { get; set; }
        [Required]
        public string Estado { get; set; }
    }
}
