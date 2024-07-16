using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LubrilogixTest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operaciones",
                columns: table => new
                {
                    TN_IDTipoOperacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC_NombreOperacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Operacio__7321F872BB5451E0", x => x.TN_IDTipoOperacion);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    TN_IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC_Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TC_Categoria = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TC_Subcategoria = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__74F248D98702D11A", x => x.TN_IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    TN_IdProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC_Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TC_Direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    TC_Provincia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TC_Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TC_Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__B72A1EAA814B6E0D", x => x.TN_IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    TN_IdSucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC_Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TC_Provincia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TC_Direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    TC_Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TC_Correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TC_Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TC_Comentarios = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sucursal__051BE04C2C31C2BA", x => x.TN_IdSucursal);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    TN_IdOrden = table.Column<int>(type: "int", nullable: false),
                    TF_Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    TN_IDSucursal = table.Column<int>(type: "int", nullable: false),
                    TN_IdProducto = table.Column<int>(type: "int", nullable: false),
                    TN_Cantidad = table.Column<int>(type: "int", nullable: false),
                    TN_Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    T_N_Descuento = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TN_IDTipoOperacion = table.Column<int>(type: "int", nullable: false),
                    TN_IdProveedor = table.Column<int>(type: "int", nullable: false),
                    TN_Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TC_Estado = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventar__5EA717080ECA917C", x => x.TN_IdOrden);
                    table.ForeignKey(
                        name: "FK__Inventari__TN_ID__534D60F1",
                        column: x => x.TN_IDSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "TN_IdSucursal");
                    table.ForeignKey(
                        name: "FK__Inventari__TN_ID__5441852A",
                        column: x => x.TN_IDTipoOperacion,
                        principalTable: "Operaciones",
                        principalColumn: "TN_IDTipoOperacion");
                    table.ForeignKey(
                        name: "FK__Inventari__TN_Id__5165187F",
                        column: x => x.TN_IdProducto,
                        principalTable: "Productos",
                        principalColumn: "TN_IdProducto");
                    table.ForeignKey(
                        name: "FK__Inventari__TN_Id__52593CB8",
                        column: x => x.TN_IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "TN_IdProveedor");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_TN_IdProducto",
                table: "Inventario",
                column: "TN_IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_TN_IdProveedor",
                table: "Inventario",
                column: "TN_IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_TN_IDSucursal",
                table: "Inventario",
                column: "TN_IDSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_TN_IDTipoOperacion",
                table: "Inventario",
                column: "TN_IDTipoOperacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Operaciones");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
