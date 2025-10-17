using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class Proveedor : IEntidad
    {
        public Proveedor()
        {
            ProveedoresPorProducto = new HashSet<ProveedorPorProducto>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public virtual ICollection<ProveedorPorProducto> ProveedoresPorProducto { get; set; }
    }
}
