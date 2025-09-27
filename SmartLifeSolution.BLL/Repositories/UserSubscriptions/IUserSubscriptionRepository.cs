using SmartLifeSolution.DAL.Dto.UserSubscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.UserSubscriptions
{
    public interface IUserSubscriptionRepository
    {
        Task<string> Subscribe(UserSubscriptionDto Dto);
    }
}
