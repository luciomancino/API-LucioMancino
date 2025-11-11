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

        #region gettersAndSetters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío o ser nulo.");
            }
            Nombre = nombre;
        }
        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("La descripción no puede estar vacía o ser nula.");
            }
            Descripcion = descripcion;
        }
        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
            {
                throw new ArgumentException("El precio no puede ser negativo.");
            }
            Precio = precio;
        }
        public void SetStock(int stock)
        {
            if (stock < 0)
            {
                throw new ArgumentException("El stock no puede ser negativo.");
            }
            Stock = stock;
        }
        #endregion

    }
}
