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
    public user? GetUserByEmail(string email)
    {
        return _context.users.FirstOrDefault(u => u.email == email);
    }

    //check dup email
    public bool CheckDuplicateEmail(string email)
    {
        return _context.users.Any(u => u.email == email);
    }

    //tao user moi
    public void CreateUser(user user)
    {
        _context.users.Add(user);
        _context.SaveChanges();
    }
}