using ProyectoUSMP_GYM.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services.Interfaces
{
    public interface ICategoriaService
    {
        public void Create(Categorium entity); //creacion 
        public Categorium Update(Categorium entity); // actualizar 
        public List<Categorium> GetAll(); //lista
        
        public bool Delete(int id); // eliminar
    }

}