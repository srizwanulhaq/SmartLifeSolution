using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao.UserDashboards
{
    public class UserDashboardDao
    {
        public string UserDashboardId { get; set; }
        public string UserDashboardName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
    public class UserDashboardDetailDao
    {
        public string DashboardId { get; set; }
        public string DashboardName { get; set; }
        public List<UserDashboardWidgetDao> Widgets { get; set; }
    }

    public class UserDashboardWidgetDao
    {
        public string WidgetId { get; set; }
        public string WidgetName { get; set; }  
        public int WidgetNumber { get; set; }
        public object DeviceData { get; set; }
    }


}
