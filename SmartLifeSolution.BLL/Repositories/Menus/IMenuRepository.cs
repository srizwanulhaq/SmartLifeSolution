using SmartLifeSolution.DAL.Dao.Menu;
using SmartLifeSolution.DAL.Dto.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Menus
{
    public interface IMenuRepository
    {
        public List<MenuDao> GetMenusByRoleId(string roleId);
        public string AddOrUpdate(MenuDto menu);
        public bool Assign(UserMenuDto menuDto);
        public List<MenuDao> GetAll();
    }
}
