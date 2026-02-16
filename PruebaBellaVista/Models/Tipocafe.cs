using System;
using System.Collections.Generic;

namespace PruebaBellaVista.Models;

public partial class Tipocafe
{
    public int IdTipocafe { get; set; }

    public string NombreCafe { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
