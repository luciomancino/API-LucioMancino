using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class CategoriaPorProducto : IEntidad
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Producto))]
        public int IdProducto { get; set; }
        public virtual Producto Producto { get; set; }

        [ForeignKey(nameof(Categoria))]
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
