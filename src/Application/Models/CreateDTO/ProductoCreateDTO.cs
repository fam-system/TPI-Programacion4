using System.ComponentModel.DataAnnotations;

namespace Application.Models.CreateDTO
{
    public class ProductoCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; }
    }
}