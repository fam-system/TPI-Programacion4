
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; }

        public ICollection<Proceso> Procesos { get; set; } = new List<Proceso>();

        public ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();
    }
}