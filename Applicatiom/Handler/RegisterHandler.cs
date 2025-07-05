using Applicatiom.Request;
using Applicatiom.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserDomain.Entities;
using UserDomain.Interface;
public class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher<User> _hasher;

    public RegisterHandler(IUserRepository repo, IPasswordHasher<User> hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByEmailAsync(request.Email);
        if (existing != null)

            return new RegisterResponse
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
        return new RegisterResponse
        {
            response = "User registered successfully"
        };
    }
}
