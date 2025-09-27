using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.BLL.Repositories.GatewayDevices;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dao;
using SmartLifeSolution.DAL.Dao.GenericResponse;
using SmartLifeSolution.DAL.Dto;
using SmartLifeSolution.DAL.Dto.GatewayDevice;
using SmartLifeSolution.DAL.Dto.Paging;
using System.Text;

namespace SmartLifeSolution.Controllers
{
    //   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayDeviceController : BaseController
    {
        private readonly IGatewayDeviceRepository _repo;
        public GatewayDeviceController(IGatewayDeviceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAll();
            return ApiResponse(data);
        }

        [HttpGet("GetPagedData")]
        public async Task<IActionResult> GetPagedData([FromQuery] PagingDto Dto)
        {
            var data = await _repo.GetPagedData(Dto);
            return ApiResponse(data);
        }

        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Edit(string Id)
        {
            var data = await _repo.Edit(Id);
            return ApiResponse(data);
        }

        [HttpPost("addorupdate")]
        public async Task<ActionResult> AddOrUpdate([FromBody] DevicesDto Dto)
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
        public IActionResult GetDropdowns()
        {
            var data = _repo.GetDropdowns();
            return ApiResponse(data);
        }

        [HttpPost("downlink")]
        public async Task<ActionResult> Downlink([FromBody] DownlinkRequestDto request)
        {
            var data = await _repo.Downlink(request.DeviceEUI, request.IsTurnOn);
            return ApiResponse(data);
        }

        //[HttpPost("get")]
        //public async Task<ActionResult> get()
        //{
        //    using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
        //    string bodyText = await reader.ReadToEndAsync();

        //    var util = new EmailUtility();
        //    util.SendEmailAsync(bodyText);
        //    return ApiResponse(null);
        //}

    }

}
