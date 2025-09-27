using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class Template : BaseEntity
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string TemplateIcon { get; set; }
        public string TemplateImage { get; set; }
        public string CreatedByUserId { get; set; }
    }
}
