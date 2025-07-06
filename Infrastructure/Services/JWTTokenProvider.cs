using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserDomain.Entities;
using UserDomain.Interface;
using Microsoft.Extensions.Configuration;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        var secret = _config["Jwt:Secret"] ??
            throw new ArgumentNullException("Jwt:Secret is missing in configuration");
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }

    public string CreateToken(User user)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(GetJwtExpirationMinutes()),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    _key,
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            // Log the error here
            throw new ApplicationException("Token generation failed", ex);
        }
    }
    private int GetJwtExpirationMinutes()
    {
        var expirationConfig = _config["Jwt:ExpirationInMinutes"];
        if (string.IsNullOrWhiteSpace(expirationConfig))
        {
            return 60; // Default fallback value
        }

        if (!int.TryParse(expirationConfig, out var minutes) || minutes <= 0)
        {
            return 60; // Default fallback value
        }

        return minutes;
    }
}