using Microsoft.Extensions.Caching.Distributed;

namespace GitActions.Api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IDistributedCache _distributedCache;
        public WeatherService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> GetWeatherForecastAsync(string key)
        {
            var cachedData = await _distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return cachedData;
            }
            // Simulate fetching data from an external source
            var weatherData = $"Weather forecast for {key} at {DateTime.Now}";
            // Cache the data for 5 minutes
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            await _distributedCache.SetStringAsync(key, weatherData, cacheOptions);
            return weatherData;
        }
    }
}
