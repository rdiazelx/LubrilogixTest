using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace LubrilogixTest.Models;

public partial class LubrilogixDbContext : DbContext
{
    public LubrilogixDbContext()
    {
    }


    public LubrilogixDbContext(DbContextOptions<LubrilogixDbContext> options, IConfiguration configuration)
    : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Operacione> Operaciones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }




    private readonly IConfiguration _configuration;

    public LubrilogixDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }


    #region Model Builder
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.TnIdOrden).HasName("PK__Inventar__5EA717080ECA917C");

            entity.ToTable("Inventario");

            entity.Property(e => e.TnIdOrden)
                .ValueGeneratedNever()
                .HasColumnName("TN_IdOrden");
            entity.Property(e => e.TNDescuento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("T_N_Descuento");
            entity.Property(e => e.TfFecha).HasColumnName("TF_Fecha");
            entity.Property(e => e.TnCantidad).HasColumnName("TN_Cantidad");
            entity.Property(e => e.TnIdProducto).HasColumnName("TN_IdProducto");
            entity.Property(e => e.TnIdProveedor).HasColumnName("TN_IdProveedor");
            entity.Property(e => e.TnIdsucursal).HasColumnName("TN_IDSucursal");
            entity.Property(e => e.TnIdtipoOperacion).HasColumnName("TN_IDTipoOperacion");
            entity.Property(e => e.TnPrecio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TN_Precio");
            entity.Property(e => e.TnTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TN_Total");

            entity.HasOne(d => d.TnIdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.TnIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__TN_Id__5165187F");

            entity.HasOne(d => d.TnIdProveedorNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.TnIdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__TN_Id__52593CB8");

            entity.HasOne(d => d.TnIdsucursalNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.TnIdsucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__TN_ID__534D60F1");

            entity.HasOne(d => d.TnIdtipoOperacionNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.TnIdtipoOperacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__TN_ID__5441852A");
        });

        modelBuilder.Entity<Operacione>(entity =>
        {
            entity.HasKey(e => e.TnIdtipoOperacion).HasName("PK__Operacio__7321F872BB5451E0");

            entity.Property(e => e.TnIdtipoOperacion).HasColumnName("TN_IDTipoOperacion");
            entity.Property(e => e.TcNombreOperacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_NombreOperacion");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.TnIdProducto).HasName("PK__Producto__74F248D98702D11A");

            entity.Property(e => e.TnIdProducto).HasColumnName("TN_IdProducto");
            entity.Property(e => e.TcCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Categoria");
            entity.Property(e => e.TcNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TC_Nombre");
            entity.Property(e => e.TcSubcategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Subcategoria");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.TnIdProveedor).HasName("PK__Proveedo__B72A1EAA814B6E0D");

            entity.Property(e => e.TnIdProveedor).HasColumnName("TN_IdProveedor");
            entity.Property(e => e.TcDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TC_Direccion");
            entity.Property(e => e.TcEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TC_Email");
            entity.Property(e => e.TcEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Estado");
            entity.Property(e => e.TcNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TC_Nombre");
            entity.Property(e => e.TcProvincia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Provincia");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.TnIdSucursal).HasName("PK__Sucursal__051BE04C2C31C2BA");

            entity.Property(e => e.TnIdSucursal).HasColumnName("TN_IdSucursal");
            entity.Property(e => e.TcComentarios)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Comentarios");
            entity.Property(e => e.TcCorreo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TC_Correo");
            entity.Property(e => e.TcDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TC_Direccion");
            entity.Property(e => e.TcEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Estado");
            entity.Property(e => e.TcNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TC_Nombre");
            entity.Property(e => e.TcProvincia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Provincia");
            entity.Property(e => e.TcTelefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TC_Telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }
     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    #endregion Model Builder


    #region StoredProcedures


    #region Read Data Procedures



    #region Read data Productos
    public async Task<List<Producto>> GetProductosAsync()
    {
        var codErrorParam = new SqlParameter("@cod_error", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        var msgErrorParam = new SqlParameter("@msg_error", SqlDbType.VarChar, 1000)
        {
            Direction = ParameterDirection.Output
        };


        var productos = await Productos
            .FromSqlRaw("EXEC spLeerProductos @nombre = NULL, @categoria = NULL, @subcategoria = NULL, @cod_error = @cod_error OUTPUT, @msg_error = @msg_error OUTPUT",
                codErrorParam, msgErrorParam)
            .ToListAsync();

        // You can access the error code and message here if needed
        int codError = (int)codErrorParam.Value;
        string msgError = (string)msgErrorParam.Value;

        // Optionally, handle errors based on codError and msgError
        if (codError != 0)
        {
            throw new Exception(msgError);
        }

        return productos;
    }
    #endregion

    #region Read Data Proveedores

    public async Task<List<Proveedore>> GetProveedoresAsync()
    {
        var codErrorParam = new SqlParameter("@cod_error", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        var msgErrorParam = new SqlParameter("@msg_error", SqlDbType.VarChar, 1000)
        {
            Direction = ParameterDirection.Output
        };


        var proveedores = await Proveedores
            .FromSqlRaw("EXEC spLeerProveedores @cod_error = @cod_error OUTPUT, @msg_error = @msg_error OUTPUT",
                codErrorParam, msgErrorParam)
            .ToListAsync();

        // You can access the error code and message here if needed
        int codError = (int)codErrorParam.Value;
        string msgError = (string)msgErrorParam.Value;

        // Optionally, handle errors based on codError and msgError
        if (codError != 0)
        {
            throw new Exception(msgError);
        }

        return proveedores;

    }

    #endregion
       
    #region Read Sucursales
    public async Task<List<Sucursale>> GetSucursalesAsync()
    {
        var codErrorParam = new SqlParameter("@cod_error", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        var msgErrorParam = new SqlParameter("@msg_error", SqlDbType.VarChar, 1000)
        {
            Direction = ParameterDirection.Output
        };


        var sucursales = await Sucursales
            .FromSqlRaw("EXEC spLeerSucursales @cod_error = @cod_error OUTPUT, @msg_error = @msg_error OUTPUT",
                codErrorParam, msgErrorParam)
            .ToListAsync();

        // You can access the error code and message here if needed
        int codError = (int)codErrorParam.Value;
        string msgError = (string)msgErrorParam.Value;

        // Optionally, handle errors based on codError and msgError
        if (codError != 0)
        {
            throw new Exception(msgError);
        }

        return sucursales;

    }

    #endregion

    #region Read data Inventario

    public async Task<List<Inventario>> GetInventarioAsync()
    {
        var codErrorParam = new SqlParameter("@cod_error", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        var msgErrorParam = new SqlParameter("@msg_error", SqlDbType.VarChar, 1000)
        {
            Direction = ParameterDirection.Output
        };

        var inventario = await Inventarios
            .FromSqlRaw("EXEC spLeerInventario @cod_error = @cod_error OUTPUT, @msg_error = @msg_error OUTPUT",
                codErrorParam, msgErrorParam)
            .ToListAsync();

        // Handle error codes if necessary
        int codError = (int)codErrorParam.Value;
        string msgError = (string)msgErrorParam.Value;

        if (codError != 0)
        {
            throw new Exception(msgError);
        }

        return inventario;
    }



    #endregion



    #endregion Read
















    #endregion

}