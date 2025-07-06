using Applicatiom.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace UserAuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        public readonly IMediator _mediatr;
        public AuthApiController(IMediator mediatr)
        {
            _mediatr = mediatr;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediatr.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            { throw ex; }
            
            
            }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _mediatr.Send(request);
            return Ok(result);
        }

    }
}
