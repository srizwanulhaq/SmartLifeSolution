using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public string FloorId { get; set; }

        [ForeignKey("FloorId")]
        public Floor Floors { get; set; }

        public string CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
