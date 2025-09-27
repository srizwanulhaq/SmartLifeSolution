
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? OtpCode { get; set; }
        public bool? IsUserVerified { get; set; } 
    }
}
