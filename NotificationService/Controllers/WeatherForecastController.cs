using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NotificationService.Controllers
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


            LaunchSendTelegramJob();


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [NonAction]
        private void LaunchSendTelegramJob()
        {
            RecurringJobOptions options = new RecurringJobOptions();

            RecurringJob.AddOrUpdate("SendRequestResultToUser", () => TEST(), Cron.MinuteInterval(1));
        }

        [NonAction]
        public void TEST()
        {
            var tbc = new TelegramBotClient("6858392505:AAHXlxagKKKFiZE0N5XUGbRwTnYxJa6Az-A");
            ChatId chatId = new ChatId(1334655443);


            tbc.SendTextMessageAsync(chatId, "Hangfire Job Succsided");
        }

    }
}
