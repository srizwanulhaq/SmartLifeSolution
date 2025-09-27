using Microsoft.EntityFrameworkCore;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.DAL.Dao.Menu;
using SmartLifeSolution.DAL.Dao.User;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.Menu;
using SmartLifeSolution.DAL.Enums;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Menus
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<MenuDao> GetAll()
        {
            return _context.Menus.Select(x => new MenuDao
            {
                Id = x.Id,
                Name = x.Name,
                Url = x.Url,
                Number = x.Number,
            }).ToList();

        }

        public List<MenuRoleDao> GetMenuDetails(string MenuId)
        {
            var data = _context.UserRoleMenus.Include(x => x.ApplicationRole)
                      .Where(x => x.MenuId == MenuId).Select(x => new MenuRoleDao
                      {
                          RoleId = x.RoleId,
                          RoleName = x.ApplicationRole.Name,
                      }).ToList();

            return data;

        }
        public string AddOrUpdate(MenuDto menu)
        {
            var objMenu = new Menu();

            if (!string.IsNullOrEmpty(menu.Id))
            {
                objMenu = _context.Menus.Where(x => x.Id == menu.Id).FirstOrDefault();
                objMenu.Name = menu.Name;
                objMenu.Url = menu.Url;
                _context.Menus.Update(objMenu);
            }
            else
            {
                objMenu = new Menu
                {
                    Name = menu.Name,
                    Url = menu.Url,
                    Number = _context.Menus.Count() + 1
                };
                _context.Menus.Add(objMenu);
            }

            _context.SaveChanges();

            return objMenu.Id;
        }
        public List<MenuDao> GetMenusByRoleId(string roleId)
        {
            var roleName = _context.ApplicationRole.Where(x => x.Id == roleId).FirstOrDefault()?.Name;

            if (roleName == UserRoleEnums.ADMIN.ToString())
            {
                return _context.Menus.Select(x => new MenuDao
                {
                    Id = x.Id,
                    Name = x.Name,
                    Number = x.Number,
                    Url = x.Url,

                }).ToList();
            }
            else
            {
                return _context.UserRoleMenus.Include(x => x.Menu)
                     .Where(x => x.RoleId == roleId).Select(x => new MenuDao
                     {
                         Id = x.Id,
                         Name = x.Menu.Name,
                         Number = x.Menu.Number,
                         Url = x.Menu.Url,
                     }).ToList();
            }

        }
        public bool Assign(UserMenuDto menuDto)
        {
            var objMenu = _context.UserRoleMenus
                  .Where(x => x.MenuId == menuDto.MenuId && x.RoleId == menuDto.RoleId)
                  .FirstOrDefault();

            if (objMenu == null)
            {
                objMenu = new UserRoleMenu()
                {
                    MenuId = menuDto.MenuId,
                    RoleId = menuDto.RoleId,
                };

                _context.UserRoleMenus.Add(objMenu);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

    }
}
