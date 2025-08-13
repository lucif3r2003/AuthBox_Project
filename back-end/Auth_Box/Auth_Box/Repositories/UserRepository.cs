using Auth_Box.Models;

namespace Auth_Box.Repositories;

public class UserRepository
{
    private readonly AuthboxDbContext _context;

    public UserRepository(AuthboxDbContext context)
    {
        _context = context;
    }

    public user? GetUserByEmail(string email)
    {
        return _context.users.FirstOrDefault(u => u.email == email);
    }
}