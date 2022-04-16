using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class ProveedorService : IProveedorService
    {
        public Proveedor Create(Proveedor entity)
        {
            Proveedor result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    if (entity != null)
                    {
                        result = new Proveedor();   
                        result.Ruc= entity.Ruc;
                        result.Razonsocial=entity.Razonsocial;
                        result.Direccion=entity.Direccion;  
                        result.Email=entity.Email;
                        result.Telefono=entity.Telefono;    
                        result.Isdelete=entity.Isdelete;

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
            return result;
        }
        // una chequeada
        public bool Delete(int id)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Proveedors.Find(id);
                    if (obj != null)
                    {
                        obj.Isdelete = true;
                        obj.Fechaedita = DateTime.Now;
                        db.SaveChanges();
                        result = true;

                    }
                    else { throw new Exception("error, proveedor no encontrado"); }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;

            }
            return result;
        }

        public Proveedor Get(int id)
        {
            Proveedor result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Proveedors.Where(u => u.PkProveedor == id && u.Isdelete != true);
                    var prov = obj.FirstOrDefault();
                    if (prov != null)
                    {
                        result = prov;
                    }
                    else { throw new Exception("El Proveedor No Existe."); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public List<Proveedor> GetAll()
        {
            List<Proveedor> result = null;
            string error = "";
            try
            {
                using ( var db= new DbContext())
                {
                    var lst = db.Proveedors.Where(p=>p.Isdelete!=true).ToList().OrderByDescending(p => p.PkProveedor).ToList();
                    if (lst.Count()> 0)
                    {
                        result = lst;
                    }
                    else { throw new Exception("Error. No hay Proveedor registrado"); }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;  
        }

        public Proveedor Update(Proveedor entity)
        {
            Proveedor result = null;
            string error = "";
            try
            {
                using (var db= new DbContext())
                {
                    var obj = db.Proveedors.Find(entity.PkProveedor);
                    if (obj != null)
                    {
                        obj.Ruc = entity.Ruc;
                        obj.Razonsocial = entity.Razonsocial;
                        obj.Direccion = entity.Direccion;
                        obj.Email = entity.Email;
                        obj.Telefono = entity.Telefono;
                        db.SaveChanges();
                        result = entity;

                    }
                    else { throw new Exception("Error. Datos no Actualizados"); }
                }

            }
            catch (Exception ex )
            {
                error = ex.Message; 
            }
            return result;  
        }
    }
}
