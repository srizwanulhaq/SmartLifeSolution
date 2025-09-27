using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.SmartBuildings
{
    public class SmartBuildingDto
    {
        public string Name { get; set; }
    }

    public class FloorDto
    {
        public string Name { get; set; }
        public string BuildingId { get; set; }
    }

    public class RoomDto
    {
        public string Name { get; set; }
        public string FloorId { get; set; }
    }

}
