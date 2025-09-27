using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class UserWidget : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        public string WidgetTypeId { get; set; }
        public string DeviceTypeId { get; set; }
        public string WidgetCategoryId { get; set; }
        public string CreatedByUserId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
