using Microsoft.AspNetCore.Identity;
using SmartLifeSolution.DAL.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Auths
{
    public interface IAuthRepository
    {
        Task<IdentityResult> Register(RegisterDto model);
    }
}
