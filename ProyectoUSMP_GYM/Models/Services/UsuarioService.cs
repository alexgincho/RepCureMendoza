using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Response;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class UsuarioService : IUsuarioService
    {
        public Usuario CreateUsuario(UsuarioRequest U)
        {
            Usuario result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    using (var transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            result = new Usuario();
                            result.Dni = U.Usuario.Dni;
                            result.Nombre = U.Usuario.Nombre;
                            result.Apellidopaterno = U.Usuario.Apellidopaterno;
                            result.Apellidomaterno = U.Usuario.Apellidomaterno;
                            result.Telefono = U.Usuario.Telefono;
                            result.Direccion = U.Usuario.Direccion;
                            result.Email = U.Usuario.Email;
                            result.Tipousuario = U.Usuario.Tipousuario; // ??
                            result.Userweb = U.Usuario.Userweb; // ??
                            result.FkPersonalcrea = 1;
                            result.Fechacrea = DateTime.Now;
                            result.Isdeleted = false;

                            db.Usuarios.Add(result);
                            db.SaveChanges();

                            Usuariologin userLogin = new Usuariologin();
                            userLogin.FkUsuario = result.PkUsuario; // valor?
                            userLogin.Usuario = result.Email;
                            userLogin.Passwords = U.LoginUser.Passwords;

                            db.Usuariologins.Add(userLogin);
                            db.SaveChanges();

                            transaccion.Commit();

                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var LstClient = db.Usuarios.ToList();
                    if(LstClient.Count > 0) { result = LstClient; }
                    else { throw new Exception(); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Usuario Login(LoginCliente user)
        {
            Usuario result = null;
            string error = "";
            try
            {             
                using (var db = new DbContext())
                {
                    result = new Usuario();
                    var usu = db.Usuarios.Join(db.Usuariologins.Where(ul => ul.Usuario == user.Email && ul.Passwords == user.Password),
                                                                u => u.PkUsuario, ulg => ulg.FkUsuarioNavigation.PkUsuario, 
                                                                (u, ulg) => new { 

                                                                    pkUsuario = u.PkUsuario,
                                                                    Nombre = u.Nombre,
                                                                    ApellidoPaterno = u.Apellidopaterno,
                                                                    ApellidoMaterno = u.Apellidomaterno,
                                                                    Email = u.Email,
                                                                    Telefono = u.Telefono,
                                                                    Direccion = u.Direccion
                                                                }).FirstOrDefault();
                    if(usu.pkUsuario != 0)
                    {
                        result.PkUsuario = usu.pkUsuario;
                        result.Nombre = usu.Nombre;
                        result.Apellidopaterno = usu.ApellidoPaterno;
                        result.Apellidomaterno = usu.ApellidoMaterno;
                        result.Telefono = usu.Telefono;
                        result.Email = usu.Email;
                        result.Direccion = usu.Direccion;
                    }
                    else { throw new Exception("Error. Cliente no Existe"); }
                    
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
