using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartLifeSolution.DAL.Dao;
using SmartLifeSolution.DAL.Dao.GenericResponse;
using SmartLifeSolution.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SmartLifeSolution.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    
        protected string BaseUrl
        {
            get
            {
                return $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            }
        }
        protected string CurrentUserId
        {
            get
            {
               return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        public string CurrentRoleName
        {
            get
            {
               return User.FindFirst("rolename")?.Value;
            }

        }

        public string CurrentRoleId
        {
            get
            {
                return User.FindFirst("roleid")?.Value;
            }

        }

        protected ActionResult ApiResponse(object result, string message = "Success", int statusCode = 200)
        {
            var response = new GenericResponseDao
            {
                Message = message,
                data = result,
                IsError = false,
                StatusCode = statusCode
            };

            return StatusCode(statusCode, response);
        }
    }


}

