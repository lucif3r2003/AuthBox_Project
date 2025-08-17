using Auth_Box.Models;
using Auth_Box.Repositories;

namespace Auth_Box.Services;

public class RegisterService
{
    private readonly UserRepository _repo;

    public RegisterService(UserRepository repo)
    {
        _repo = repo;
    }

    public APIResponse<object> registerUser(RegisterRequest req)
    {
        //check dup
        var isDup = _repo.CheckDuplicateEmail(req.email);
        if (isDup) return APIResponse<object>.Fail("Email already exists");
        
        // tao user
        var user = new User();
        user.Email = req.email;
        user.PasswordHash = _repo.HashPassword(req.password);
        user.FullName = req.full_name;
        user.PhoneNumber = req.phone_number;
        
        _repo.CreateUser(user);
        return APIResponse<object>.Ok("User created");
    }
}