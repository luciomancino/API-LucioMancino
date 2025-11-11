using MadereraMancino.Entities;
using MadereraMancino.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MadereraMancino.DataAccess
{
    public class DbDataAccess : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    if (!optionsBuilder.IsConfigured)
        //    {//PONER DATABASE STRING
        //        optionsBuilder
        //            //.UseSqlServer("")
        //            .LogTo(Console.WriteLine)
        //            .EnableDetailedErrors();
        //    }
        //}
    }
}