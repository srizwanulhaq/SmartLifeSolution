using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.Menu
{
    public class MenuDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Url { get; set; }

    }

    public class UserMenuDto
    {
        public string MenuId { get; set; }
        public string RoleId { get; set; }
    }
}
