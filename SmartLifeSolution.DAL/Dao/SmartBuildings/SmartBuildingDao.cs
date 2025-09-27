using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao.SmartBuildings
{
    public class SmartBuildingDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class FloorDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
    }

    public class RoomDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
    }

    public class ApplianceDao
    {
        public string ApplianceId { get; set; }
        public string ApplianceName { get; set; }
        public string FloorName { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedDate { get; set; }
    }



}
