using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StarterAPI.Utils
{
    public class RandomStringProvider
    {
        private readonly HttpClient _client;
        private const string RandomStringUri =
            "https://www.random.org/strings/?num=1&len=8&digits=on&upperalpha=on&loweralpha=on&unique=on&format=plain&rnd=new";
        public RandomStringProvider(HttpClient client)
        {
            _client = client;
        }

        public string RandomString { get; private set; }

        public async Task UpdateString(CancellationToken token)
        {
            var response = await _client.GetAsync(RandomStringUri, token);
            if(response.IsSuccessStatusCode)
            {
                RandomString = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
