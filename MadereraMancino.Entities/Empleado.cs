using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class Empleado : IEntidad
    {
        public Empleado()
        {
            Ventas = new HashSet<Venta>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

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
        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("El apellido no puede estar vacío o ser nulo.");
            }
            Apellido = apellido;
        }
        public string GetNombreCompleto()
        {
            return string.Join(", ", Apellido, Nombre);
        }
        #endregion
    }
}
