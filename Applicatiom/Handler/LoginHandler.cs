using Applicatiom.Request;
using Applicatiom.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserDomain.Entities;
using UserDomain.Interface;

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher<User> _hasher;
    private readonly ITokenService _tokenService;

    public LoginHandler(IUserRepository repo, IPasswordHasher<User> hasher, ITokenService tokenService)
    {
        _repo = repo;
        _hasher = hasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetLoginAsync(request.Email);
        if (user == null)
            return new LoginResponse { Token = "", Message = "Invalid credentials" };

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result != PasswordVerificationResult.Success)
            return new LoginResponse { Token = "", Message = "Invalid credentials" };

        string token = _tokenService.CreateToken(user);

        return new LoginResponse { Token = token, Message = "Login successful" };
    }
}
