using SmartLifeSolution.DAL.Dto.Appliances;
using SmartLifeSolution.DAL.Dto.SmartBuildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.SmartBuildings
{
    public interface ISmartBuildingRepository
    {
        Task<string> AddBuilding(SmartBuildingDto Dto);
        Task<string> AddFloor(FloorDto Dto);
        Task<string> AddRoom(RoomDto Dto);
        Task<string> AddAppliances(ApplianceDto Dto);

    }
}
