using Microsoft.EntityFrameworkCore;
using SmartLifeSolution.DAL.Dao.Templates;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.Templates;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Templates
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public TemplateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TemplateDao> GetAllTemplates()
        {
            var lstTemplates = _context.Templates.Select(x => new TemplateDao
            {
                TemplateId = x.Id,
                TemplateName = x.Name,
                TemplateNumber = x.Number
            }).ToList();

            return lstTemplates;
        }

        public string Add(AddTemplateDto Dto)
        {
            var objTemplate = new DAL.Models.Template
            {
                Name = Dto.Name
            };

            _context.Templates.Add(objTemplate);
            _context.SaveChanges();

            var lstTemplateWidgets = new List<TemplateWidget>();

            foreach(var widget in Dto.TemplateWidgets)
            {
               lstTemplateWidgets.Add(new TemplateWidget
                {
                    TemplateId = objTemplate.Id,
                    WidgetId = widget.WidgetId
                });
            }

            if (lstTemplateWidgets.Count > 0)
            {
                _context.TemplateWidgets.AddRange(lstTemplateWidgets);
                _context.SaveChanges();
            }

            return objTemplate.Id;
        }

        public TemplateDetailDao GetTemplateById(string TemplateId)
        {
            var objTemplate = _context.Templates
                .Where(x => x.Id == TemplateId)
                .Select(x => new TemplateDetailDao
                {
                    TemplateId = x.Id,
                    TemplateName = x.Name,
                    Widgets = _context.TemplateWidgets
                       .Include(x => x.Widget)
                       .Select(y => new TemplateWidgetDao
                       {
                           WidgetId = y.Id,
                           WidgetName = y.Widget.Name,
                           WidgetNumber = y.Widget.Number
                       }).ToList()
                }).FirstOrDefault();

            return objTemplate;
        }

        public void AssignDevice()
        {

        }



    }
}
