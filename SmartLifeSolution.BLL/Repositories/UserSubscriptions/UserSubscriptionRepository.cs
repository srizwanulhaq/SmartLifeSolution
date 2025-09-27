using SmartLifeSolution.DAL.Constants;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.UserSubscription;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.UserSubscriptions
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly StripePayment _stripePayment;
        public UserSubscriptionRepository(ApplicationDbContext context, StripePayment stripePayment)
        {
            _context = context;
            _stripePayment = stripePayment;
        }

        public async Task<string> Subscribe(UserSubscriptionDto Dto)
        {
            var objPlan = _context.SubscriptionPlans.FirstOrDefault(x => x.Id == Dto.SubscriptionPlanId);

            var objSession = _stripePayment.CreateCheckoutSession(objPlan.Amount, CurrencyCodes.USD, objPlan.Name);

            if (!string.IsNullOrEmpty(objSession.Id))
            {
                var objPayment = new Payment
                {
                    Amount = objPlan.Amount,
                    Currency = CurrencyCodes.USD,
                    SessionId = objSession.Id,
                    IsPaid = false,
                };

                _context.Payments.Add(objPayment);
                await _context.SaveChangesAsync();

                var objUserSubscription = new UserSubscription
                {
                    UserId = Dto.UserId,
                    SubscriptionPlanId = Dto.UserId,
                    PaymentId = objPayment.Id,
                };

                _context.UserSubscriptions.Add(objUserSubscription);
                await _context.SaveChangesAsync();

                return objSession.Url;
            }
            else
                return null;
        }


    }
}
