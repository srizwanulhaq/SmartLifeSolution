using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.UserDashboards
{
    public class AddDashboardDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? TemplateId { get; set; }
        public string? DashboardImage { get; set; }
        public string? UserId { get; set; }

    }

    public class UserDashboardWidgetDto
    {
        public string UserDashboardId { get; set; }
        public string UserWidgetId { get; set; }
    }

    public class AddWidgetTypeDto
    {
        public string WidgetTypeName { get; set; }
    }

    public class  AddWidgetCategoryDto
    {
        public string WidgetCategoryName { get; set; }

    }


}
