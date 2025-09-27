using SmartLifeSolution.DAL.Dao.UserDashboards;
using SmartLifeSolution.DAL.Dto.UserDashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.UserDashboards
{
    public interface IUserDashboardRepository
    {
        List<UserDashboardDao> GetAllDashboards();
        string AddUserDashboard(AddDashboardDto Dto);
        void AddDashboardWidgets(UserDashboardWidgetDto Dto);
        UserDashboardDetailDao? GetUserDashboardById(string DashboardId);
        bool DeleteDashboard(string DashboardId);
    }
}
