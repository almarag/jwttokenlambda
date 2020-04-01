namespace JWT.Token.Lambda.Models
{
    using JWT.Token.Lambda.Interfaces;

    public class AWSSettings : IAWSSettings
    {
        public string AccessKey { get; set; }
        public string AccessSecret { get; set; }
    }
}
