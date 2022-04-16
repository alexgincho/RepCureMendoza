using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Request
{
    public class CarritoCompra
    {
        public string Codigo { get; set; }
        public double Total { get; set; }
        public List<Detalle> Detalles { get; set; }
        public MetodoPagosc Metodo { get; set; }

        public CarritoCompra()
        {

        }

    }
    public class Detalle
    {
        public int pkProducto { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }

        public Detalle()
        {

        }
    }
    public class MetodoPagosc
    {
        public string Propietario { get; set; }
        public string Numeroccv { get; set; }
        public string Numerotarjeta { get; set; }
        public bool Tipotarjeta { get; set; }
        public MetodoPagosc()
        {

        }
    }
}
