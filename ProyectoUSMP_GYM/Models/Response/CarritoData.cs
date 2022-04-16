using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Response
{
    public class CarritoData
    {
        public int? FkProducto { get; set; }
        public double? Preciounitario { get; set; }
        public int? Cantidad { get; set; }
        public double? Descuento { get; set; }
        public double? Subtotal { get; set; }
    }
}
