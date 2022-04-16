using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class ProductoService : IProductoService
    {
        public Producto Create(Producto entity)
        {
            Producto result = null;
            string error  = "";
            try
            {
                using (var db = new DbContext())
                {
                    if (entity != null)
                    {
                        result = new Producto();
                        result.Codigo=entity.Codigo;
                        result.Nombre=entity.Nombre;    
                        result.Descripcion=entity.Descripcion;  
                        result.Precioventa=entity.Precioventa;  
                        result.Preciocompra=entity.Preciocompra;
                        result.Cantidad=entity.Cantidad;    
                        result.Descuento=entity.Descuento;
                        result.FkProveedor = 2;
                        result.FkCategoria=entity.FkCategoria;  
                        result.Fechavencimiento=DateTime.Now;
                        result.FkPersonalcrea = 1;
                        result.Fechacrea=DateTime.Now;
                        result.Imagen = entity.Imagen;
                        result.Isdelete = false;
                        
                       

                        db.Add(result);
                        db.SaveChanges();
                    }
                    else { throw new Exception("Error"); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Productos.Find(id);
                    if (obj != null)
                    {
                        obj.Isdelete = true;    
                        obj.Fechaedita= DateTime.Now;
                        db.SaveChanges();
                        result = true;

                    }
                     else { throw new Exception("error"); }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message; 

            }
            return result;
        }


        // otra chequeada al get producto
        public Producto Get(int id)
        {
            Producto result = null;
            string error = "";
            try
            {
                using (var db= new DbContext())
                {
                    var obj = db.Productos.Where(u => u.PkProducto == id && u.Isdelete != true);
                  
                    

                    var Producto = obj.FirstOrDefault();
                    if (Producto != null)
                    {
                        result = Producto;
                    }
                    else { throw new Exception("no existe ");  }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
                
        }

        public Producto GetProductoxCodigo(string codigo)
        {
            Producto result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Productos.Where(u => u.Codigo == codigo && u.Isdelete != true);
                   
                   var Codigo = obj.FirstOrDefault();
                    if (Codigo != null)
                    {
                        result = Codigo;
                    }
                    else { throw new Exception("no existe "); }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;

        }

        public List<Producto> GetAll()
        {
            List<Producto> result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {

                    //la eh cagado
                    var lst = db.Productos.Where(p => p.Isdelete != true).ToList().OrderByDescending(p => p.PkProducto).ToList();
                    
                    if (lst.Count() > 0)
                    {
                        result = lst;
                    }
                    else { throw new Exception("Error. No hay producto registrado"); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Producto Update(Producto entity)
        {
            Producto result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Productos.Find(entity.PkProducto);
                    if (obj != null)
                    {

                        obj.Codigo = entity.Codigo;
                        obj.Nombre = entity.Nombre;
                        obj.Descripcion = entity.Descripcion;
                        obj.Precioventa = entity.Precioventa;
                        obj.Preciocompra = entity.Preciocompra;
                        obj.Cantidad = entity.Cantidad;
                        obj.Descuento = entity.Descuento;
                        obj.FkProveedor = entity.FkProveedor;
                        obj.FkCategoria = entity.FkCategoria;
                        obj.Fechavencimiento = DateTime.Now;
                        obj.FkPersonalcrea = 1;
                        obj.Fechacrea = DateTime.Now;
                        obj.Fechaedita = entity.Fechaedita;
                        obj.Imagen = entity.Imagen;
                        db.SaveChanges();
                        result = entity;
                    }
                    else { throw new Exception("Error. Datos no Actualizados"); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Producto GetProducxNombre(string Nombre)
        {
            Producto result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Productos.Where(u => u.Nombre == Nombre && u.Isdelete != true);

                    var Codigo = obj.FirstOrDefault();
                    if (Codigo != null)
                    {
                        result = Codigo;
                    }
                    else { throw new Exception("no existe "); }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;

        }

        public List<Producto> GetAllProductoStock()
        {
            List<Producto> result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var LstProduct = db.Productos.Where(prod => prod.Isdelete != true && prod.Cantidad != 0).ToList();
                    if(LstProduct.Count > 0) { result = LstProduct; }
                    else { throw new Exception(); }
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }
    }
}
