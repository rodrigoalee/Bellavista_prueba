using System;
using System.Collections.Generic;

namespace PruebaBellaVista.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public string? Presentacion { get; set; }

    public decimal Precio { get; set; }

    public int IdTipocafe { get; set; }

    public virtual Tipocafe? IdTipocafeNavigation { get; set; }

    public bool? Activo { get; set; }
}