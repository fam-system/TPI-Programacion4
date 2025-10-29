namespace Application.Models
{
    public class ArchivoCreateDTO
    {
        public string Nombre { get; set; }
        public int ProductoId { get; set; } // Solo referencia el producto
    }
}