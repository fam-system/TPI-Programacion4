using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }

        
        public string Nombre { get; set; }

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }
    }
}
