using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class RolesService : IRolesService
    {
        public List<Role> GetRoles()
        {
            List<Role> result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                    var lst = db.Roles.ToList();

                    if (lst.Count() > 0) { result = lst; }
                    else { throw new Exception("Error. Datos Vacios."); }
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
