using ProyectoUSMP_GYM.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services.Interfaces
{
    public interface IProveedorService
    {
        public Proveedor Create(Proveedor entity); //creacion 
        public Proveedor Update(Proveedor entity); // actualizar 
        public List<Proveedor> GetAll(); //lista
        public bool Delete(int id); // eliminar
        public Proveedor Get(int id); //obtener un proveedor mediante el ID
    }

}