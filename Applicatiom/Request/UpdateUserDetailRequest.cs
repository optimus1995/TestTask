using Applicatiom.Response;
using MediatR;

namespace Applicatiom.Request;

public class UpdateUserDetailRequest : IRequest<UpdateUserDetailResponse>
{
    public string? Email { get; set; }

    public string Name { get; set; }
}