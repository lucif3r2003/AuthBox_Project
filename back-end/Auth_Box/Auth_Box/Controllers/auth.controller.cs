using Auth_Box.Models;
using Auth_Box.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Box.Controllers;
[ApiController]
[Route("api/auth/login")]
public class auth_controller : ControllerBase
{
    private readonly LoginService _loginService;

    public auth_controller(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public APIResponse<object> login([FromBody] LoginRequest loginRequest)
    {
        var result = _loginService.Login(loginRequest.email, loginRequest.password);
        return result;
    }
    
}