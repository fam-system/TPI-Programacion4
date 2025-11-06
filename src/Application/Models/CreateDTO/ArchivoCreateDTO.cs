using System.ComponentModel.DataAnnotations;

namespace Application.Models.CreateDTO
{
    public class ArchivoCreateDTO
    {
        [Required(ErrorMessage = "El nombre del archivo es obligatorio")]
        [MaxLength(150)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int ProductoId { get; set; } // Solo referencia el producto
    }
}