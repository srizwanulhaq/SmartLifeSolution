using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class UserDashboard : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string? TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        public Template Template { get; set; }
        public string DashboardImage { get; set; }

    }
}
