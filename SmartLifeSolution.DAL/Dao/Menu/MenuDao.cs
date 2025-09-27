using SmartLifeSolution.DAL.Dao.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao.Menu
{
    public class MenuDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Url { get; set; }
    }

    public class MenuDdlDao
    {
        public List<MenuDao> MenuList { get; set; }
        public List<RoleDao> RoleList { get; set; }
    }

    public class MenuDetailsDao
    {
      public List<MenuRoleDao> RoleList { get; set; }   
    }

    public class MenuRoleDao
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsChecked { get; set; }
    }


}
