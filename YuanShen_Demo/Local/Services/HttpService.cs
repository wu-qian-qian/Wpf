using Extension;
using HttpFactory;
using System.Windows.Media.Imaging;
using WPF.Tools.Local.Service;

namespace YuanShen_Demo.Local.Services
{
    class HttpService:IService
    {
        private readonly HttpClientFactory _http;
        public HttpService()
        {
            _http = new HttpClientFactory();
        }

        public async Task<string> GetStringAsync(string url)
        {
            using var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<BitmapImage> GetImageAsync(string url)
        {
            using var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsByteArrayAsync();
            return ReadResources.ReadStreamResource(data);
        }
    }
}
