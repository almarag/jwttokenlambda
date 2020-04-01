namespace JWT.Token.Lambda.Interfaces
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public interface IStartUpLambda
    {
        IServiceCollection AddLambdaServices(IConfiguration configuration);
        IConfigurationRoot GetConfiguration();
    }
}
