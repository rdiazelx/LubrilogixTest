using System;
using System.Collections.Generic;

namespace LubrilogixTest.Models;

public partial class Producto
{
    public int TnIdProducto { get; set; }

    public string TcNombre { get; set; } = null!;

    public string TcCategoria { get; set; } = null!;

    public string TcSubcategoria { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
