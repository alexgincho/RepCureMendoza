using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoUSMP_GYM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using ProyectoUSMP_GYM.Models.Response;
using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Helpers;
using Microsoft.AspNetCore.Http;
using ProyectoUSMP_GYM.Models.Request;

namespace ProyectoUSMP_GYM.Controllers
{
    public class HomeController : Controller 
    {

        private IProductoService _sProduct;
        private IUsuarioService _Us;
        private IVentaService _Sv;
        private readonly ILogger<HomeController> _loger;

        public List<CarritoData> carritoDatas = new List<CarritoData>();

        public HomeController(IProductoService product, IUsuarioService Us, IVentaService ventas, ILogger<HomeController> loger)
        {
            this._sProduct = product;
            this._Us = Us;
            this._Sv = ventas;
            this._loger = loger;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Equipo()
        {
            return View();
        }
     
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUsuario([FromBody] UsuarioRequest users)
        {
            Response rpta = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    //var user = _Us.CreateUsuario(users);
                    //if (user != null)
                    //{
                    //    rpta.Data = user;
                    //    rpta.State = 200;
                    //    rpta.Message = "Exito!";
                    //}
                    //else { throw new Exception("Error."); }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                rpta.Message = ex.Message;
                rpta.Data = null;
                rpta.State = 400;
            }

            return Ok(rpta);
        }
        public IActionResult VistaProducto()
        {
            return View();
        }
        public IActionResult GetProductosAll()
        {
            Response rpta = new Response();
            try
            {
                var LstProducto = _sProduct.GetAll();
                if(LstProducto.Count > 0)
                {
                    rpta.Data = LstProducto;
                    rpta.State = 200;
                    rpta.Message = "Success";
                }
                else { throw new Exception();  }
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.State = 400;
                rpta.Message = "Error";
            }
            return Ok(rpta);
        }
        public IActionResult GetProducto(int id)
        {
            Producto producto = new Producto();
            try
            {             
                var prod = _sProduct.Get(id);
                if(prod != null) { producto = prod; }
                else { prod = null; }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return View(producto);
        }
        [HttpGet]
        public IActionResult GetProductoxCategoria(int id)
        {

            return Ok();
        }
        public IActionResult WebUser()
        {
            return View();
        }

        public IActionResult Carrito()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCarrito([FromBody] CarritoCompra CarritoCompra)
        {
            Response rpta = new Response();
            try
            {
                var usuario = HttpContext.Session.GetString("usuario");
                if (usuario != null)
                {
                    var venta = _Sv.AddCarrito(CarritoCompra);
                    if (venta != null)
                    {
                        rpta.Data = venta;
                        rpta.State = 200;
                        rpta.Message = "Success";
                    }
                    else { throw new Exception(); }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.State = 400;
                rpta.Message = "Error";
            }
            return Ok(rpta);
        }
    }
}

