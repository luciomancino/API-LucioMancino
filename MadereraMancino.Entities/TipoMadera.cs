using MadereraMancino.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Entities
{
    public class TipoMadera : IEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        #region gettersAndSetters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío o ser nulo.");
            }
            Nombre = nombre;
        }
        #endregion
    }

}
