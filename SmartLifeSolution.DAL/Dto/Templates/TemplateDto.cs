using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.Templates
{
    public class AddTemplateDto
    {
        public string Name { get; set; } 
        public List<TemplateWidgetsDto> TemplateWidgets { get; set; }
    }

    public class TemplateWidgetsDto
    {
        public string WidgetId { get; set; }
    }
}
