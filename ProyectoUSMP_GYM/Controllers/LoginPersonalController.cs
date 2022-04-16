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
    public class LoginPersonalController : Controller
    {
        public IPersonalService _sPersonal;
        private IMenuService _MenuServ;
        public LoginPersonalController(IPersonalService person,IMenuService menu)
        {
            this._sPersonal = person;
            this._MenuServ = menu;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginPersonal entity)
        {
            if (ModelState.IsValid)
            {
                PersonalData person = new PersonalData();
                    person.Personal = _sPersonal.LoginPersonal(entity);
                if(person.Personal != null)
                {
                    person.Menu = _MenuServ.GetMenuxRol(person.Personal.FkRol);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "personal", person);
                    return RedirectToAction("Index","Admin");
                }
            }
            else { return BadRequest(); }
            return View();
        }
    }
}
