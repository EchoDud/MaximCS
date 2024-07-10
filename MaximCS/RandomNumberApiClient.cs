using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaximCS
{
    public class RandomNumberApiClient //тут можно и интерфейсы и родительский класс навернуть, но мне лень (: 
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl;

        public RandomNumberApiClient(string apiUrl)
        {
            _client = new HttpClient();
            _apiUrl = apiUrl;
        }

        public async Task<int> GetRandomNumberAsync(int maxValue)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_apiUrl}?min=0&max={maxValue}&count=1");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    using (JsonDocument document = JsonDocument.Parse(responseBody))
                    {
                        JsonElement root = document.RootElement;

                        if (root.ValueKind == JsonValueKind.Array)
                        {
                            int randomNumber = root[0].GetInt32();
                            return randomNumber;
                        }
                        else
                        {
                            throw new Exception("Expected JSON array from API response.");
                        }
                    }
                }
                else
                {
                    throw new Exception($"Failed to get random number from API. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get random number from API: {ex.Message}");
                Random random = new Random();
                return random.Next(maxValue);
            }
        }
    }


}
