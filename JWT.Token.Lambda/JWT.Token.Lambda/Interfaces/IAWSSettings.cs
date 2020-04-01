namespace JWT.Token.Lambda.Interfaces
{
    public interface IAWSSettings
    {
        string AccessKey { get; set; }
        string AccessSecret { get; set; }
    }
}
