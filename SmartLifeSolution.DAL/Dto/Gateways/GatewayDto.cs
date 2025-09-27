using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.Gateway
{

    public class GatewayDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public string ManufacturerId { get; set; }
        public string DeviceEUI { get; set; }
    }
}
