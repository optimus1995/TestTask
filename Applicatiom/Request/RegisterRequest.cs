using Applicatiom.Response;
using MediatR;

namespace Applicatiom.Request;

public class RegisterRequest : IRequest<RegisterResponse>
{
    public string Email { get; set; }

    public string Name { get; set; }
    public string Password { get; set; }
}