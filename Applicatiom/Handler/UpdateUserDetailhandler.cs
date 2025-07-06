using Applicatiom.Request;
using Applicatiom.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using UserDomain.Entities;
using UserDomain.Interface;
public class UpdateUserDetailHandler : IRequestHandler<UpdateUserDetailRequest, UpdateUserDetailResponse>
{
    private readonly IUserRepository _repo;


    public UpdateUserDetailHandler(IUserRepository repo)
    {
        _repo = repo;

    }

    public async Task<UpdateUserDetailResponse> Handle(UpdateUserDetailRequest request, CancellationToken cancellationToken)
    {
        
        var existing = await _repo.GetByEmailAsync(request.Email);
        if (existing==0)

            return new UpdateUserDetailResponse
            {
                response = "No Record Exist according to this email"
            };

        var user = new User
        {Email=request.Email,
            Name = request.Name,

        };

        await _repo.UpdateDetailsAsync(user);
        return new UpdateUserDetailResponse
        {
            response = "Updated successfully"
        };
    }
}
