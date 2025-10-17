using MadereraMancino.Entities;
using Microsoft.EntityFrameworkCore;

namespace MadereraMancino.DataAccess
{
    public class DbDataAccess : DbContext
    {
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<CategoriaPorProducto> CategoriasPorProducto { get; set; }
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<ProveedorPorProducto> ProveedoresPorProducto { get; set; }
        public virtual DbSet<TipoMadera> TiposMadera { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<DetalleVenta> DetallesVenta { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }

        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {//PONER DATABASE STRING
                optionsBuilder
                    //.UseSqlServer("")
                    .LogTo(Console.WriteLine)
                    .EnableDetailedErrors();
            }
        }
    }
}