using SmartLifeSolution.DAL.Models.BaseEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Models
{
    public class UserSubscription : BaseEntity
    {
        public string SubscriptionPlanId { get; set; }

        [ForeignKey("SubscriptionPlanId")]
        public SubscriptionPlan SubscriptionPlans { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string PaymentId { get; set; }

        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
    }
}
