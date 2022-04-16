using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services.Interfaces
{
    public interface IPersonalService
    {
        public Personaladm Create(Personaladm entity); // Creacion de un Personal Administrativo.
        public Personaladm Get(int id); // Obtener un Personal por su ID.
        public List<Personaladm> GetAll(); // Listado de todo los Personales.
        public Personaladm Update(Personaladm entity); // Actualizar Personal.
        public bool Delete(int id); //Eliminar por ID personal.
        public bool ValidarDniPersonal(string dni); // Validamos el dni que no se ingrese duplicados.
        public bool ValidarUsuarioPersonal(string usuario); // Validamos que no se registren usuarios con el mismo name.
        public bool ValidarEmailPersonal(string email); // Validamos que no se registren email duplicados.
        public Personaladm LoginPersonal(LoginPersonal usuario); // Login del Personal.
    }
}
