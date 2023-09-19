using System.Net.Http.Headers;

namespace ExchangeRateUpdater.Repository.Services
{
    public class ApiService : IApiService
    {
        private HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> CallAPI<T>(string url)
        {
            _httpClient.BaseAddress = GetUri(url);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync(GetUri(url));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<T>();
                return result;
            }

            return default;   //<-- todo wrap the call in success/failure class
        }

        private Uri? _Uri = null;

        // maybe over-kill, but thought if called numerous times with same url,
        // then create lots of objects that are the same.
        private Uri? GetUri(string url)
        {
            if (_Uri == null || _Uri.AbsoluteUri != url)
            {
                _Uri = new Uri(url);
            }
            return _Uri;
        }
    }
}
