using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class TemplateWidget : BaseEntity
    {
        public string ImageId { get; set; }
        public string TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        public Template Templates { get; set; }
        public string UserWidgetId { get; set; }

        [ForeignKey("UserWidgetId")]
        public UserWidget UserWidgets { get; set; }
    }
}
