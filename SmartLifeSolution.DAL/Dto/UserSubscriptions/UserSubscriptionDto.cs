using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.UserSubscription
{
    public class UserSubscriptionDto
    {
        public string SubscriptionPlanId { get; set; }
        public string? UserId { get; set; }
    }
}
