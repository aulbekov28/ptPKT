using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ptPKT.Core.Services.Identity;
using ptPKT.Tests;
using ptPKT.WebUI;
using Xunit;

namespace ptPKT.FunctionalTests.Api
{
    public class AuthController : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string ApiRoute = "api/auth";
        
        public AuthController(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task NewUserRegistration_Success()
        {
            var user = new AppUserBuilder().NewUserRegistration().Build();
            var registerModel = new UserRegistedModel()
            {
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
            };
            
            var data = ConvertToJsonStringContent(registerModel);
            var response = await _client.PostAsync($"{ApiRoute}/SignUp", data);
            response.EnsureSuccessStatusCode();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);       
        }
        
        public static StringContent ConvertToJsonStringContent(object obj)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.Default, "application/json");
        }
    }
}