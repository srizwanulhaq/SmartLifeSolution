using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.Menus;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.Menu;
using System.Security.Claims;


namespace SmartLifeSolution.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMenuRepository _repo;
        public MenuController(ApplicationDbContext context, 
            IMenuRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var data = _repo.GetAll();
            return ApiResponse(data);
        }

        [HttpPost("addorupdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] MenuDto Dto)
        {
            var data = _repo.AddOrUpdate(Dto);
            return ApiResponse(data);
        }

        [HttpGet("getmenusbyroleid")]
        public async Task<IActionResult> GetMenusByRoleId()
        {
            var data = _repo.GetMenusByRoleId(CurrentRoleId);
            return ApiResponse(data);
        }
   
        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] UserMenuDto Dto)
        {
            var data = _repo.Assign(Dto);
            return ApiResponse(data);
        }

    }
}
