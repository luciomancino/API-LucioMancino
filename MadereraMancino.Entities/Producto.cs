using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class Producto : IEntidad
    {
        public Producto()
        {
            CategoriasPorProducto = new HashSet<CategoriaPorProducto>();
            ProveedoresPorProducto = new HashSet<ProveedorPorProducto>();
            DetallesVenta = new HashSet<DetalleVenta>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        [ForeignKey(nameof(TipoMadera))]
        public int IdTipoMadera { get; set; }
        public virtual TipoMadera TipoMadera { get; set; }

        public virtual ICollection<CategoriaPorProducto> CategoriasPorProducto { get; set; }
        public virtual ICollection<ProveedorPorProducto> ProveedoresPorProducto { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
