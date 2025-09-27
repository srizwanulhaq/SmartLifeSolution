using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.GatewayDevice
{

   
    public class DevicesDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceEUI { get; set; }
        public string GatewayId { get; set; }
        public string ApplicationId { get; set; }
        public string ProfileId { get; set; }
        public string AppKey { get; set; }
        public string TurnOnCode { get; set; }
        public string TurnOffCode { get; set; }
        public string ManufacturerId { get; set; }
      
    }


    public class GatewayDeviceDto
    {
        public string type { get; set; }
        public string id { get; set; }
        public string method { get; set; }
        public string url { get; set; }
        public DeviceBodyDto body { get; set; }
    }

    public class DeviceBodyDto
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? devEUI { get; set; }
        public string? profileID { get; set; }
        public int fPort { get; set; }
        public bool skipFCntCheck { get; set; }
        public string? appKey { get; set; }
        public string? applicationID { get; set; }
        public string? payloadCodecID { get; set; }
    }

    public class DownlinkRequestDto
    {
        public string DeviceEUI { get; set; }
        public bool IsTurnOn { get; set; }
       // public string hexStr { get; set; }
    }


}
