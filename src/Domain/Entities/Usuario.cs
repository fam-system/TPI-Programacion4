using Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        
        public string Nombre { get; set; }

        
        public string Apellido { get; set; }

        
        public string Dni { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaIngreso { get; set; }

        
        public string NombreUsuario { get; set; }

        public string PasswordHash { get; set; }

        public EstadoEmpleado Estado { get; set; }
        public Rol Rol { get; set; }
    }
}
