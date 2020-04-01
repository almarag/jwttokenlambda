namespace JWT.Token.Lambda.Models
{
    public class JwtAdminCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public JwtAdminCredentials() { }

        public JwtAdminCredentials(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
