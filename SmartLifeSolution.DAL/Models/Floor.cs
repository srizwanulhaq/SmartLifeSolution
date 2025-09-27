using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class Floor : BaseEntity
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string BuildingId { get; set; }
        public string CreatedByUserId { get; set; }

        [ForeignKey("BuildingId")]
        public SmartBuilding SmartBuilding { get; set; }
    }

}
