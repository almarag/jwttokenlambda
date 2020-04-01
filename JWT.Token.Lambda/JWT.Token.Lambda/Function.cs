using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace JWT.Token.Lambda
{
    using Amazon.Lambda.APIGatewayEvents;
    using JWT.Token.Lambda.Entities;
    using JWT.Token.Lambda.Models;
    using JWT.Token.Lambda.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public class Function
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;
        private readonly jwtContext _context;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Function constructor
        /// </summary>
        public Function()
        {
            var startupLambda = new StartUpLambda();
            _configuration = startupLambda.GetConfiguration();
            _services = startupLambda.AddLambdaServices(_configuration);
            _provider = _services.BuildServiceProvider();
            _context = _provider.GetRequiredService<jwtContext>();
            _jwtSettings = _provider.GetService<JwtSettings>();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public object FunctionHandler(Stream input, ILambdaContext context)
        {
            try
            {
                Amazon.Lambda.Serialization.Json.JsonSerializer jsonSerializer = new Amazon.Lambda.Serialization.Json.JsonSerializer();
                var requestInput = jsonSerializer.Deserialize<JwtAdminCredentials>(input);

                // Default response
                var result = new APIGatewayProxyResponse() { StatusCode = 500 };

                if (requestInput != null)
                {
                    var token = new JWTService(_context)
                            .CreateToken(requestInput, _jwtSettings);

                    return new
                    {
                        token
                    };
                }

                return result;
            }
            catch(Exception e)
            {
                context.Logger.Log(string.Format("Internal server error: {0}", e.Message));
                context.Logger.Log(string.Format("Stack trace: {0}", e.StackTrace));
                return new APIGatewayProxyResponse()
                {
                    Body = JsonConvert.SerializeObject(new { message = e.Message + e.StackTrace }),
                    StatusCode = 500,
                    Headers = new Dictionary<string, string> { { "Access-Control-Allow-Origin", "*" }, { "Access-Control-Allow-Methods", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS" } }
                };
            }
        }
    }
}