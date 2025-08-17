using Auth_Box.Models;
using Auth_Box.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Box.Controllers;
[ApiController]
[Route("api/auth")]
public class auth_controller : ControllerBase
{
    private readonly LoginService _loginService;
    private readonly RegisterService _registerService;

    public auth_controller(LoginService loginService,  RegisterService registerService)
    {
        _loginService = loginService;
        _registerService = registerService;
    }
    

    [HttpPost("login")]
    public APIResponse<LoginResultDto> Login([FromBody] LoginRequest loginRequest)
    {
        var result = _loginService.Login(loginRequest.email, loginRequest.password);
        if (!result.Success) return result;

        // lưu refresh token vào cookie (HttpOnly để tránh JS đọc)
        Response.Cookies.Append("Auth_Box_RefreshToken", result.Data.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // bật nếu dùng https
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        // access token thường trả về body (client lưu trong memory, không nên bỏ vào cookie)
        return result;
    }

    [HttpPost("register")]
    public APIResponse<object> register([FromBody] RegisterRequest registerRequest)
    {
        var rs = _registerService.registerUser(registerRequest);
        return rs;
    }
    
}