using SmartLifeSolution.DAL.Dao.GatewayDevices;
using SmartLifeSolution.DAL.Dto.GatewayDevice;
using SmartLifeSolution.DAL.Dto.Paging;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.GatewayDevices
{
    public interface IGatewayDeviceRepository
    {
        Task<List<GetAllDeviceDao>> GetAll();
        Task<PagingDao<GetAllDeviceDao>> GetPagedData(PagingDto Dto);
        Task<GetDeviceDao> Edit(string Id);
        Task<bool> AddOrUpdate(DevicesDto addDeviceDto);
        Task<bool> Delete(string Id);
        DeviceDropdownDao GetDropdowns();
        Task<bool> Downlink(string deviceEUI, bool IsTurnOn);
    }
}
