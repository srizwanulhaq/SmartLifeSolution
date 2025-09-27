using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class GatewayDeviceData : BaseEntity
    {
        public string JsonData { get; set; }
        public string GatewayDeviceId { get; set; }

        [ForeignKey("GatewayDeviceId")]
        public GatewayDevice GatewayDevice { get; set; }        
    }
}
