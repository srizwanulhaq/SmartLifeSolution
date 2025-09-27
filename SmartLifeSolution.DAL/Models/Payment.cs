using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{

    public class Payment : BaseEntity
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public string SessionId { get; set; }
    }
}
