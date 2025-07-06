using MediatR;
using Applicatiom.Response;
namespace Applicatiom.Request;
public class LoginRequest : IRequest<LoginResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
