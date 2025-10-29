using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        // FK a Producto
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }
    }
}
