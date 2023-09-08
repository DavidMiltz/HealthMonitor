namespace WeatherApi
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "dc3d2dbb1e145330b74e064d9482f83e"; // Replace with your API key

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetAirPressureAsync(string city)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city},Germany&appid={_apiKey}&units=metric";

                var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);

                if (response != null && response.Main != null)
                {
                    return response.Main.Pressure;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }

    public class WeatherResponse
    {
        public MainData? Main { get; set; }
    }

    public class MainData
    {
        public int Pressure { get; set; }
    }
}