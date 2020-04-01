namespace JWT.Token.Lambda.Interfaces
{
    using JWT.Token.Lambda.Models;

    public class IJwtSettings
    {
        string Audience { get; set; }
        string Secret { get; set; }
        string Issuer { get; set; }
        int ExpirationDays { get; set; }
        JwtAdminCredentials JwtAdminCredentials { get; set; }
    }
}
