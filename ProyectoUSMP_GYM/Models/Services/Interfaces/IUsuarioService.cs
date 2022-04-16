using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Usuario CreateUsuario(UsuarioRequest usuario);
        public Usuario Login(LoginCliente user);
        public List<Usuario> GetAll();
    }
}
