using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Detalleventum
    {
        public int PkDetalle { get; set; }
        public int? FkProducto { get; set; }
        public int? FkVenta { get; set; }
        public double? Preciounitario { get; set; }
        public int? Cantidad { get; set; }
        public double? Descuento { get; set; }
        public double? Subtotal { get; set; }

        public virtual Producto FkProductoNavigation { get; set; }
        public virtual Ventum FkVentaNavigation { get; set; }
    }
}
