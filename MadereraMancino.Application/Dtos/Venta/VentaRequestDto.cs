using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Application.Dtos.Venta
{
    public class VentaRequestDto
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }
    }
}
