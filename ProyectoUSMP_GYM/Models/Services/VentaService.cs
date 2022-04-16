using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Request;
using ProyectoUSMP_GYM.Models.Response;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class VentaService : IVentaService
    {
        public Ventum AddCarrito(CarritoCompra carrito)
        {
            Ventum result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    using (var transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            result = new Ventum();
                            result.Codigo = carrito.Codigo;
                            result.Observacion = "";
                            result.Fechaentrega = DateTime.Now;
                            result.FkUsuario = 1;
                            result.Delivery = false;
                            result.Estado = 0;
                            result.Totalventa = carrito.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
                            result.Fechacrea = DateTime.Now;
                            result.FkPersonalcrea = 1;
                            result.Isdelete = false;

                            db.Venta.Add(result);
                            db.SaveChanges();

                            foreach (var item in carrito.Detalles)
                            {
                                var Detalle = new Detalleventum();
                                Detalle.FkVenta = result.PkVenta;
                                Detalle.FkProducto = item.pkProducto;
                                Detalle.Cantidad = item.Cantidad;
                                Detalle.Preciounitario = item.PrecioUnitario;
                                Detalle.Descuento = 0;
                                Detalle.Subtotal = item.Cantidad * item.PrecioUnitario;

                                db.Detalleventa.Add(Detalle);
                                db.SaveChanges();

                            }

                            Metodopago metodo = new Metodopago();
                            metodo.Numerotarjeta = carrito.Metodo.Numerotarjeta;
                            metodo.Numeroccv = carrito.Metodo.Numeroccv;
                            metodo.Propietario = carrito.Metodo.Propietario;
                            metodo.Tipotarjeta = carrito.Metodo.Tipotarjeta;

                            db.Metodopagos.Add(metodo);
                            db.SaveChanges();

                            transaccion.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            throw new Exception("Ocurrio un Error en la Insercion de Datos. ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Ventum CreateVenta(Ventum venta)
        {
            Ventum result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    using (var transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            result = new Ventum();
                            result.Codigo = venta.Codigo;
                            result.Observacion = venta.Observacion;
                            result.Fechaentrega = venta.Fechaentrega;
                            result.FkUsuario = venta.FkUsuario;
                            result.Delivery = false;
                            result.Estado = 0;
                            result.Totalventa = venta.Totalventa;
                            result.Fechacrea = DateTime.Now;
                            result.FkPersonalcrea = 1;
                            result.Isdelete = false;

                            db.Venta.Add(result);
                            db.SaveChanges();

                            foreach(var item in venta.Detalleventa)
                            {
                                var Detalle = new Detalleventum();
                                Detalle.FkVenta = result.PkVenta;
                                Detalle.FkProducto = item.FkProducto;
                                Detalle.Cantidad = item.Cantidad;
                                Detalle.Preciounitario = item.Preciounitario;
                                Detalle.Descuento = item.Descuento;
                                Detalle.Subtotal = item.Subtotal;

                                db.Detalleventa.Add(Detalle);
                                db.SaveChanges();

                            }

                            transaccion.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            throw new Exception("Ocurrio un Error en la Insercion de Datos. ");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }
    }
}
