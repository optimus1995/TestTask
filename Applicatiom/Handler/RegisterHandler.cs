using Applicatiom.Request;
using Applicatiom.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserDomain.Entities;
using UserDomain.Interface;
public class RegisterHandler : IRequestHandler<RegisterRequest, UpdateUserDetailResponse>
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher<User> _hasher;

    public RegisterHandler(IUserRepository repo, IPasswordHasher<User> hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<UpdateUserDetailResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByEmailAsync(request.Email);
        if (existing>0)

            return new UpdateUserDetailResponse
            {
                response = "Email already registered"
            };

        var user = new User
        {
            Name = request.Name,
            Email = request.Email
        };

        user.PasswordHash = _hasher.HashPassword(user, request.Password);

        await _repo.AddAsync(user);
        return new UpdateUserDetailResponse
        {
            response = "User registered successfully"
        };
    }
}
