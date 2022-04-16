using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class CategoriaService : ICategoriaService
    {
        public void Create(Categorium entity)
        {
            Categorium result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    if (entity != null)
                    {
                        result = new Categorium();
                        result.Descripcion= entity.Descripcion;
                        db.Add(result);
                        db.SaveChanges();
                    }
                    else { throw new Exception("Error. Datos vacios."); }

                }
            }
            catch (Exception ex)
            {
                error = ex.Message;

            }
        }
        public bool Delete(int id)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Categoria.Find(id);
                    if (obj != null)
                    {
                        obj.Isdelete = true;
                        db.SaveChanges();
                        result = true;
                    
                }
                    else { throw new Exception("Error. producto no encontrado"); }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public List<Categorium> GetAll()
        {
            List<Categorium> result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var lst = db.Categoria.ToList().OrderByDescending(p => p.PkCategoria).ToList();
                    if (lst.Count > 0)
                    {
                        result = lst;
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

        public Categorium Update(Categorium entity)
        {
            Categorium result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj =db.Categoria.Find (entity.PkCategoria);
                    if (obj != null)
                    {
                        obj.Descripcion = entity.Descripcion;
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
    }


}
