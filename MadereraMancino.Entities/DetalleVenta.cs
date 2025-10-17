using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class DetalleVenta : IEntidad
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Venta))]
        public int IdVenta { get; set; }
        public virtual Venta Venta { get; set; }

        [ForeignKey(nameof(Producto))]
        public int IdProducto { get; set; }
        public virtual Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
