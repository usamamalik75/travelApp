using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TravelApp
{
    public class SabreAuthService
    {
        private readonly string _clientId = "V1:7c0v9147t9el0yna:DEVCENTER:EXT";
        private readonly string _clientSecret = "1jNau6KA";
        private readonly HttpClient _client = new();
        private readonly string _authUrl = "https://api.platform.sabre.com/v2/auth/token";
        private string? _accessToken;
        private DateTime _tokenExpiration;

        public SabreAuthService()
        {
            _accessToken = null;
            _tokenExpiration = DateTime.MinValue;
        }

        // Method to retrieve the access token
        public async Task<string> GetAccessTokenAsync()
        {
            // If token is still valid, return it
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.Now < _tokenExpiration)
            {
                return _accessToken;
            }

            // Prepare credentials as Base64 encoded string
            string baseClientId = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_clientId));
            string baseSecret = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_clientSecret));
            var base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{baseClientId}:{baseSecret}"));

            // Set Authorization header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            // Prepare form data
            var content = new StringContent("grant_type=client_credentials", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            // Send POST request for token
            HttpResponseMessage response = await _client.PostAsync(_authUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get token: {response.StatusCode}");
                return string.Empty;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            JObject tokenData = JObject.Parse(jsonResponse);

            // Set token and expiration time
            _accessToken = tokenData["access_token"].ToString();
            int expiresIn = int.Parse(tokenData["expires_in"].ToString());
            _tokenExpiration = DateTime.Now.AddSeconds(expiresIn - 60);

            return _accessToken;
        }
    }
}
