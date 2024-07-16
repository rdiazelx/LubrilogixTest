﻿// <auto-generated />
using System;
using LubrilogixTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LubrilogixTest.Migrations
{
    [DbContext(typeof(LubrilogixDbContext))]
    partial class LubrilogixDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LubrilogixTest.Models.Inventario", b =>
                {
                    b.Property<int>("TnIdOrden")
                        .HasColumnType("int")
                        .HasColumnName("TN_IdOrden");

                    b.Property<decimal>("TNDescuento")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("T_N_Descuento");

                    b.Property<string>("TcEstado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("TfFecha")
                        .HasColumnType("date")
                        .HasColumnName("TF_Fecha");

                    b.Property<int>("TnCantidad")
                        .HasColumnType("int")
                        .HasColumnName("TN_Cantidad");

                    b.Property<int>("TnIdProducto")
                        .HasColumnType("int")
                        .HasColumnName("TN_IdProducto");

                    b.Property<int>("TnIdProveedor")
                        .HasColumnType("int")
                        .HasColumnName("TN_IdProveedor");

                    b.Property<int>("TnIdsucursal")
                        .HasColumnType("int")
                        .HasColumnName("TN_IDSucursal");

                    b.Property<int>("TnIdtipoOperacion")
                        .HasColumnType("int")
                        .HasColumnName("TN_IDTipoOperacion");

                    b.Property<decimal>("TnPrecio")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("TN_Precio");

                    b.Property<decimal>("TnTotal")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("TN_Total");

                    b.HasKey("TnIdOrden")
                        .HasName("PK__Inventar__5EA717080ECA917C");

                    b.HasIndex("TnIdProducto");

                    b.HasIndex("TnIdProveedor");

                    b.HasIndex("TnIdsucursal");

                    b.HasIndex("TnIdtipoOperacion");

                    b.ToTable("Inventario", (string)null);
                });

            modelBuilder.Entity("LubrilogixTest.Models.Operacione", b =>
                {
                    b.Property<int>("TnIdtipoOperacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TN_IDTipoOperacion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TnIdtipoOperacion"));

                    b.Property<string>("TcNombreOperacion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_NombreOperacion");

                    b.HasKey("TnIdtipoOperacion")
                        .HasName("PK__Operacio__7321F872BB5451E0");

                    b.ToTable("Operaciones");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Producto", b =>
                {
                    b.Property<int>("TnIdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TN_IdProducto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TnIdProducto"));

                    b.Property<string>("TcCategoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_Categoria");

                    b.Property<string>("TcNombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TC_Nombre");

                    b.Property<string>("TcSubcategoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_Subcategoria");

                    b.HasKey("TnIdProducto")
                        .HasName("PK__Producto__74F248D98702D11A");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Proveedore", b =>
                {
                    b.Property<int>("TnIdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TN_IdProveedor");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TnIdProveedor"));

                    b.Property<string>("TcDireccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("TC_Direccion");

                    b.Property<string>("TcEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TC_Email");

                    b.Property<string>("TcEstado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_Estado");

                    b.Property<string>("TcNombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TC_Nombre");

                    b.Property<string>("TcProvincia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_Provincia");

                    b.HasKey("TnIdProveedor")
                        .HasName("PK__Proveedo__B72A1EAA814B6E0D");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Sucursale", b =>
                {
                    b.Property<int>("TnIdSucursal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TN_IdSucursal");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TnIdSucursal"));

                    b.Property<string>("TcComentarios")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_Comentarios");

                    b.Property<string>("TcCorreo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TC_Correo");

                    b.Property<string>("TcDireccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("TC_Direccion");

                    b.Property<string>("TcEstado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_Estado");

                    b.Property<string>("TcNombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TC_Nombre");

                    b.Property<string>("TcProvincia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TC_Provincia");

                    b.Property<string>("TcTelefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("TC_Telefono");

                    b.HasKey("TnIdSucursal")
                        .HasName("PK__Sucursal__051BE04C2C31C2BA");

                    b.ToTable("Sucursales");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Inventario", b =>
                {
                    b.HasOne("LubrilogixTest.Models.Producto", "TnIdProductoNavigation")
                        .WithMany("Inventarios")
                        .HasForeignKey("TnIdProducto")
                        .IsRequired()
                        .HasConstraintName("FK__Inventari__TN_Id__5165187F");

                    b.HasOne("LubrilogixTest.Models.Proveedore", "TnIdProveedorNavigation")
                        .WithMany("Inventarios")
                        .HasForeignKey("TnIdProveedor")
                        .IsRequired()
                        .HasConstraintName("FK__Inventari__TN_Id__52593CB8");

                    b.HasOne("LubrilogixTest.Models.Sucursale", "TnIdsucursalNavigation")
                        .WithMany("Inventarios")
                        .HasForeignKey("TnIdsucursal")
                        .IsRequired()
                        .HasConstraintName("FK__Inventari__TN_ID__534D60F1");

                    b.HasOne("LubrilogixTest.Models.Operacione", "TnIdtipoOperacionNavigation")
                        .WithMany("Inventarios")
                        .HasForeignKey("TnIdtipoOperacion")
                        .IsRequired()
                        .HasConstraintName("FK__Inventari__TN_ID__5441852A");

                    b.Navigation("TnIdProductoNavigation");

                    b.Navigation("TnIdProveedorNavigation");

                    b.Navigation("TnIdsucursalNavigation");

                    b.Navigation("TnIdtipoOperacionNavigation");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Operacione", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Producto", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Proveedore", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("LubrilogixTest.Models.Sucursale", b =>
                {
                    b.Navigation("Inventarios");
                });
#pragma warning restore 612, 618
        }
    }
}
