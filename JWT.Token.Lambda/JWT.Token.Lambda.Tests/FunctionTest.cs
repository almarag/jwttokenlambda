namespace JWT.Token.Lambda.Tests
{
    using Xunit;
    using Amazon.Lambda.TestUtilities;
    using JWT.Token.Lambda.Models;
    using Newtonsoft.Json;
    using System.IO;

    [Collection("TokenCollection")]
    public class FunctionTest
    {
        [Fact]
        public void TestValidTokenFunction()
        {
            var validCredentials = new JwtAdminCredentials()
            {
                UserName = "almarag",
                Password = "test123"
            };

            var inputString = JsonConvert.SerializeObject(validCredentials);
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(inputString);
            writer.Flush();
            stream.Position = 0;

            var function = new Function();
            var context = new TestLambdaContext();
            dynamic response = function.FunctionHandler(stream, context);

            Assert.NotNull(response);
            var token = response.GetType().GetProperty("token").GetValue(response, null);
            Assert.NotEqual(string.Empty, token);
        }

        [Fact]
        public void TestInvalidTokenFunction()
        {
            var validCredentials = new JwtAdminCredentials()
            {
                UserName = "almarag",
                Password = "invalid"
            };

            var inputString = JsonConvert.SerializeObject(validCredentials);
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(inputString);
            writer.Flush();
            stream.Position = 0;

            var function = new Function();
            var context = new TestLambdaContext();

            dynamic response = function.FunctionHandler(stream, context);

            Assert.NotNull(response);
            var token = response.GetType().GetProperty("token").GetValue(response, null);
            Assert.Equal(string.Empty, token);
        }
    }
}
