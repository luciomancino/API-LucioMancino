using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class ProveedorPorProducto : IEntidad
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Producto))]
        public int IdProducto { get; set; }
        public virtual Producto Producto { get; set; }

        [ForeignKey(nameof(Proveedor))]
        public int IdProveedor { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
