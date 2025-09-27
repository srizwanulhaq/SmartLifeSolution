using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.Dashboards;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dao;
using SmartLifeSolution.DAL.Dto;
using System.Security.Claims;

namespace SmartLifeSolution.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly IDashboardRepository _repo;

        public DashboardController(IDashboardRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("userdashboard")]
        public async Task<ActionResult> GetUserDashboard()
        {
            //var data = _repo.GetUserDashboard(CurrentUserId);
            return ApiResponse("");
        }

        [HttpGet("details/{DashboardId}")]
        public async Task<ActionResult> GetDashboardDetails(string DashboardId)
        {
            //var data = _repo.GetDashboardDetailsById(DashboardId);
             return ApiResponse("");
        }



    }


}
