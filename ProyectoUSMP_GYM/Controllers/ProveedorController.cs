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
    public class ProveedorController : Controller
    {
        private IProveedorService _sProv;
        public ProveedorController(IProveedorService sProv)
        {
            this._sProv = sProv;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MantenimientoProveedor(int id = 0)
        {
            Proveedor entity = null;
            if (id != 0) entity = _sProv.Get(id);
            return PartialView("_MantenimientoProveedor", entity ?? new Proveedor());
        }

        // Services Rest
        [HttpPost]
        public IActionResult CreateProveedor([FromBody] Proveedor entity)
        {
            Response rpta = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    if (entity.PkProveedor != 0)
                    {
                        
                    }
                    else
                    {
                       /* var ValidateRuc = _sProv.ValidarRucProveedor(entity.Ruc);
                        if (ValidateRuc)
                        {
                            throw new Exception("Error. Los datos ya se encuentran registrados");
                        }*/

                        rpta.Data = _sProv.Create(entity);
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
        public IActionResult UpdateProveedor([FromBody] Proveedor entity)
        {
            Response rpta = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    if (entity.PkProveedor != 0)
                    {

                        rpta.Data = _sProv.Update(entity);
                        rpta.Message = "Success.";
                        rpta.State = 200;

                    }
                    else
                    {
                        return BadRequest();
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
        public IActionResult DesactiveProveedor([FromBody] int id)
        {
            Response rpta = new Response();
            try
            {
                var DeletePerson = _sProv.Delete(id);
                if (DeletePerson)
                {
                    rpta.Data = true;
                    rpta.Message = "Se Desactivo Proveedor Correctamente!";
                    rpta.State = 200;
                }
                else { throw new Exception("Error. No se pudo desactivar el Proveedor."); }
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
        public IActionResult GetAllProveedor()
        {
            Response rpta = new Response();
            try
            {
                rpta.Data = _sProv.GetAll();
                rpta.State = 200;
                rpta.Message = "Success.";
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.Message = "Error";
                rpta.State = 400;
            }
            return Ok(rpta);
        }
    }
}