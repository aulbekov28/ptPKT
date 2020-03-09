using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ptPKT.FunctionalTests.Infrastructure
{
    internal static class FunctionalTestsHttpClientExtension
    {
        internal static async Task AuthorizeAsGuest(this HttpClient httpClient, string userName = "GuestUser")
        {
            var response = await httpClient.PostAsync($"/api/auth/login-as-guest?userName={userName}", null);
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        internal static Task AuthorizeAsUsualUser()
        {
            throw new NotImplementedException();
        }
    }
}