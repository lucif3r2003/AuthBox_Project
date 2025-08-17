using Auth_Box.Models;

namespace Auth_Box.Repositories;

public class RefreshTokenRepository
{
    private readonly AuthboxDbContext _context;

    public RefreshTokenRepository(AuthboxDbContext context)
    {
        _context = context;
    }

    // Lưu refresh token mới
    public void SaveRefreshToken(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        _context.SaveChanges();
    }

    // Lấy refresh token theo token string
    public RefreshToken? GetByToken(string token)
    {
        return _context.RefreshTokens.FirstOrDefault(rt => rt.Token == token && rt.RevokedAt == null);
    }

    // Revoke token (logout hoặc refresh)
    public void RevokeToken(string token)
    {
        var existing = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
        if (existing != null)
        {
            existing.RevokedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }

    // Xoá hết token của 1 user (logout all devices)
    public void RevokeAllByUser(Guid userId)
    {
        var tokens = _context.RefreshTokens.Where(rt => rt.UserId == userId && rt.RevokedAt == null);
        foreach (var t in tokens)
        {
            t.RevokedAt = DateTime.UtcNow;
        }
        _context.SaveChanges();
    }
}