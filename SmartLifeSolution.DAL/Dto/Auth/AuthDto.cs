using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.Auth
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? OtpCode { get; set; }
    }

    public class VerifyCodeDto
    {
        public string Email { get; set; }
        public int? OtpCode { get; set; }
    }

    public class CreatePasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public int OtpCode { get; set; }
    }

    public class GoogleLoginDto
    {
        public string IdToken { get; set; }
    }

    public class GitHubTokenRequest
    {
        public string Code { get; set; }
    }


}
