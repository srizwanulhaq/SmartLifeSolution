using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class GatewayDevice : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceEUI { get; set; }
        public string? GatewayId { get; set; }
        public string? ApplicationId { get; set; }
        public string? ProfileId { get; set; }
        public string AppKey { get; set; }
        public string TurnOnCode { get; set; }
        public string TurnOffCode { get; set; }
        public string? ManufacturerId { get; set; }
       // public string RoomId { get; set; }
        public string CreatedByUserId { get; set; }

        [ForeignKey("GatewayId")]
        public Gateway Gateway { get; set; }

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }

        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }

        [ForeignKey("DeviceTypeId")]
        public DeviceTypes DeviceType { get; set; }

    }
}
