using DAL;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace AdminPanel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var context = new ServiceBotContext();
            var closedRequests = context.RequestsForDays
                .Where(x=> x.RequestStatus == DAL.Models.Enums.RequestStatus.Закрыто)
                .ToArray();
            foreach (var closedRequest in closedRequests)
            {
                var tbc = new TelegramBotClient("6858392505:AAHXlxagKKKFiZE0N5XUGbRwTnYxJa6Az-A");
                ChatId chatId = new ChatId(closedRequest.TelegramChatId);
                tbc.SendTextMessageAsync(chatId, $"Заявка № {closedRequest.Number} одобрена!");
            }



            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
