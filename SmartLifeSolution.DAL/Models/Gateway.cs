using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class Gateway : BaseEntity
    {
        public string Name { get; set; }
        public string DeviceEUI { get; set; }
        public string IPAddress { get; set; }
        public string ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
        
    }
}
