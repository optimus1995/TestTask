using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        public UserApiController()
        {
            
        }
        [HttpGet("Getprofile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUserProfile()
        {
            var name = User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

            return Ok(new { name, email });
        }
        [HttpPatch("UpdateProfile")]
        [Authorize]
        public IActionResult UpdateProfile()
        {
            return Ok();
        }
    }
}
