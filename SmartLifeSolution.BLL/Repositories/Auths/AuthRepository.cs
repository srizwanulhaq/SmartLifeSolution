using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.DAL.Dto.Auth;
using SmartLifeSolution.DAL.Enums;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Auths
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IConfiguration _configuration;
        private readonly EmailUtility _emailUtility;

        public AuthRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration,
            EmailUtility emailUtility, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailUtility = emailUtility;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FirstName + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = model.Password,
                OtpCode = code,
                IsUserVerified = false
            };

            var isSuccess = await _userManager.CreateAsync(user, model.Password);

            var isRoleAdded = await _userManager.AddToRoleAsync(user, UserRoleEnums.USER.ToString());

            return isRoleAdded;
        }

    }


}
