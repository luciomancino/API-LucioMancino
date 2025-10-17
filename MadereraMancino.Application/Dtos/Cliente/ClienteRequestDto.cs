using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Application.Dtos.Cliente
{
    public class ClienteRequestDto
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; set; }
        [StringLength(30)]
        public string Telefono { get; set; }
        [StringLength(100)]
        public string Direccion { get; set; }

    }
}
