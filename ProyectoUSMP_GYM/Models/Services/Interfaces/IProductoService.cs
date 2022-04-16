using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services.Interfaces
{
    public interface IProductoService
    {
        public Producto Create(Producto entity); //crear producto nuevo

        public Producto Get(int id); //obtener producto mediante el id

        public Producto GetProductoxCodigo(string Codigo); //obtener mediante el codigo

        public Producto GetProducxNombre(string Nombre); //obtener mediante el nombre del producto

        public bool Delete(int id); //eliminar producto mediante el ID

        public List<Producto> GetAll(); // listado de todos los productos

        public Producto Update(Producto entity); //actualizar producto

        public List<Producto> GetAllProductoStock();
    

    
    
    
    
    
    
    
    
    }
}