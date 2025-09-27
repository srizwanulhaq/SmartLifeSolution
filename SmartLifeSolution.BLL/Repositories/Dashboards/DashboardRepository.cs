using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.Dashboard;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Dashboards
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public void AddDashboard(AddDashboardDto dashboardDto)
        //{
        //    var objDashboard = new UserDashboard
        //    {
        //        Name = dashboardDto.Name,
        //        //Number = _context.Dashboards.Count() + 1,
        //    };

        //    _context.UserDashboards.Add(objDashboard);
        //    _context.SaveChanges();
        //}

        //public void AddDashboardWidget(WidgetDto widgetDto)
        //{
        //    var objWidget = new UserDashboardWidget
        //    {
        //        Name = widgetDto.Name,
        //        DashboardId = widgetDto.DashboardId,
        //      //  Number = _context.DashboardWidgets.Count() + 1,
        //    };

        //    _context.DashboardWidgets.Add(objWidget);
        //    _context.SaveChanges();

        //    var lstWidgets = new List<DashboardWidget>();

        //    var count = _context.DashboardWidgetDetails.Count();

        //    foreach (var item in widgetDto.Details)
        //    {
        //        var lstWidgetDetails = new List<DashboardWidgetDetails>();

        //        lstWidgetDetails.Add(new DashboardWidgetDetails
        //        {
        //            DashboardWidgetId = objWidget.Id,
        //            Name = item.Name,
        //            Value = item.Value,
        //            Number = count++
        //        });

        //        _context.DashboardWidgetDetails.AddRange(lstWidgetDetails);
        //    }

        //    _context.DashboardWidgets.AddRange(lstWidgets);
        //    _context.SaveChanges();
        //}

        //public List<DashboardDao> GetUserDashboard(string UserId)
        //{
        //    var data = _context.Dashboards.Select(a => new DashboardDao
        //    {
        //        DashboardId = a.Id,
        //        DashboardName = a.Name,
        //        DashboardNumber = a.Number,
        //        Widgets = _context.UserDashboards.Include(d => d.Dashboard)
        //         .Include(dw => dw.DashboardWidget)
        //          .Where(x => x.UserId == UserId && x.DashboardId == a.Id)
        //         .Select(y => new DashboardWidgetsDao
        //         {
        //             WidgetId = y.DashboardWidget.Id,
        //             WidgetName = y.DashboardWidget.Name,
        //             WidgetNumber = y.DashboardWidget.Number,
        //             WidgetDetails = _context.DashboardWidgetDetails
        //             .Where(z => z.DashboardWidgetId == y.DashboardWidget.Id)
        //             .Select(w => new WidgetDetailDto
        //             {
        //                 Name = w.Name,
        //                 Value = w.Value
        //             }).ToList()
        //         }).ToList()
        //    }).ToList();

        //    return data;
        //}

        //public List<DashboardDao> GetAllDashboards()
        //{
        //    return _context.Dashboards.Select(x => new DashboardDao
        //    {
        //        DashboardNumber = x.Number,
        //        DashboardName = x.Name
        //    }).ToList();
        //}

        //public DashboardDao GetDashboardDetailsById(string DashboardId)
        //{
        //    var data = _context.Dashboards.Where(m => m.Id == DashboardId)
        //        .Select(x => new DashboardDao
        //        {
        //            DashboardId = x.Id,
        //            DashboardNumber = x.Number,
        //            DashboardName = x.Name,
        //            Widgets = _context.DashboardWidgets
        //        .Where(y1 => y1.DashboardId == x.Id)
        //        .Select(y => new DashboardWidgetsDao
        //        {
        //            WidgetId = y.Id,
        //            WidgetNumber = y.Number,
        //            WidgetName = y.Name,
        //            WidgetDetails = _context.DashboardWidgetDetails
        //             .Where(z1 => z1.DashboardWidgetId == y.Id)
        //             .Select(z => new WidgetDetailDto
        //             {
        //                 Name = z.Name,
        //                 Value = z.Value,
        //                 Number = z.Number
        //             }).ToList()
        //        }).ToList()

        //        }).FirstOrDefault();

        //    return data;
        //}

        //public void SaveUserDashboard(string userId, AddUserDashboardDto model)
        //{
        //    var lstUserDashboard = new List<UserDashboard>();

        //    model.WidgetIds.ForEach(widgetid =>
        //    lstUserDashboard.Add(new UserDashboard
        //    {
        //        DashboardId = model.DashboardId,
        //        DashboardWidgetId = widgetid,
        //        UserId = userId,
        //    }));

        //    _context.UserDashboards.AddRange(lstUserDashboard);
        //    _context.SaveChanges();
        //}

    }
}
