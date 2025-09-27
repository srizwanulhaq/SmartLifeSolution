using SmartLifeSolution.DAL.Dao.Manufacturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao.Gateway
{
    public class GatewayDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DeviceEUI { get; set; }
        public string IPAddress { get; set; }
        public string ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }    
        public List<ManufacturerDao> ManufacturerList { get; set; }
    }

    public class GatewayDropdownsDao
    {
        public List<ManufacturerDao> ManufacturerList { get; set; }

    }
}
