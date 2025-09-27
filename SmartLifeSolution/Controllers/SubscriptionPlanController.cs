using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.SubscriptionPlans;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dao.SubscriptionPlan;
using SmartLifeSolution.DAL.Dto.Subscription;

namespace SmartLifeSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : BaseController
    {
        private readonly ISubscriptionPlanRepository _repo;
        public SubscriptionPlanController(ISubscriptionPlanRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAll();
            return ApiResponse(data);
        }

        [HttpPost("addorupdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] SubscriptionPlanDto Dto)
        {
            var data = await _repo.AddOrUpdate(Dto);
            return ApiResponse(data);
        }

        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Edit(string Id)
        {
            var data = await _repo.Edit(Id);
            return ApiResponse(data);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var data = await _repo.Delete(Id);
            return ApiResponse(data);
        }

    }
}
