using System.Security.Cryptography;
using System.Text;
using Auth_Box.Models;

namespace Auth_Box.Repositories;

public class UserRepository
{
    private readonly AuthboxDbContext _context;

    public UserRepository(AuthboxDbContext context)
    {
        _context = context;
    }

    //lay user bang email
    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    //check dup email
    public bool CheckDuplicateEmail(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }

    //tao user moi
    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    
    public string HashPassword(string password)
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