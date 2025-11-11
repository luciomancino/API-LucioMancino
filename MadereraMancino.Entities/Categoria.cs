using MadereraMancino.Abstractions;

namespace MadereraMancino.Entities
{
    public class Categoria : IEntidad
    {
        public Categoria()
        {
            CategoriasPorProducto = new HashSet<CategoriaPorProducto>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<CategoriaPorProducto> CategoriasPorProducto { get; set; }

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
