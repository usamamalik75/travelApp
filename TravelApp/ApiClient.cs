namespace TravelApp
{
    public class ApiClient(SabreAuthService authService)
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _apiBaseUrl = "https://api.sabre.com/v1/shop/";
        private readonly SabreAuthService _authService = authService;

        public async Task<string> GetAsync(string endpoint)
        {
            try
            {
                // Get access token from the auth service
                string accessToken = await _authService.GetAccessTokenAsync();

                if (string.IsNullOrEmpty(accessToken))
                {
                    Console.WriteLine("Failed to retrieve access token.");
                    return string.Empty;
                }

                // Set Authorization header with the retrieved access token
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                // Make the API request
                HttpResponseMessage response = await _client.GetAsync(_apiBaseUrl + endpoint);
                response.EnsureSuccessStatusCode(); // Throws exception if status code is not 200-299

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return string.Empty;
            }
        }
    }
}
