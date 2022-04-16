using ProyectoUSMP_GYM.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Response
{
    public class UsuarioRequest
    {
        public Usuario Usuario { get; set; }
        public Usuariologin LoginUser { get; set; }
    }
}
