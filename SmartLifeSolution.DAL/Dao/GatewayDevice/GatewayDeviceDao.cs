using Azure.Identity;
using SmartLifeSolution.DAL.Dto.Manufacturer;
using SmartLifeSolution.DAL.Dto.Profile;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao.GatewayDevices
{
    public class GetAllDeviceDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceEUI { get; set; }
        public string AppKey { get; set; }
        public string TurnOnCode { get; set; }
        public string TurnOffCode { get; set; }
        public string GatewayName { get; set; }
        public string ProfileName { get; set; }
        public string ManufacturerName { get; set; }
        public string ApplicationName { get; set; }
    }

    public class GetDeviceDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceEUI { get; set; }
        public string AppKey { get; set; }
        public string TurnOnCode { get; set; }
        public string TurnOffCode { get; set; }
        public string GatewayName { get; set; } 
        public string GatewayId { get; set; }
        public string ProfileId { get; set; }
        public string ManufacturerId { get; set; }
        public string ApplicationId { get; set; }

        public object GatewayList { get; set; }

        public object ManufacturerList { get; set; }

        public object ApplicationList { get; set; }

        public object ProfileList { get; set; }

    }

    public class DeviceDropdownDao
    {
        public object GatewayList { get; set; }

        public object ManufacturerList { get; set; }

        public object ApplicationList { get; set; }

        public object ProfileList { get;set; }

    }

    
}
