using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao.Templates
{
    public class TemplateDao
    {
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }
        public int TemplateNumber { get; set; }
    }
    public class TemplateDetailDao
    {
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }
        public List<TemplateWidgetDao> Widgets { get; set; }
    }
    public class TemplateWidgetDao
    {
        public string WidgetId { get; set; }
        public string WidgetName { get; set; }
        public int WidgetNumber { get; set; }
    }

}
