using Microsoft.EntityFrameworkCore;
using SmartLifeSolution.DAL.Dao.SubscriptionPlan;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.Subscription;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.SubscriptionPlans
{
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SubscriptionPlanDao>> GetAll()
        {
            var data = await _context.SubscriptionPlans
                .Select(x => new SubscriptionPlanDao
                {
                    Id = x.Id,
                    Name = x.Name,
                    Amount = x.Amount,
                }).ToListAsync();

            return data;
        }
        public async Task<string> AddOrUpdate(SubscriptionPlanDto Dto)
        {
            var objPlan = new SubscriptionPlan();

            if (string.IsNullOrEmpty(Dto.Id))
            {
                objPlan = new SubscriptionPlan
                {

                    Name = Dto.Name,
                    Number = _context.SubscriptionPlans.Count() + 1,
                    Amount = Dto.Amount,
                };

                _context.SubscriptionPlans.Add(objPlan);
                await _context.SaveChangesAsync();
            }
            else
            {
                objPlan = _context.SubscriptionPlans.FirstOrDefault(x => x.Id == Dto.Id); ;
                objPlan.Name = Dto.Name;
                objPlan.Amount = Dto.Amount;
                _context.SubscriptionPlans.Update(objPlan);
            }

            await _context.SaveChangesAsync();

            return objPlan.Id;
        }
        public async Task<SubscriptionPlanDao> Edit(string Id)
        {
            var objPlan = await _context.SubscriptionPlans
                .Where(x => x.Id == Id).Select(x => new SubscriptionPlanDao
                {
                    Id = x.Id,
                    Name = x.Name,
                    Amount = x.Amount,
                }).FirstOrDefaultAsync();

            return objPlan;
        }
        public async Task<bool> Delete(string Id)
        {
            var objPlan = _context.SubscriptionPlans.FirstOrDefault(y => y.Id == Id);

            if (objPlan != null)
            {
                objPlan.Active = false;
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
