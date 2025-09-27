using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.Gateways;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dto.Gateway;

namespace SmartLifeSolution.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : BaseController
    {
        private readonly IGatewayRepository _repo;
        public GatewayController(IGatewayRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAll();
            return ApiResponse(data);
        }

        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Edit(string Id)
        {
            var data = await _repo.Edit(Id);
            return ApiResponse(data);
        }

        [HttpPost("addorupdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] GatewayDto Dto)
        {
            var data = await _repo.AddOrUpdate(Dto);
            return ApiResponse(data);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var data = await _repo.Delete(Id);
            return ApiResponse(data);
        }

        [HttpGet("dropdowns")]
        public IActionResult Dropdowns()
        {
            var data = _repo.GetDropdowns();
            return ApiResponse(data);
        }

        [HttpGet("getdevices/{Id}")]
        public IActionResult GetDevices(string Id)
        {
            var data = _repo.GetDevicesByGatewayId(Id);
            return ApiResponse(data);
        }
    
    }
}
