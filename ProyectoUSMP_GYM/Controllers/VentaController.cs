using Microsoft.AspNetCore.Mvc;
using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Response;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Controllers
{
    public class VentaController : Controller
    {
        private IProductoService _Ps;
        private IUsuarioService _Us;
        private IVentaService _Vs;
        public VentaController(IProductoService Ps, IUsuarioService Us,IVentaService Vs)
        {
            _Ps = Ps;
            _Us = Us;
            _Vs = Vs;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VentaCreate([FromBody] Ventum venta)
        {
            Response rpta = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    var RptaVenta = _Vs.CreateVenta(venta);
                    if(RptaVenta != null)
                    {
                        rpta.Data = RptaVenta;
                        rpta.State = 200;
                        rpta.Message = "Success";
                    }
                    else { throw new Exception(); }
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.State = 404;
                rpta.Message = "Error.";
            }
            return Ok();
        }
        public IActionResult PartialListProductos()
        {
            return PartialView("_ListadoProductosVenta");
        }
        public IActionResult GetProductos()
        {
            Response rpta = new Response();
            try
            {
                //var LstProd = _Ps.GetAllProductoStock();
                //if(LstProd.Count > 0) { rpta.Data = LstProd; rpta.State = 200; rpta.Message = "Success"; }
                //else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.State = 404;
                rpta.Message = "Error.";
            }
            return Ok(rpta);
        }
        public IActionResult PartialListCliente()
        {
            return PartialView("_ListaClientesVenta");
        }
        public IActionResult GetClientes()
        {
            Response rpta = new Response();
            try
            {
                //var LstCliente = _Us.GetAll();
                //if (LstCliente.Count > 0) { rpta.Data = LstCliente; rpta.State = 200; rpta.Message = "Success"; }
                //else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.State = 404;
                rpta.Message = "Error.";
            }
            return Ok(rpta);
        }
    }
}
