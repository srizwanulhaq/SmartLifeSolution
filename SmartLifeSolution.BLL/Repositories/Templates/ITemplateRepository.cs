using SmartLifeSolution.DAL.Dao.Templates;
using SmartLifeSolution.DAL.Dto.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Templates
{
    public interface ITemplateRepository
    {
        List<TemplateDao> GetAllTemplates();
        string Add(AddTemplateDto Dto);
        TemplateDetailDao GetTemplateById(string TemplateId);
    }
}
