namespace JWT.Token.Lambda.Models
{
    using JWT.Token.Lambda.Interfaces;

    public class JwtSettings : IJwtSettings
    {
        public string Audience { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int ExpirationDays { get; set; }
        public JwtAdminCredentials JwtAdminCredentials { get; set; }
    }
}
