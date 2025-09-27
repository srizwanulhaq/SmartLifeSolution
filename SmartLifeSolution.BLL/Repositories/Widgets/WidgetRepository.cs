using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.UserDashboards;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Widgets
{
    public class WidgetRepository : IWidgetRepository
    {
        private readonly ApplicationDbContext _context;
        public WidgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddWidgetType(AddWidgetTypeDto Dto)
        {
            var objWidgetType = new WidgetType
            {
                Name = Dto.WidgetTypeName,
                Number = _context.WidgetTypes.Count() + 1
            };

            _context.WidgetTypes.Add(objWidgetType);
            _context.SaveChanges();
        }

        public void AddWidgetCategory(AddWidgetCategoryDto Dto)
        {
            var objWidgetCategory = new WidgetCategory
            {
                Name = Dto.WidgetCategoryName,
                Number = _context.WidgetCategories.Count() + 1
            };

            _context.WidgetCategories.Add(objWidgetCategory);
            _context.SaveChanges();
        }




    }
}
