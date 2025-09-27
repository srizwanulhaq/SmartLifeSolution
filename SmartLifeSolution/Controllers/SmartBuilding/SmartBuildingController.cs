using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Repositories.SmartBuildings;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dto.Appliances;
using SmartLifeSolution.DAL.Dto.SmartBuildings;

namespace SmartLifeSolution.Controllers.SmartBuilding
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartBuildingController : BaseController
    {
        private readonly ISmartBuildingRepository _repo;
        public SmartBuildingController(ISmartBuildingRepository repo)
        {
            _repo = repo;
        }


        [HttpPost("addbuilding")]
        public async Task<IActionResult> AddBuilding([FromBody] SmartBuildingDto Dto)
        {
            var data = await _repo.AddBuilding(Dto);
            return ApiResponse(data);
        }

        [HttpPost("addfloor")]
        public async Task<IActionResult> AddFloor([FromBody] FloorDto Dto)
        {
            var data = await _repo.AddFloor(Dto);
            return ApiResponse(data);
        }

        [HttpPost("addroom")]
        public async Task<IActionResult> AddRoom([FromBody] RoomDto Dto)
        {
            var data = await _repo.AddRoom(Dto);
            return ApiResponse(data);
        }

        [HttpPost("addappliances")]
        public async Task<IActionResult> AddAppliances([FromBody] ApplianceDto Dto)
        {
            var data = await _repo.AddAppliances(Dto);    
            return ApiResponse(data);
        }




    }
}
