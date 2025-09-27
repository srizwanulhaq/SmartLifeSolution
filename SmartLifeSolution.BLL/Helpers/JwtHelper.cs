using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SmartLifeSolution.BLL.Helpers.JwtHelper;

namespace SmartLifeSolution.BLL.Helpers
{
    public class JwtHelper
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }


}
