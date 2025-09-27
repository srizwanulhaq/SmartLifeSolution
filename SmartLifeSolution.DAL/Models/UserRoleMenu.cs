using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class UserRoleMenu : BaseEntity
    {
        public string MenuId { get; set; }
        public string RoleId { get; set; }

        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole ApplicationRole { get; set; }
    }

}
