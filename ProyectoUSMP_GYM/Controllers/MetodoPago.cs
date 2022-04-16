using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Controllers
{
    public class MetodoPago : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
