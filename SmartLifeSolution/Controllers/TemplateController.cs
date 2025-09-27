using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.Templates;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dto.Templates;

namespace SmartLifeSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : BaseController
    {
        private readonly ITemplateRepository _templateRepository;

        public TemplateController(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        [HttpGet("getall")]
        public IActionResult GetAllTemplates()
        {
            var lstTemplates = _templateRepository.GetAllTemplates();
            return Ok(lstTemplates);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AddTemplateDto Dto)
        {
            var id = _templateRepository.Add(Dto);
            return Ok(id);
        }

        [HttpGet("get/{TemplateId}")]
        public IActionResult GetTemplateById(string TemplateId)
        {
            var objTemplate = _templateRepository.GetTemplateById(TemplateId);
            return ApiResponse(objTemplate);
        }


    }
}
