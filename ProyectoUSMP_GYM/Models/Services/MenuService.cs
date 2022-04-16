using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Services
{
    public class MenuService : IMenuService
    {
        public List<Menu> GetMenuxRol(int? id)
        {
            List<Menu> result = null;
            string error = "";
            try
            {
                using (var db = new DbContext())
                {
                     var LstMenu = db.Menus.Join(db.MenuRols.Where(a=>a.FkRol == id), m => m.PkMenu,mr => mr.FkMenuNavigation.PkMenu,
                                               (m, mr) => new {
                                                   PkMenu = m.PkMenu,
                                                   Menu1 = m.Menu1,
                                                   Controller = m.Controller,
                                                   Actions = m.Actions,
                                                   Icons = m.Icons,
                                                   FkMenupadre = m.FkMenupadre,
                                                   Tipomenu = m.Tipomenu,
                                                   Ordenmenu = m.Ordenmenu 
                                               }).ToList();
                    if(LstMenu.Count > 0)
                    {
                        result = new List<Menu>();
                        foreach (var menu in LstMenu)
                        {
                            Menu Menu = new Menu();
                            Menu.PkMenu = menu.PkMenu;
                            Menu.Menu1 = menu.Menu1;
                            Menu.Controller = menu.Controller;
                            Menu.Actions = menu.Actions;
                            Menu.Icons = menu.Icons;
                            Menu.FkMenupadre = menu.FkMenupadre;
                            Menu.Tipomenu = menu.Tipomenu;
                            Menu.Ordenmenu = menu.Ordenmenu;
                            result.Add(Menu);
                        }
                    }
                    else { throw new Exception(); }
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
