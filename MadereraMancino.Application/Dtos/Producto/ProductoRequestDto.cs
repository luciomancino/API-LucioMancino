using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Application.Dtos.Producto
{
    public class ProductoRequestDto
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
        public int IdTipoMadera { get; set; }
    }
}
