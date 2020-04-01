namespace JWT.Token.Lambda.Interfaces
{
    using JWT.Token.Lambda.Models;

    public interface IJWTService
    {
        string CreateToken(JwtAdminCredentials credentials, JwtSettings settings);
    }
}