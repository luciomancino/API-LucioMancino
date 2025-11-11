using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class Cliente : IEntidad
    {
        public Cliente()
        {
            Ventas = new HashSet<Venta>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }

        #region gettersAndSetters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío o ser nulo.");
            }
            Nombre = nombre;
        }
        public void SetDireccion(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion))
            {
                throw new ArgumentException("La dirección no puede estar vacía o ser nula.");
            }
            Direccion = direccion;
        }
        public void SetTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
            {
                throw new ArgumentException("El teléfono no puede estar vacío o ser nulo.");
            }
            Telefono = telefono;
        }
        #endregion
    }
}
