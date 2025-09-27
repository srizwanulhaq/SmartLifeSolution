using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.UserDashboards;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dto.UserDashboards;

namespace SmartLifeSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDashboardController : BaseController
    {
        private readonly IUserDashboardRepository _userDashboardRepository;

        public UserDashboardController(IUserDashboardRepository userDashboardRepository)
        {
            _userDashboardRepository = userDashboardRepository;
        }

        [HttpGet("getall")]
        public IActionResult GetAllDashboards()
        {
            var lstDashboards = _userDashboardRepository.GetAllDashboards();
            return ApiResponse(lstDashboards);
        }

        [HttpGet("get/{DashboardId}")]
        public IActionResult GetDashboardById(string DashboardId)
        {
            var objDashboard = _userDashboardRepository.GetUserDashboardById(DashboardId);
            return ApiResponse(objDashboard);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AddDashboardDto Dto)
        {
            Dto.UserId = CurrentUserId;
            var id = _userDashboardRepository.AddUserDashboard(Dto);
            return Ok(id);
        }

        [HttpGet("delete")]
        public IActionResult Delete(string DashboardId)
        {
            var result = _userDashboardRepository.DeleteDashboard(DashboardId);
            return ApiResponse(result);
        }

        [HttpPost("addwidget")]
        public async Task<IActionResult> AddWidget([FromBody] AddWidgetDto Dto)
        {



            return ApiResponse("Success");
        }

    }
}
