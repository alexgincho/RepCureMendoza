using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Request;
using ProyectoUSMP_GYM.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services.Interfaces
{
    public interface IVentaService
    {
        public Ventum CreateVenta(Ventum venta);
        public Ventum AddCarrito(CarritoCompra carrito);
    }
}
