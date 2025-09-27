using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.Controllers.Base;
using SmartLifeSolution.DAL.Dto.Auth;
using SmartLifeSolution.DAL.Enums;
using SmartLifeSolution.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using System.Net.Http;
using Newtonsoft.Json;
using SmartLifeSolution.BLL.Repositories.Auths;
using Azure.Core;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Google.Apis.Auth.OAuth2;
using System.Numerics;
using System.Xml.Linq;

namespace SmartLifeSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;
        private readonly EmailUtility _emailUtility;
        private readonly IAuthRepository _authRepository;
        private readonly IWebHostEnvironment _env;
       // string bKey = "a8a64524-1f60-46a6-801e-70eaf1f647f4";
        public AuthController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            EmailUtility emailUtility, RoleManager<ApplicationRole> roleManager,
            IAuthRepository authRepository, HttpClient httpClient, 
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailUtility = emailUtility;
            _roleManager = roleManager;
            _authRepository = authRepository;
            _httpClient = httpClient;
            _env = env;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("Account already exists");

            var isSuccess = await _authRepository.Register(model);

            if (!isSuccess.Succeeded)
                return BadRequest("Invalid role");


            if (isSuccess.Succeeded)
                return Ok(new { Message = "Registered successfully" });

            return BadRequest(isSuccess.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound("No account found");
            }

            if (user.IsUserVerified != true)
            {
                // var verifyToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = string.Format("{0}/verifycode?email={1}&otpcode={2}",
                     BaseUrl, user.Email, user.OtpCode);

                var body = $"<p>{user.OtpCode} is your verification code, please enter it by clicking the link:</p> <a href='{confirmationLink}'>Click here</a>";

                await _emailUtility.SendEmailAsync("Smart Life Verification Code", user.Email, body);

                return Unauthorized(new
                {
                    Token = string.Empty,
                    IsEmailVerified = false,
                    Message = "We have sent a code, plz enter to complete login process"
                });
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            var token = await GenerateJwtToken(user);

            return ApiResponse(new { Token = token, IsEmailVerified = true });
        }

        [HttpPost("verifycode")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDto Dto)
        {
            var user = await _userManager.FindByEmailAsync(Dto.Email);

            if (user == null)
                return NotFound("User not found");

            if (user.OtpCode == Dto.OtpCode && Dto.OtpCode != null)
            {
                user.IsUserVerified = true;
                user.OtpCode = null;
                await _userManager.UpdateAsync(user);

                return Ok("Code verified successfully");
            }
            else
                return Unauthorized();
        }

        [HttpPost("SendCode/{email}")]
        public async Task<IActionResult> SendCode(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var confirmationLink = string.Format("{0}/verifycode?email={1}&otpcode={2}",
                    BaseUrl, user.Email, user.OtpCode);

            var body = $"<p>{user.OtpCode} is your account verification code, please enter it by clicking the link:</p> <a href='{confirmationLink}'>Click here</a><br/><p>Regards</p><p>SmartLife Team</p>";
            await _emailUtility.SendEmailAsync("Verify SmartLife Account", user.Email, body);

            return ApiResponse("Code sent successfully");
        }

        [HttpPost("SendResetLink/{email}")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            Random random = new Random();
            int code = random.Next(1000, 10000);

            user.OtpCode = code;
            await _userManager.UpdateAsync(user);

            var confirmationLink = string.Format("{0}/createnewpassword?Email={1}&OtpCode={2}",
                   BaseUrl, user.Email, user.OtpCode);

            var body = $"<p>please reset your password by clicking the link:</p> <a href='{confirmationLink}'>Click here</a><br/><p>Regards</p><p>SmartLife Team</p>";

            await _emailUtility.SendEmailAsync("Reset SmartLife Account Password", user.Email, body);

            return ApiResponse("Password reset link sent successfully");
        }

        [HttpPost("CreateNewPassword")]
        public async Task<IActionResult> CreateNewPassword([FromBody] CreatePasswordDto Dto)
        {
            var user = await _userManager.FindByEmailAsync(Dto.Email);

            if (user.OtpCode == Dto.OtpCode && user.OtpCode != null)
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, Dto.NewPassword);

                if (result.Succeeded)
                    return ApiResponse("Password created successfully");
            }

            return BadRequest("Reset password failed");
        }

        [HttpPost("googlelogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto request)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

            if (await _userManager.FindByEmailAsync(payload.Email) == null && payload.Email != null)
            {
                Random random = new Random();
                int code = random.Next(1000, 10000);

                var pwd = "Google" + code + "++";

                var user = new ApplicationUser
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    FullName = payload.Name + string.Empty,
                    FirstName = payload.Name,
                    LastName = string.Empty,
                    PasswordHash = pwd,
                    OtpCode = code,
                    IsUserVerified = true
                };

                var isSuccess = await _userManager.CreateAsync(user, pwd);
                var isRoleAdded = await _userManager.AddToRoleAsync(user, UserRoleEnums.USER.ToString());

                var body = $"<p>You have just registered using gmail account, your password is {pwd} you can also log in via credentials i.e email & password rather using Google Sign in option and you can also reset your password using forgot password link on smartlife platform</p><br/><p>Regards</p><p>SmartLife Team</p>";

                await _emailUtility.SendEmailAsync("SmartLife Account Created", payload.Email, body);
            }

            if (payload.Email != null)
            {
                var user = await _userManager.FindByEmailAsync((string)payload.Email);
                var token = await GenerateJwtToken(user);
                return ApiResponse(new { Token = token, IsEmailVerified = true });
            }

            return BadRequest("Login failed");
        }

        [HttpPost("githublogin")]
        public async Task<IActionResult> GitHubCallback([FromBody] GitHubTokenRequest Dto)
        {
            var clientId = "Ov23liGxGt3fpA3MnAFA";
            var clientSecret = "c1eb8d1cee8848144179a5de513339e96f078812";

            var tokenResponse = await _httpClient.PostAsync("https://github.com/login/oauth/access_token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "code", Dto.Code }
                }));

            var responseContent = await tokenResponse.Content.ReadAsStringAsync();
            var query = System.Web.HttpUtility.ParseQueryString(responseContent);
            var accessToken = query["access_token"];

            if (accessToken == null)
                return BadRequest("Failed to get GitHub token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("smartlife");

            var userResponse = await _httpClient.GetAsync("https://api.github.com/user");
            var userJson = await userResponse.Content.ReadAsStringAsync();

            var objUserJson = JsonDocument.Parse(userJson).RootElement;
            var fullName = objUserJson.GetProperty("name").GetString() ?? "";

            var userEmailResponse = await _httpClient.GetAsync("https://api.github.com/user/emails");
            var emailJson = await userEmailResponse.Content.ReadAsStringAsync();
            dynamic emails = JsonConvert.DeserializeObject(emailJson);
            string email = emails[0].email;

            if (await _userManager.FindByEmailAsync(email) == null && email != null)
            {
                Random random = new Random();
                int code = random.Next(1000, 10000);
                var pwd = "Github" + code + "++";
                
                var githubUserName = fullName ?? email.Split('@')[0];

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = githubUserName,
                    FirstName = githubUserName,
                    LastName = string.Empty,
                    PasswordHash = pwd,
                    IsUserVerified = true
                };

                var isSuccess = await _userManager.CreateAsync(user, pwd);
                var isRoleAdded = await _userManager.AddToRoleAsync(user, UserRoleEnums.USER.ToString());
                var body = $"<p>You have just registered using gmail account, your password is {pwd} you can also log in via credentials i.e email & password rather using Google Sign in option and you can also reset your password using forgot password link on smartlife platform</p><br/><p>Regards</p><p>SmartLife Team</p>";
                await _emailUtility.SendEmailAsync("SmartLife Account Created", email, body);
            }

            var objUser = await _userManager.FindByEmailAsync(email);
            var token = await GenerateJwtToken(objUser);
            return ApiResponse(new { Token = token, IsEmailVerified = true });
        }

        [HttpGet("getfile")]
        public async Task<IActionResult> GetFileInfo()
        {
            string filePath = Path.Combine(_env.WebRootPath, "dwg", "title_block-iso.dwg");


            // your DXF file name
           // string file = "sample.dxf";

            // create a new document, by default it will create an AutoCad2000 DXF version
            //DxfDocument doc = new DxfDocument();
            //// an entity
            //Line entity = new Line(new Vector2(5, 5), new Vector2(10, 5));
            //// add your entities here
            //doc.Entities.Add(entity);
            //// save to file
            //doc.Save(file);

            //// this check is optional but recommended before loading a DXF file
            //DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(file);
            //// netDxf is only compatible with AutoCad2000 and higher DXF versions
            //if (dxfVersion < DxfVersion.AutoCad2000) return;
            //// load file
            //DxfDocument loaded = DxfDocument.Load(file);

            var accessToken = await GetForgeAccessToken();
            var result = await CreateBucketAsync(accessToken);
            string bucketKey = JsonConvert.DeserializeObject<dynamic>(result).bucketKey;
            var fileName = Path.GetFileName(filePath);

            var objKeyResult = await GetUploadKey(accessToken, bucketKey, fileName);

            var url = objKeyResult.Urls[0];

           // var filePath1 = "part00"; // Path to your 10MB part file

            using var fileStream = System.IO.File.OpenRead(filePath);
            var content = new StreamContent(fileStream);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Headers.ContentLength = fileStream.Length;

            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("File part uploaded successfully.");
            }


             url = objKeyResult.Urls[1];

           //  filePath1 = "part01"; // Path to your 10MB part file

            using var fileStream2 = System.IO.File.OpenRead(filePath);
             content = new StreamContent(fileStream2);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Headers.ContentLength = fileStream.Length;

             request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = content
            };

             response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("File part uploaded successfully.");
            }

            // var uploadResult = await UploadFileAsync(accessToken, filePath, fileName, bucketKey);
            // URN is returned in the upload result JSON
            var urn = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{bucketKey}/{fileName}")).TrimEnd('=');

            var translateResult = await TranslateToSVFAsync(accessToken, urn);

            return Ok();
        }

        public async Task<string> CreateBucketAsync(string accessToken)
        {
            //var bucketKey = bKey;

           string bKey = System.Guid.NewGuid().ToString();

            var payload = new
            {
                bucketKey = bKey,
                policyKey = "transient"
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var res = await _httpClient.PostAsync("https://developer.api.autodesk.com/oss/v2/buckets", content);
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<string> UploadFileAsync(string accessToken, string filePath, string fileName, string bucket_key)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            var content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

           // var url2 = "https://developer.api.autodesk.com/oss/v2/buckets/{bucket_key}/objects/skyscpr1.3ds/signeds3upload?parts=2";
            var uploadUrl = $"https://developer.api.autodesk.com/oss/v2/buckets/{bucket_key}/objects/{fileName}"+ "/signeds3upload?parts=2";
            var response = await _httpClient.PutAsync(uploadUrl, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> TranslateToSVFAsync(string accessToken, string objectUrn)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var payload = new
            {
                input = new { urn = objectUrn },
                output = new
                {
                    formats = new[] {
                    new {
                        type = "svf",
                        views = new[] { "2d", "3d" }
                    }
                }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync("https://developer.api.autodesk.com/modelderivative/v2/designdata/job", content);
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<string> GetForgeAccessToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://developer.api.autodesk.com/authentication/v2/token");
            var client_id = "jrokdwEmdaDrVU7sEc7BGF2xFWAAy3jm0WGqxfhqW7f3jATj";
            var client_secret = "eN2PzyCRkHS6WEJTqRUZd1X7pDfGUIKD7yYOjH0uAnlpTUYyICbD5s3H0dZAhhsl";

            string credentials = $"{client_id}:{client_secret}";
            byte[] bytes = Encoding.UTF8.GetBytes(credentials);
            var base64 =  Convert.ToBase64String(bytes);

            // Set headers
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64);

            // Set form data
            var formData = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("scope", "code:all bucket:create bucket:read data:create data:write data:read")
            });

            request.Content = formData;

            // Send request
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            // Read response content
            var responseBody = await response.Content.ReadAsStringAsync();

            string? token =  (JsonConvert.DeserializeObject<dynamic>(responseBody)).access_token;
            return token;
        //    var url = "https://developer.api.autodesk.com/authentication/v1/authenticate";

        //    var client = new HttpClient();
        //    var requestBody = new Dictionary<string, string>
        //{
        //        { "client_id", "jrokdwEmdaDrVU7sEc7BGF2xFWAAy3jm0WGqxfhqW7f3jATj" },
        //{ "client_secret", "eN2PzyCRkHS6WEJTqRUZd1X7pDfGUIKD7yYOjH0uAnlpTUYyICbD5s3H0dZAhhsl" },
        //{ "grant_type", "client_credentials" },
        //{ "scope", "data:read data:write data:create bucket:create bucket:read" } // adjust as needed
        //    };

        //    var content = new FormUrlEncodedContent(requestBody);
        //    var response = await client.PostAsync(url, content);

        //    return "";
        //    //if (response.IsSuccessStatusCode)
        //    //{
        //    //    var json = await response.Content.ReadAsStringAsync();
        //    //    var tokenResponse = JsonSerializer.Deserialize<ForgeTokenResponse>(json);
        //    //    return tokenResponse.access_token;
        //    //}

        }

        private async Task<UploadResponse> GetUploadKey(string accessToken, string bukKey, string fileName)
        {
            var url = $"https://developer.api.autodesk.com/oss/v2/buckets/{bukKey}/objects/{fileName}/signeds3upload?parts=2";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                var objResult = JsonConvert.DeserializeObject<UploadResponse>(responseBody);

                //string uploadKey = objResult.UploadKey;
                //var lstUrls = objResult.Urls;

                return objResult;
            }
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var lstRole = await _userManager.GetRolesAsync(user);

            string roleId = string.Empty;
            string roleName = string.Empty;

            if (lstRole.Count() > 0)
            {
                roleName = lstRole.FirstOrDefault();
                var objRole = await _roleManager.FindByNameAsync(roleName);
                roleId = objRole.Id;
            }

            var claims = new[]
            {
            new Claim("roleid", roleId),
            new Claim("rolename", roleName),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(365),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

    public class UploadResponse
    {
        public string UploadKey { get; set; }
        public DateTime UploadExpiration { get; set; }
        public DateTime UrlExpiration { get; set; }
        public List<string> Urls { get; set; }
    }

}
