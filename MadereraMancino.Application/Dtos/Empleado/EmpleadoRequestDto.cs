using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Application.Dtos.Empleado
{
    public class EmpleadoRequestDto
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; set; }
        [StringLength(30)]
        public string Apellido { get; set; }

    }
}
