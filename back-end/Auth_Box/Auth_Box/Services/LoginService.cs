using Auth_Box.Models;
using System.Security.Cryptography;
using System.Text;
using Auth_Box.Repositories;

namespace Auth_Box.Services;

public class LoginService
{
    private readonly UserRepository _repo;
    public LoginService(UserRepository repo)
    {
        _repo = repo;
    }
    
    //Login 
    public APIResponse<object> Login(string email, string password)
    {
        var user =  _repo.GetUserByEmail(email);
        if(user == null) return APIResponse<object>.Fail("User not found");
        if(user.password_hash != HashPassword(password)) return APIResponse<object>.Fail("Wrong password");
        return APIResponse<object>.Ok(new {user.id, user.email}, "Login successful");
    }
    
    //logic handle 

    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convert the password string to a byte array
            byte[] bytes = Encoding.UTF8.GetBytes(password);
        
            // Compute the hash
            byte[] hash = sha256.ComputeHash(bytes);
        
            // Convert the hash to a hexadecimal string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
        
            return builder.ToString();
        }
    }
}