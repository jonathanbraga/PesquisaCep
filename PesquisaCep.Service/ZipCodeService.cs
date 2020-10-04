using Newtonsoft.Json;
using PesquisaCep.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PesquisaCep.Service
{
    public class ZipCodeService : IZipCode
    {
        private readonly HttpClient _httpClient;
        private static readonly string BASEADDRESS = "https://viacep.com.br";
        public ZipCodeService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BASEADDRESS)
            };
        }
        public async Task<ZipCodeInfo> GetZipCodeInfo(string zipCode)
        {
            var response = await _httpClient.GetAsync($"/ws/{zipCode}/json");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ZipCodeInfo>(content);
            }

            return new ZipCodeInfo();
        }
    }
}
