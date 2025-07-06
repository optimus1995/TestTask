using Applicatiom.Request;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserDomain;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        public readonly IMediator _mediator;
        public UserApiController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet("Getprofile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUserProfile()
        {
            try
            {
                var name = User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
                var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? User.FindFirst("email")?.Value ?? User.FindFirst(ClaimTypes.Email)?.Value;

                return Ok(new { name, email });
            }
            catch (Exception ex)
            { throw new BadRequestException("Bad Request"); }
        }
        [HttpPatch("UpdateProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateProfile(UpdateUserDetailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Email == "" || request.Email == null)
                {
                    request.Email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? User.FindFirst("email")?.Value ?? User.FindFirst(ClaimTypes.Email)?.Value;

                }
                var result = _mediator.Send(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Bad Request");
            }
            return Ok();
        }
    }
}
