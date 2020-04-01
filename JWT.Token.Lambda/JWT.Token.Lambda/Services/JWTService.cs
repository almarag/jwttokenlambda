namespace JWT.Token.Lambda.Services
{
    using JWT.Token.Lambda.Entities;
    using JWT.Token.Lambda.Models;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Linq;
    using JWT.Token.Lambda.Interfaces;
    using System.Threading.Tasks;

    public class JWTService : IJWTService
    {
        private jwtContext _context { get; set; }
        
        public JWTService(jwtContext context) => _context = context;

        public string CreateToken(JwtAdminCredentials credentials, JwtSettings settings)
        {
            var query = (from user in _context.Users
                        where user.UserName == credentials.UserName
                        && user.Password == credentials.Password
                        select user).FirstOrDefault();

            if (query != null)
            {
                if (credentials.UserName == query.UserName
                    && credentials.Password == query.Password)
                {
                    var issuedAt = DateTime.UtcNow;
                    var expires = DateTime.UtcNow.AddDays(settings.ExpirationDays);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var claimsIdentity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, credentials.UserName)
                    });

                    var sec = settings.Secret;
                    var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(sec));
                    var signingCredentials = new SigningCredentials(
                        securityKey,
                        SecurityAlgorithms.HmacSha256Signature
                    );

                    var token = tokenHandler.CreateJwtSecurityToken(
                        settings.Issuer,
                        settings.Audience,
                        claimsIdentity,
                        issuedAt,
                        expires,
                        signingCredentials: signingCredentials
                    );

                    var tokenString = tokenHandler.WriteToken(token);

                    return tokenString;
                }
            }

            return string.Empty;
        }
    }
}
