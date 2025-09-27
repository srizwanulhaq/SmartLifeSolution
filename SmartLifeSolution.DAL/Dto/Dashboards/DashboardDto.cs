using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.Dashboard
{

    public class AddDashboardDto
    {
        public string Name { get; set; }
    }

    public class WidgetDto
    {
        public string DashboardId { get; set; }
        public string Name { get; set; }
        public List<WidgetDetailDto> Details { get; set; }
    }

    public class WidgetDetailDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Number { get; set; }
    }

    public class AddUserDashboardDto
    {
        public string DashboardId { get; set; }
        public List<string> WidgetIds { get; set; }
    }


}
