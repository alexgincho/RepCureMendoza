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
    public class PersonalController : Controller
    {
        private  IRolesService _sRol;
        private IPersonalService _sPer;
        public PersonalController(IRolesService sRol, IPersonalService sPer)
        {
            this._sRol = sRol;
            this._sPer = sPer;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MantenimientoPersonal(int id = 0)
        {
            Personaladm entity = null;
            ViewBag.Roles = _sRol.GetRoles();
            if (id != 0) entity = _sPer.Get(id);
            return PartialView("_MantenimientoPersonal",entity ?? new Personaladm());
        }
      
        // Services Rest
        [HttpPost]
        public IActionResult CreatePersonal([FromBody] Personaladm entity)
        {
            Response rpta = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    if (entity.PkPersonal != 0)
                    {
                        // Logica de Actualizacion de Personal.

                    }
                    else
                    {
                        var ValidateDni = _sPer.ValidarDniPersonal(entity.Dni);
                        var ValidateEmail = _sPer.ValidarEmailPersonal(entity.Email);
                        var ValidateUser = _sPer.ValidarUsuarioPersonal(entity.Usuario);
                        if (ValidateDni || ValidateEmail || ValidateUser)
                        {
                            throw new Exception("Error. Datos ya Registrados.");
                        }
                        rpta.Data = _sPer.Create(entity);
                        rpta.Message = "Success.";
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
        public IActionResult UpdatePersonal([FromBody] Personaladm entity)
        {
            Response rpta = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    if (entity.PkPersonal != 0)
                    {
                       //var ValidateDni = _sPer.ValidarDniPersonal(entity.Dni);
                       // if (ValidateDni)
                        //{
                          //  throw new Exception("Error. Datos ya Registrados.");
                        //}
                        rpta.Data = _sPer.Update(entity);
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
        public IActionResult DesactivePersonal([FromBody] int id)
        {
            Response rpta = new Response();
            try
            {
                var DeletePerson = _sPer.Delete(id);
                if (DeletePerson)
                {
                    rpta.Data = true;
                    rpta.Message = "Se Desactivo Personal Correctamente!";
                    rpta.State = 200;
                }
                else { throw new Exception("Error. No se pudo desactivar el Personal."); }
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
        public IActionResult GetAllPersonal()
        {
            Response rpta = new Response();
            try
            {        
                rpta.Data = _sPer.GetAll();
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
