using SmartLifeSolution.DAL.Dao.SubscriptionPlan;
using SmartLifeSolution.DAL.Dto.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.SubscriptionPlans
{
    public interface ISubscriptionPlanRepository
    {
        Task<List<SubscriptionPlanDao>> GetAll();
        Task<string> AddOrUpdate(SubscriptionPlanDto Dto);
        Task<bool> Delete(string Id);
        Task<SubscriptionPlanDao> Edit(string Id);
    }
}
