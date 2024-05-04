using AdminPanel.Server.Models;
using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdminPanel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestsForDaysController : ControllerBase
    {

        [HttpGet("GetActiveRequests")]
        public IActionResult GetActive()
        {
            var context = new ServiceBotContext();
            var requests = context.RequestsForDays
                .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Рассматривается)
                .Include(x=> x.Employee)
                .Select(x=> new
                {
                    Id = x.Id,    
                    Number = x.Number,
                    Date = x.CreateDate.ToString("yyyy-mm-dd"),
                    Status = x.RequestStatus.ToString(),
                    Type = x.RequestType.ToString(),    
                    Fio = x.EmployeFullName,
                    Period = $"{x.StartDate.ToString("yyyy-mm-dd")} - {x.EndDate.ToString("yyyy-mm-dd")}",
                    Description = x.Description,
                })
                .ToArray();

            if (requests.Length > 0)
                return Ok(requests);
            else
                return null;
        }

        [HttpGet("GetAllRequests")]
        public IActionResult GetAll()
        {
            var context = new ServiceBotContext();
            var requests = context.RequestsForDays
                .Select(x => new
                {
                    Id = x.Id,
                    Number = x.Number,
                    Date = x.CreateDate.ToString("yyyy-mm-dd"),
                    Status = x.RequestStatus.ToString(),
                    Type = x.RequestType.ToString(),
                    Fio = x.EmployeFullName,
                    Period = $"{x.StartDate.ToString("yyyy-mm-dd")} - {x.EndDate.ToString("yyyy-mm-dd")}",
                    Description = x.Description,
                })
                .ToArray();

            if (requests.Length > 0)
                return Ok(requests);
            else
                return NotFound(context);
        }
        [HttpGet("GetClosedRequests")]
        public IActionResult GetClosed()
        {
            var context = new ServiceBotContext();
            var requests = context.RequestsForDays
                .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Закрыто)
                .Select(x => new
                {
                    Id = x.Id,
                    Number = x.Number,
                    Date = x.CreateDate.ToString("yyyy-mm-dd"),
                    Status = x.RequestStatus.ToString(),
                    Type = x.RequestType.ToString(),
                    Fio = x.EmployeFullName,
                    Period = $"{x.StartDate.ToString("yyyy-mm-dd")} - {x.EndDate.ToString("yyyy-mm-dd")}",
                    Description = x.Description,
                })
                .ToArray();

            if (requests.Length > 0)
                return Ok(requests);
            else
                return NotFound(context);
        }
        [HttpGet("GetApprovedRequests")]
        public IActionResult GetAproved()
        {
            var context = new ServiceBotContext();
            var requests = context.RequestsForDays
                .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Одобрено)
                .Select(x => new
                {
                    Id = x.Id,
                    Number = x.Number,
                    Date = x.CreateDate.ToString("yyyy-mm-dd"),
                    Status = x.RequestStatus.ToString(),
                    Type = x.RequestType.ToString(),
                    Fio = x.EmployeFullName,
                    Period = $"{x.StartDate.ToString("yyyy-mm-dd")} - {x.EndDate.ToString("yyyy-mm-dd")}",
                    Description = x.Description,
                })
                .ToArray();

            if (requests.Length > 0)
                return Ok(requests);
            else
                return NotFound(context);
        }

        [HttpGet("GetNotApprovedRequests")]
        public IActionResult GetNotAproved()
        {
            var context = new ServiceBotContext();
            var requests = context.RequestsForDays
                .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Неодобрено)
                .Select(x => new
                {
                    Id = x.Id,
                    Number = x.Number,
                    Date = x.CreateDate.ToString("yyyy-mm-dd"),
                    Status = x.RequestStatus.ToString(),
                    Type = x.RequestType.ToString(),
                    Fio = x.EmployeFullName,
                    Period = $"{x.StartDate.ToString("yyyy-mm-dd")} - {x.EndDate.ToString("yyyy-mm-dd")}",
                    Description = x.Description,
                })
                .ToArray();

            if (requests.Length > 0)
                return Ok(requests);
            else
                return NotFound(context);
        }


        [HttpPut("AproveRequest")]
        public async Task<IActionResult> Aprove([FromBody] AprovedRequest id)
        {
            var guidId = Guid.Parse(id.Id);
            var context = new ServiceBotContext();
            var request = context.RequestsForDays
                .Where(x => x.Id == guidId)
                .FirstOrDefault();
            request.RequestStatus = DAL.Models.Enums.RequestStatus.Одобрено;
            context.SaveChanges();

            var telegramBot = new TelegramBotClient("6858392505:AAHXlxagKKKFiZE0N5XUGbRwTnYxJa6Az-A");
            var chatId = new ChatId(request.TelegramChatId);
            await telegramBot.SendTextMessageAsync(chatId, $"Ваша заявка #{request.Number} одобрена." );

            return Ok();
        }

        [HttpPut("DeniedRequests")]
        public async Task<IActionResult> Denied(
            [FromBody] DeniedRequest deniedRequest)
        {
            var deniedId = Guid.Parse(deniedRequest.Id);    
            var context = new ServiceBotContext();
            var request = context.RequestsForDays
                .Where(x => x.Id == deniedId)
                .FirstOrDefault();
            request.RequestStatus = DAL.Models.Enums.RequestStatus.Неодобрено;
            request.Responce = deniedRequest.Reason;

            var telegramBot = new TelegramBotClient("6858392505:AAHXlxagKKKFiZE0N5XUGbRwTnYxJa6Az-A");
            var chatId = new ChatId(request.TelegramChatId);
            await telegramBot.SendTextMessageAsync(chatId, $"Ваша заявка #{request.Number} не одобрена. \n Причина: {deniedRequest.Reason}");

            context.SaveChanges();

            return Ok();
        }
    }
}
