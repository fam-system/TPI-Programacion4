using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UsuarioCreateDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Estado { get; set; }
    }
}
