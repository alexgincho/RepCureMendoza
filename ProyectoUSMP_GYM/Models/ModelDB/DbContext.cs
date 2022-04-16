using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
        {
        }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Detalleventum> Detalleventa { get; set; }
        public virtual DbSet<Distrito> Distritos { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuRol> MenuRols { get; set; }
        public virtual DbSet<Metodopago> Metodopagos { get; set; }
        public virtual DbSet<Personaladm> Personaladms { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<Provincium> Provincia { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Usuariologin> Usuariologins { get; set; }
        public virtual DbSet<Usuariomembresium> Usuariomembresia { get; set; }
        public virtual DbSet<Ventum> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ec2-44-198-223-154.compute-1.amazonaws.com;Database=d4pgbm8tk0rt86;Username=bozijzuypldcmm;Password=dbd1b10f36f5e55700195cc2d3791f058e70fedf930ee9ddd7c52b7d5662206b;Port=5432;SSL Mode=Require;Trust Server Certificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.PkCategoria)
                    .HasName("categoria_pkey");

                entity.ToTable("categoria");

                entity.Property(e => e.PkCategoria).HasColumnName("pk_categoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.PkDepartamento)
                    .HasName("departamento_pkey");

                entity.ToTable("departamento");

                entity.Property(e => e.PkDepartamento).HasColumnName("pk_departamento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Detalleventum>(entity =>
            {
                entity.HasKey(e => e.PkDetalle)
                    .HasName("detalleventa_pkey");

                entity.ToTable("detalleventa");

                entity.Property(e => e.PkDetalle).HasColumnName("pk_detalle");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Descuento).HasColumnName("descuento");

                entity.Property(e => e.FkProducto).HasColumnName("fk_producto");

                entity.Property(e => e.FkVenta).HasColumnName("fk_venta");

                entity.Property(e => e.Preciounitario).HasColumnName("preciounitario");

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");

                entity.HasOne(d => d.FkProductoNavigation)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.FkProducto)
                    .HasConstraintName("fk_producto");

                entity.HasOne(d => d.FkVentaNavigation)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.FkVenta)
                    .HasConstraintName("fk_venta");
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.HasKey(e => e.PkDistrito)
                    .HasName("distrito_pkey");

                entity.ToTable("distrito");

                entity.Property(e => e.PkDistrito).HasColumnName("pk_distrito");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FkProvincia).HasColumnName("fk_provincia");

                entity.HasOne(d => d.FkProvinciaNavigation)
                    .WithMany(p => p.Distritos)
                    .HasForeignKey(d => d.FkProvincia)
                    .HasConstraintName("fk_provincia");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.PkMenu)
                    .HasName("menu_pkey");

                entity.ToTable("menu");

                entity.Property(e => e.PkMenu).HasColumnName("pk_menu");

                entity.Property(e => e.Actions)
                    .HasMaxLength(100)
                    .HasColumnName("actions");

                entity.Property(e => e.Controller)
                    .HasMaxLength(100)
                    .HasColumnName("controller");

                entity.Property(e => e.FkMenupadre).HasColumnName("fk_menupadre");

                entity.Property(e => e.Icons)
                    .HasMaxLength(50)
                    .HasColumnName("icons");

                entity.Property(e => e.Menu1)
                    .HasMaxLength(100)
                    .HasColumnName("menu");

                entity.Property(e => e.Ordenmenu).HasColumnName("ordenmenu");

                entity.Property(e => e.Tipomenu).HasColumnName("tipomenu");
            });

            modelBuilder.Entity<MenuRol>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_rol");

                entity.Property(e => e.FkMenu).HasColumnName("fk_menu");

                entity.Property(e => e.FkRol).HasColumnName("fk_rol");

                entity.HasOne(d => d.FkMenuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.FkMenu)
                    .HasConstraintName("fk_menu");

                entity.HasOne(d => d.FkRolNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.FkRol)
                    .HasConstraintName("fk_rol");
            });

            modelBuilder.Entity<Metodopago>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("metodopago");

                entity.Property(e => e.Fechaexpiraciontar)
                    .HasColumnType("character varying")
                    .HasColumnName("fechaexpiraciontar");

                entity.Property(e => e.Numeroccv)
                    .HasColumnType("character varying")
                    .HasColumnName("numeroccv");

                entity.Property(e => e.Numerotarjeta)
                    .HasColumnType("character varying")
                    .HasColumnName("numerotarjeta");

                entity.Property(e => e.Pkmetodopago)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("pkmetodopago");

                entity.Property(e => e.Propietario)
                    .HasColumnType("character varying")
                    .HasColumnName("propietario");

                entity.Property(e => e.Tipotarjeta).HasColumnName("tipotarjeta");
            });

            modelBuilder.Entity<Personaladm>(entity =>
            {
                entity.HasKey(e => e.PkPersonal)
                    .HasName("personaladm_pkey");

                entity.ToTable("personaladm");

                entity.Property(e => e.PkPersonal).HasColumnName("pk_personal");

                entity.Property(e => e.Apellidomaterno)
                    .HasMaxLength(100)
                    .HasColumnName("apellidomaterno");

                entity.Property(e => e.Apellidopaterno)
                    .HasMaxLength(100)
                    .HasColumnName("apellidopaterno");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .HasColumnName("direccion");

                entity.Property(e => e.Dni)
                    .HasMaxLength(8)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Fechacrea)
                    .HasColumnType("date")
                    .HasColumnName("fechacrea");

                entity.Property(e => e.Fechaedita)
                    .HasColumnType("date")
                    .HasColumnName("fechaedita");

                entity.Property(e => e.FkPersonalcrea).HasColumnName("fk_personalcrea");

                entity.Property(e => e.FkPersonaledita).HasColumnName("fk_personaledita");

                entity.Property(e => e.FkRol).HasColumnName("fk_rol");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Passwords)
                    .HasMaxLength(255)
                    .HasColumnName("passwords");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .HasColumnName("telefono");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.FkRolNavigation)
                    .WithMany(p => p.Personaladms)
                    .HasForeignKey(d => d.FkRol)
                    .HasConstraintName("fk_rol");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.PkProducto)
                    .HasName("producto_pkey");

                entity.ToTable("producto");

                entity.Property(e => e.PkProducto).HasColumnName("pk_producto");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(100)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Descuento).HasColumnName("descuento");

                entity.Property(e => e.Fechacrea)
                    .HasColumnType("date")
                    .HasColumnName("fechacrea");

                entity.Property(e => e.Fechaedita)
                    .HasColumnType("date")
                    .HasColumnName("fechaedita");

                entity.Property(e => e.Fechavencimiento)
                    .HasColumnType("date")
                    .HasColumnName("fechavencimiento");

                entity.Property(e => e.FkCategoria).HasColumnName("fk_categoria");

                entity.Property(e => e.FkPersonalcrea).HasColumnName("fk_personalcrea");

                entity.Property(e => e.FkPersonaledita).HasColumnName("fk_personaledita");

                entity.Property(e => e.FkProveedor).HasColumnName("fk_proveedor");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("imagen");

                entity.Property(e => e.Isdelete).HasColumnName("isdelete");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Preciocompra).HasColumnName("preciocompra");

                entity.Property(e => e.Precioventa).HasColumnName("precioventa");

                entity.HasOne(d => d.FkCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkCategoria)
                    .HasConstraintName("fk_categoria");

                entity.HasOne(d => d.FkProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkProveedor)
                    .HasConstraintName("fk_proveedor");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.PkProveedor)
                    .HasName("proveedor_pkey");

                entity.ToTable("proveedor");

                entity.Property(e => e.PkProveedor).HasColumnName("pk_proveedor");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Fechacrea)
                    .HasColumnType("date")
                    .HasColumnName("fechacrea");

                entity.Property(e => e.Fechaedita)
                    .HasColumnType("date")
                    .HasColumnName("fechaedita");

                entity.Property(e => e.FkDistrito).HasColumnName("fk_distrito");

                entity.Property(e => e.FkPersonalcrea).HasColumnName("fk_personalcrea");

                entity.Property(e => e.FkPersonaledita).HasColumnName("fk_personaledita");

                entity.Property(e => e.Isdelete).HasColumnName("isdelete");

                entity.Property(e => e.Razonsocial)
                    .HasMaxLength(150)
                    .HasColumnName("razonsocial");

                entity.Property(e => e.Ruc)
                    .HasMaxLength(100)
                    .HasColumnName("ruc");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.HasKey(e => e.PkProvincia)
                    .HasName("provincia_pkey");

                entity.ToTable("provincia");

                entity.Property(e => e.PkProvincia).HasColumnName("pk_provincia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FkDepartamento).HasColumnName("fk_departamento");

                entity.HasOne(d => d.FkDepartamentoNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.FkDepartamento)
                    .HasConstraintName("fk_departamento");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.PkErol)
                    .HasName("roles_pkey");

                entity.ToTable("roles");

                entity.Property(e => e.PkErol).HasColumnName("pk_erol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.PkUsuario)
                    .HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.Property(e => e.PkUsuario).HasColumnName("pk_usuario");

                entity.Property(e => e.Apellidomaterno)
                    .HasMaxLength(100)
                    .HasColumnName("apellidomaterno");

                entity.Property(e => e.Apellidopaterno)
                    .HasMaxLength(100)
                    .HasColumnName("apellidopaterno");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .HasColumnName("direccion");

                entity.Property(e => e.Dni)
                    .HasMaxLength(8)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Fechacrea)
                    .HasColumnType("date")
                    .HasColumnName("fechacrea");

                entity.Property(e => e.Fechaedita)
                    .HasColumnType("date")
                    .HasColumnName("fechaedita");

                entity.Property(e => e.FkPersonalcrea).HasColumnName("fk_personalcrea");

                entity.Property(e => e.FkPersonaledita).HasColumnName("fk_personaledita");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .HasColumnName("telefono");

                entity.Property(e => e.Tipousuario).HasColumnName("tipousuario");

                entity.Property(e => e.Userweb).HasColumnName("userweb");
            });

            modelBuilder.Entity<Usuariologin>(entity =>
            {
                entity.HasKey(e => e.PkUsuariologin)
                    .HasName("usuariologin_pkey");

                entity.ToTable("usuariologin");

                entity.Property(e => e.PkUsuariologin).HasColumnName("pk_usuariologin");

                entity.Property(e => e.FkUsuario).HasColumnName("fk_usuario");

                entity.Property(e => e.Passwords)
                    .HasMaxLength(255)
                    .HasColumnName("passwords");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.Usuariologins)
                    .HasForeignKey(d => d.FkUsuario)
                    .HasConstraintName("fk_usuario");
            });

            modelBuilder.Entity<Usuariomembresium>(entity =>
            {
                entity.HasKey(e => e.PkMembresia)
                    .HasName("usuariomembresia_pkey");

                entity.ToTable("usuariomembresia");

                entity.Property(e => e.PkMembresia).HasColumnName("pk_membresia");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechacrea)
                    .HasColumnType("date")
                    .HasColumnName("fechacrea");

                entity.Property(e => e.Fechaedita)
                    .HasColumnType("date")
                    .HasColumnName("fechaedita");

                entity.Property(e => e.Fechafinal)
                    .HasColumnType("date")
                    .HasColumnName("fechafinal");

                entity.Property(e => e.FkPersonalcrea).HasColumnName("fk_personalcrea");

                entity.Property(e => e.FkPersonaledita).HasColumnName("fk_personaledita");

                entity.Property(e => e.FkUsuario).HasColumnName("fk_usuario");

                entity.Property(e => e.Isdelete).HasColumnName("isdelete");

                entity.HasOne(d => d.FkPersonalcreaNavigation)
                    .WithMany(p => p.Usuariomembresia)
                    .HasForeignKey(d => d.FkPersonalcrea)
                    .HasConstraintName("fk_personalcrea");

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.Usuariomembresia)
                    .HasForeignKey(d => d.FkUsuario)
                    .HasConstraintName("fk_usuario");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.PkVenta)
                    .HasName("venta_pkey");

                entity.ToTable("venta");

                entity.Property(e => e.PkVenta).HasColumnName("pk_venta");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(100)
                    .HasColumnName("codigo");

                entity.Property(e => e.Delivery).HasColumnName("delivery");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechacrea)
                    .HasColumnType("date")
                    .HasColumnName("fechacrea");

                entity.Property(e => e.Fechaedita)
                    .HasColumnType("date")
                    .HasColumnName("fechaedita");

                entity.Property(e => e.Fechaentrega)
                    .HasColumnType("date")
                    .HasColumnName("fechaentrega");

                entity.Property(e => e.FkDistrito).HasColumnName("fk_distrito");

                entity.Property(e => e.FkPersonalcrea).HasColumnName("fk_personalcrea");

                entity.Property(e => e.FkPersonaledita).HasColumnName("fk_personaledita");

                entity.Property(e => e.FkUsuario).HasColumnName("fk_usuario");

                entity.Property(e => e.Isdelete).HasColumnName("isdelete");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(255)
                    .HasColumnName("observacion");

                entity.Property(e => e.Totalventa).HasColumnName("totalventa");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
