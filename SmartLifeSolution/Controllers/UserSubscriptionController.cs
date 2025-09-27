using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.UserSubscriptions;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dto.UserSubscription;

namespace SmartLifeSolution.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : BaseController
    {
        private readonly IUserSubscriptionRepository _repo;
        public UserSubscriptionController(IUserSubscriptionRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] UserSubscriptionDto Dto)
        {
            Dto.UserId = CurrentUserId;

            var data = await _repo.Subscribe(Dto);

            if (!string.IsNullOrEmpty(data))
                return ApiResponse(data);
            else
                return BadRequest("Error occurred");
        }
    }
}
