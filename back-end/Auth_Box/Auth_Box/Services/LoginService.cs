using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Auth_Box.Models;
using System.Security.Cryptography;
using System.Text;
using Auth_Box.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Auth_Box.Services;

public class LoginService
{
    private readonly IConfiguration _config;
    private readonly UserRepository _repo;
    private readonly RefreshTokenRepository _token;
    public LoginService(UserRepository repo, IConfiguration config,  RefreshTokenRepository token)
    {
        _repo = repo;
        _config = config;
        _token = token;
    }
    
    //Login 
    public APIResponse<LoginResultDto> Login(string email, string password)
    {
        var user =  _repo.GetUserByEmail(email);
        if(user == null) return APIResponse<LoginResultDto>.Fail("User not found");
        if(user.PasswordHash != _repo.HashPassword(password)) return APIResponse<LoginResultDto>.Fail("Wrong password");
        //gen access token
        var accessToken = generateToken(user);
        
        //gen refresh token 
        var refreshTokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshTokenString,
            ExpiresAt = DateTime.UtcNow.AddDays(7) // tuỳ config
        };

        _token.SaveRefreshToken(refreshToken);

        return APIResponse<LoginResultDto>.Ok(new LoginResultDto
        {
            UserId = user.Id,
            Email = user.Email,
            AccessToken = accessToken,
            RefreshToken = refreshTokenString
        }, "Login successful");
    }
    

    //gen token
    private string generateToken(User user, int expireMinutes = 15)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]);

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, "User") // hoặc "Admin"
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}