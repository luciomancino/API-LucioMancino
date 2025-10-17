using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Application.Dtos.Venta
{
    public class VentaResponseDto
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }
    }
}
