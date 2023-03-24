namespace AopLibraryTest
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
        Task<string> GreetingAsync(string name);
        Task GetEmpty();
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
       };
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public async Task<string> GreetingAsync(string name)
        {
            await Task.CompletedTask;
            return  $"{name} 你好吗?";
        }

        public async Task GetEmpty()
        {
            await Task.Delay(500);
            Console.WriteLine("这是一个测试方法");
        }
    }
}
