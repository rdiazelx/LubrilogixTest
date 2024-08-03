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

        // Add the configuration for spLeerInventario_Result
        modelBuilder.Entity<spLeerInventario_Result>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null); // Specify that this is not a table or view
            entity.Property(e => e.TN_IdOrden).HasColumnName("TN_IdOrden");
            entity.Property(e => e.TF_Fecha).HasColumnName("TF_Fecha");
            entity.Property(e => e.TN_IDSucursal).HasColumnName("TN_IDSucursal");
            entity.Property(e => e.SucursalNombre).HasColumnName("SucursalNombre");
            entity.Property(e => e.TN_IdProducto).HasColumnName("TN_IdProducto");
            entity.Property(e => e.ProductoNombre).HasColumnName("ProductoNombre");
            entity.Property(e => e.TN_Precio).HasColumnName("TN_Precio");
            entity.Property(e => e.TN_Descuento).HasColumnName("T_N_Descuento");
            entity.Property(e => e.TN_IDTipoOperacion).HasColumnName("TN_IDTipoOperacion");
            entity.Property(e => e.TipoOperacionNombre).HasColumnName("TipoOperacionNombre");
            entity.Property(e => e.TN_IdProveedor).HasColumnName("TN_IdProveedor");
            entity.Property(e => e.ProveedorNombre).HasColumnName("ProveedorNombre");
            entity.Property(e => e.TN_Total).HasColumnName("TN_Total");
            entity.Property(e => e.TC_Estado).HasColumnName("TC_Estado");
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

    public async Task<List<spLeerInventario_Result>> spLeerInventario()
    {
        var codErrorParam = new SqlParameter("@cod_error", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        var msgErrorParam = new SqlParameter("@msg_error", SqlDbType.VarChar, 1000)
        {
            Direction = ParameterDirection.Output
        };

        var result = await this.Set<spLeerInventario_Result>()
            .FromSqlRaw("EXEC spLeerInventario @cod_error = @cod_error OUTPUT, @msg_error = @msg_error OUTPUT",
                codErrorParam, msgErrorParam)
            .ToListAsync();

        int codError = (int)codErrorParam.Value;
        string msgError = (string)msgErrorParam.Value;

        if (codError != 0)
        {
            throw new Exception(msgError);
        }

        return result;
    }



    #endregion

    #region
    public async Task UpdateEstadoAsync(int tnIdOrden)
    {
        var tnIdOrdenParam = new SqlParameter("@TN_IdOrden", tnIdOrden);

        try
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC dbo.spActualizarEstadoOrden @TN_IdOrden",
                tnIdOrdenParam
            );
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            throw new Exception($"Error updating order state: {ex.Message}", ex);
        }
    }
    #endregion

    #endregion Read

    #region Written Data  Procedures

    //escribir en Agregar Producto
    public async Task AgregarProductoAsync(Producto producto)
    {
        // Crear el parámetro del nombre del producto y asignar el valor 
        var nombreParam = new SqlParameter("@Nombre", producto.TcNombre);

        // Crear el parámetro de categoría del producto y asignar el valor 
        var categoriaParam = new SqlParameter("@Categoria", producto.TcCategoria);

        // Crear el parámetro de subcategoría del producto y asignar el valor
        var subcategoriaParam = new SqlParameter("@Subcategoria", producto.TcSubcategoria);

        try
        {
            // Ejecutar el procedimiento almacenado 'spAgregarProducto' con los parámetros especificados.
            await Database.ExecuteSqlRawAsync(
                "EXEC dbo.spAgregarProducto @Nombre, @Categoria, @Subcategoria",
                nombreParam, categoriaParam, subcategoriaParam
            );
        }
        catch (Exception ex)
        {

            // Lanza una nueva excepción con un mensaje detallado
            throw new Exception($"Error inserting product: {ex.Message}", ex);
        }
    }
    //escribir en surcusales
    public async Task AgregarSurcursalAsync(Sucursale sucursal)
    {
        // Crear el parámetro del nombre de la sucursal y asignar el valor del objeto 'sucursal'
        var nombreParam = new SqlParameter("@Nombre", sucursal.TcNombre);

        // Crear el parámetro de provincia de la sucursal y asignar el valor del objeto 'sucursal'
        var provinciaParam = new SqlParameter("@Provincia", sucursal.TcProvincia);

        // Crear el parámetro de dirección de la sucursal y asignar el valor del objeto 'sucursal'
        var direccionParam = new SqlParameter("@Direccion", sucursal.TcDireccion);

        // Crear el parámetro de teléfono de la sucursal y asignar el valor del objeto 'sucursal'
        var telefonoParam = new SqlParameter("@Telefono", sucursal.TcTelefono);

        // Crear el parámetro de correo de la sucursal y asignar el valor del objeto 'sucursal'
        var correoParam = new SqlParameter("@Correo", sucursal.TcCorreo);

        // Crear el parámetro de estado de la sucursal y asignar el valor del objeto 'sucursal'
        var estadoParam = new SqlParameter("@Estado", sucursal.TcEstado);

        // Crear el parámetro de comentarios de la sucursal y asignar el valor del objeto 'sucursal'
        var comentariosParam = new SqlParameter("@Comentarios", sucursal.TcComentarios);

        try
        {
            // Ejecutar el procedimiento almacenado 'spAgregarSucursal' de forma asíncrona
            // y pasar los parámetros creados anteriormente
            await Database.ExecuteSqlRawAsync(
                "EXEC dbo.spAgregarSucursal @Nombre, @Provincia, @Direccion, @Telefono, @Correo, @Estado, @Comentarios",
                nombreParam, provinciaParam, direccionParam, telefonoParam, correoParam, estadoParam, comentariosParam
            );
        }
        catch (Exception ex)
        {
            // Lanza una nueva excepción con un mensaje detallado que incluye el mensaje de la excepción original
            throw new Exception($"Error inserting branch: {ex.Message}", ex);
        }
    }
    //escribir en proveedores
    public async Task AgregarProveedorAsync(Proveedore proveedor)
    {
        // Crear el parámetro del nombre del proveedor y asignar el valor 
        var nombreParam = new SqlParameter("@Nombre", proveedor.TcNombre);
        // Crear el parámetro de la provincia del proveedor y asignar el valor 
        var provinciaParam = new SqlParameter("@Provincia", proveedor.TcProvincia);
        // Crear el parámetro de la dirección del proveedor y asignar el valor]
        var direccionParam = new SqlParameter("@Direccion", proveedor.TcDireccion);
        // Crear el parámetro del email del proveedor y asignar el valor 
        var emailParam = new SqlParameter("@Email", proveedor.TcEmail);
        // Crear el parámetro del estado del proveedor y asignar el valor 
        var estadoParam = new SqlParameter("@Estado", proveedor.TcEstado);

        try
        {
            // Ejecutar el procedimiento almacenado 'spAgregarProveedor']
            await Database.ExecuteSqlRawAsync(
                "EXEC dbo.spAgregarProveedor @Nombre, @Direccion, @Provincia, @Email, @Estado",
                nombreParam, direccionParam, provinciaParam, emailParam, estadoParam
            );
        }
        catch (Exception ex)
        {
            // Lanza una nueva excepción con un mensaje 
            throw new Exception($"Error inserting supplier: {ex.Message}", ex);
        }
    }
    #region written registro proveedor 
    public async Task RegistroProveedorAsync(Proveedore proveedor)
    {
        // Establece el estado por defecto a "inactivo"
        proveedor.TcEstado = "inactivo";

        // Crear los parámetros para el procedimiento almacenado
        var nombreParam = new SqlParameter("@Nombre", proveedor.TcNombre);
        var provinciaParam = new SqlParameter("@Provincia", proveedor.TcProvincia);
        var direccionParam = new SqlParameter("@Direccion", proveedor.TcDireccion);
        var emailParam = new SqlParameter("@Email", proveedor.TcEmail);
        var estadoParam = new SqlParameter("@Estado", proveedor.TcEstado);

        try
        {
            // Ejecutar el procedimiento almacenado 'spAgregarProveedor' con el parámetro @Estado
            await Database.ExecuteSqlRawAsync(
                "EXEC dbo.spAgregarProveedor @Nombre, @Direccion, @Provincia, @Email, @Estado",
                nombreParam, direccionParam, provinciaParam, emailParam, estadoParam
            );
        }
        catch (Exception ex)
        {
            // Lanza una nueva excepción con un mensaje
            throw new Exception($"Error inserting supplier: {ex.Message}", ex);
        }
    }
    #endregion
    #endregion
}















    #endregion

