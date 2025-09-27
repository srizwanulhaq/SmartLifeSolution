using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Helpers
{
    public class PaymentDto
    {
        [Required]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        [Required]
        public long Amount { get; set; }

        [Required]
        public string Currency { get; set; }
    }
    
}
