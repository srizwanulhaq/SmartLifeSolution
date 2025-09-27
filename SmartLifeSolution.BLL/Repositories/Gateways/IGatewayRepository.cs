using SmartLifeSolution.DAL.Dao.Gateway;
using SmartLifeSolution.DAL.Dao.GatewayDevices;
using SmartLifeSolution.DAL.Dto.Gateway;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Gateways
{
    public interface IGatewayRepository
    {
        Task<string> AddOrUpdate(GatewayDto Dto);
        Task<List<GatewayDao>> GetAll();
        Task<GatewayDao> Edit(string Id);
        List<GetDeviceDao> GetDevicesByGatewayId(string gatewayId);
        Task<bool> Delete(string Id);
        GatewayDropdownsDao GetDropdowns();

    }
}
