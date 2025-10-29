using Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

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

        public DateTime FechaIngreso { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public EstadoEmpleado Estado { get; set; }
        public Rol Rol { get; set; }
    }
}
