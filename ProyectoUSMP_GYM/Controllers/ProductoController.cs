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
    public class ProductoController : Controller
    {
        private ICategoriaService _sCat;
        private IProductoService _sPro;
        public ProductoController(ICategoriaService sCat, IProductoService sPro)
        {
            
            this._sCat = sCat;
            this._sPro= sPro;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MantenimientoProductos(int id = 0)
        {
            Producto entity =null;
            ViewBag.Categoria = _sCat.GetAll();
            if(id!=0) entity = _sPro.Get(id);
            return PartialView("_MantenimientoProducto",entity ?? new Producto());
        }
         

        // Services Rest
        [HttpPost]
        public IActionResult CreateProducto([FromBody] Producto entity)
        {
            Response rpta = new Response();
            try
            {
                if(ModelState.IsValid)
                {
                    if(entity.PkProducto != 0)
                    {

                    }
                    else
                    {   
                        /* Falta en IProductoService Validar
                        var ValidateCodigo = _sPro.ValidarCodigoProducto(entity.Codigo);
                        var ValidateNombre = _sPro.ValidarNombreProducto(entity.Nombre);
                        if(ValidateCodigo || ValidateNombre)
                        {
                            throw new Exception("Error. Los datos ya se encuentran registrados");
                        }
                        */
                        rpta.Data = _sPro.Create(entity);
                        rpta.Message = "Success";
                        rpta.State = 200;
                    }
                }
                else { return BadRequest(); }
            }
            catch (Exception ex) 
            {
                rpta.State = 404;
                rpta.Message = ex.Message;
                rpta.Data = null;
            }
            return Ok(rpta);
        }

        [HttpPost]
        public IActionResult UpdateProducto([FromBody] Producto entity)
        {
            Response rpta = new Response();
            try
            {
                if(ModelState.IsValid)
                {
                    if(entity.PkProducto != 0)
                    {
                        rpta.Data = _sPro.Update(entity);
                        rpta.Message = "Success.";
                        rpta.State = 200;
                    }else
                    {
                        return BadRequest();
                    }
                    
                }else {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                rpta.State = 404;
                rpta.Message = ex.Message;
                rpta.Data = null;
            }
            return Ok(rpta);
        }
       


        
        [HttpPost]
        public IActionResult DesactiveProducto([FromBody] int id)
        {
            Response rpta = new Response();
            try
            {
                var DeleteProduct = _sPro.Delete(id);
                if (DeleteProduct)
                {
                    rpta.Data = true;
                    rpta.Message = "Producto Desactivado";
                    rpta.State = 200;
                }
                else { throw new Exception("Error. El producto no se pudo desactivar");}

            }
            catch (Exception ex)
            {
                rpta.State = 404;
                rpta.Data = null;
                rpta.Message = ex.Message; 
                
            }
            return Ok(rpta);
        }
        [HttpGet]
        public IActionResult GetAllProductoP()
        {
            Response rpta = new Response();
            try
            {
                rpta.Data = _sPro.GetAll();
                rpta.State = 200;
                rpta.Message = "Success.";
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