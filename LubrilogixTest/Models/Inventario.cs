using System;
using System.Collections.Generic;

namespace LubrilogixTest.Models;

public partial class Inventario
{
    public int TnIdOrden { get; set; }

    public DateOnly TfFecha { get; set; }

    public int TnIdsucursal { get; set; }

    public int TnIdProducto { get; set; }

    public int TnCantidad { get; set; }

    public decimal TnPrecio { get; set; }

    public decimal TNDescuento { get; set; }

    public int TnIdtipoOperacion { get; set; }

    public int TnIdProveedor { get; set; }

    public decimal TnTotal { get; set; }

    public virtual Producto TnIdProductoNavigation { get; set; } = null!;

    public virtual Proveedore TnIdProveedorNavigation { get; set; } = null!;

    public virtual Sucursale TnIdsucursalNavigation { get; set; } = null!;

    public virtual Operacione TnIdtipoOperacionNavigation { get; set; } = null!;
}
