using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartLifeSolution.DAL.Dao.DevicesData;
using SmartLifeSolution.DAL.Dao.UserDashboards;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.UserDashboards;
using SmartLifeSolution.DAL.Enums;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.UserDashboards
{
    public class UserDashboardRepository : IUserDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        public UserDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserDashboardDao> GetAllDashboards()
        {
            var lstDashboards = _context.UserDashboards.Include(x => x.User)
                .Where(x=> x.Active).Select(x => new UserDashboardDao
                {
                    UserDashboardId = x.Id,
                    UserDashboardName = x.Name,
                    Description = x.Description,
                    CreatedBy = x.User.FirstName,
                    LastModified = x.UpdatedDate
                }).ToList();

            return lstDashboards;
        }
        public string AddUserDashboard(AddDashboardDto Dto)
        {
            var objDashboard = new UserDashboard
            {
                Name = Dto.Name,
                TemplateId = !string.IsNullOrEmpty(Dto.TemplateId) ? Dto.TemplateId : null,
                UserId = Dto.UserId,
                DashboardImage = Dto.DashboardImage
            };

            _context.UserDashboards.Add(objDashboard);
            _context.SaveChanges();

            if (!string.IsNullOrEmpty(Dto.TemplateId))
            {
                var objDashboardWidgets = new List<UserDashboardWidget>();

                var lstTemplateWidgets = _context.TemplateWidgets
                    .Where(x => x.TemplateId == Dto.TemplateId).ToList();

                foreach (var widget in lstTemplateWidgets)
                {
                    objDashboardWidgets.Add(new UserDashboardWidget
                    {
                        WidgetId = widget.Id,
                        UserDashboardId = objDashboard.Id,
                    });
                }

                if (objDashboardWidgets.Count > 0)
                {
                    _context.UserDashboardWidgets.AddRange(objDashboardWidgets);
                    _context.SaveChanges();
                }
            }

            return objDashboard.Id;
        }
        public void AddDashboardWidgets(UserDashboardWidgetDto Dto)
        {
            var objDashboardWidget = new UserDashboardWidget
            {
                UserDashboardId = Dto.UserDashboardId,
                WidgetId = Dto.WidgetId,
            };

            _context.UserDashboardWidgets.Add(objDashboardWidget);
            _context.SaveChanges();
        }

        public UserDashboardDetailDao? GetUserDashboardById(string DashboardId)
        {
           return _context.UserDashboards
                .Where(x => x.Id == DashboardId)
                .Select(x => new UserDashboardDetailDao
                {
                    DashboardId = x.Id,
                    DashboardName = x.Name,
                    Widgets = _context.UserDashboardWidgets
                    .Include(x => x.Widget)
                    .Include(x=> x.GatewayDevice)
                    .Where(x=> x.UserDashboardId == DashboardId)
                       .Select(y => new UserDashboardWidgetDao
                       {
                           WidgetId = y.Id,
                           WidgetName = y.Widget.Name,
                           WidgetNumber = y.Widget.Number,
                           DeviceData = GetDeviceData(y.GatewayDevice.Id)
                       }).ToList()
                }).FirstOrDefault();
        }

        public bool DeleteDashboard(string DashboardId)
        {
            var objDashboard = _context.UserDashboards.FirstOrDefault(x => x.Id == DashboardId);
            objDashboard.Active = false;
            _context.UserDashboards.Update(objDashboard);
            _context.SaveChanges();
            return true;
        }

        public void AddWidget()
        {
          

        }

        private object GetDeviceData(string DeviceId)
        {
           var objData =  _context.GatewayDeviceData
                .Include(x=> x.GatewayDevice).ThenInclude(x=> x.DeviceType)
                .Where(x => x.GatewayDeviceId == DeviceId).FirstOrDefault();

            if ((int)DeviceTypeEnums.SENSOR == objData.GatewayDevice.DeviceType.Number)
            {
                var data = JsonConvert.DeserializeObject<SensorDataDao>(objData.JsonData);
                return data;
            }

            return null;
        }


    }
}
