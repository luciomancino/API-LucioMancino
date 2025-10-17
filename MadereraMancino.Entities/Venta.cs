using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class Venta : IEntidad
    {
        public Venta()
        {
            DetallesVenta = new HashSet<DetalleVenta>();
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        [ForeignKey(nameof(Empleado))]
        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }

        public decimal Total { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
