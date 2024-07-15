using System;
using System.Collections.Generic;

namespace LubrilogixTest.Models;

public partial class Proveedore
{
    public int TnIdProveedor { get; set; }

    public string TcNombre { get; set; } = null!;

    public string TcDireccion { get; set; } = null!;

    public string TcProvincia { get; set; } = null!;

    public string TcEmail { get; set; } = null!;

    public string TcEstado { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
