using System;
using System.Collections.Generic;

namespace LubrilogixTest.Models;

public partial class Operacione
{
    public int TnIdtipoOperacion { get; set; }

    public string TcNombreOperacion { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
