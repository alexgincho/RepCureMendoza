using Microsoft.AspNetCore.Mvc;
using ProyectoUSMP_GYM.Helpers;
using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Response;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Controllers
{
    public class LoginUsuarioController : Controller
    {
        private IUsuarioService _Us;
        public LoginUsuarioController(IUsuarioService user)
        {
            this._Us = user;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginCliente entity)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario();
                usuario = _Us.Login(entity);
                if (usuario != null)
                {         
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "usuario", usuario);
                    return RedirectToAction("Index", "Home");
                }
            }
            else { return BadRequest(); }
            return View();
        }
    }
}
