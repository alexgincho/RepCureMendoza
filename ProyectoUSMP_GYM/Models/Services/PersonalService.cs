using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Response;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class PersonalService : IPersonalService
    {
        public Personaladm Create(Personaladm entity)
        {
            Personaladm result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    if(entity != null)
                    {
                        result = new Personaladm();
                        result.Dni = entity.Dni;
                        result.Nombre = entity.Nombre;
                        result.Apellidopaterno = entity.Apellidopaterno;
                        result.Apellidomaterno = entity.Apellidomaterno;
                        result.Direccion = entity.Direccion;
                        result.Telefono = entity.Telefono;
                        result.Email = entity.Email;
                        result.Usuario = entity.Usuario;
                        result.Passwords = entity.Passwords;
                        result.FkRol = entity.FkRol;
                        result.Isdeleted = false;
                        result.Fechacrea = DateTime.Now;
                        result.Fechaedita = DateTime.Now;
                        result.FkPersonalcrea = 1;

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
        public bool Delete(int id)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Personaladms.Find(id);
                    if (obj != null)
                    {
                        obj.Isdeleted = true;
                        obj.Fechaedita = DateTime.Now;
                        db.SaveChanges();
                        result = true;
                    }
                    else { throw new Exception("Error. Personal Adm no encontrado."); }
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }
        public Personaladm Get(int id)
        {
            Personaladm result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Personaladms.Where(u=> u.PkPersonal == id && u.Isdeleted != true);
                    var usuario = obj.FirstOrDefault();
                    if (usuario != null)
                    {
                        result = usuario;
                    }
                    else { throw new Exception("Usuaio no Existe."); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public List<Personaladm> GetAll()
        {
            List<Personaladm> result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var lst = db.Personaladms.Where(p=>p.Isdeleted!=true).ToList().OrderByDescending(p => p.PkPersonal).ToList();
                    //var lst = db.Personaladms.Join(
                    //            db.Roles, p => p.FkRol, 
                    //            r => r.PkErol, 
                    //            (p, r) => 
                    //            new 
                    //            { 
                    //                p.Nombre,
                    //                r.Descripcion
                    //            });
                    //var personales = lst.ToList();
                    if (lst.Count() > 0)
                    {
                        result = lst;
                    }
                    else { throw new Exception("Error. No hay Personal Administrativo registrado"); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Personaladm LoginPersonal(LoginPersonal usuario)
        {
            Personaladm result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Personaladms.Where(p => p.Usuario == usuario.Usuario &&
                                                    p.Passwords == usuario.Password && p.Isdeleted != true).FirstOrDefault();
                    if(obj != null) { result = obj; }
                    else { throw new Exception("Error. Usuario no Existe"); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Personaladm Update(Personaladm entity)
        {
            Personaladm result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var obj = db.Personaladms.Find(entity.PkPersonal);
                    if(obj != null)
                    {
                        obj.Dni = entity.Dni; // Validar Dni no repetidos
                        obj.Nombre = entity.Nombre;
                        obj.Apellidopaterno = entity.Apellidopaterno;
                        obj.Apellidomaterno = entity.Apellidomaterno;
                        obj.Direccion = entity.Direccion;
                        obj.Telefono = entity.Telefono;
                        obj.Email = entity.Email; // Validar Email no repetidos
                        obj.Usuario = entity.Usuario; // Validar Usuarios no repetidos
                        obj.Passwords = entity.Passwords;
                        obj.Fechaedita = DateTime.Now;
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

        public bool ValidarDniPersonal(string dni)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var person = db.Personaladms.Where(p => p.Dni == dni).FirstOrDefault();
                    if (person != null) { result = true; }
                    else { result = false; }
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public bool ValidarEmailPersonal(string email)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var person = db.Personaladms.Where(p => p.Email == email).FirstOrDefault();
                    if (person != null) { result = true; }
                    else { result = false; }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public bool ValidarUsuarioPersonal(string usuario)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var person = db.Personaladms.Where(p => p.Usuario == usuario).FirstOrDefault();
                    if (person != null) { result = true; }
                    else { result = false; }
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
