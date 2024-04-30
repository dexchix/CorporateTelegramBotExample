using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Aprove([FromHeader] Guid id)
        {
            //var guidId = Guid.Parse(id);
            var context = new ServiceBotContext();
            var request = context.RequestsForDays
                .Where(x => x.Id == id)
                .FirstOrDefault();
            request.RequestStatus = DAL.Models.Enums.RequestStatus.Одобрено;
            context.RequestsForDays.Add(request);

            return Ok();
        }

        [HttpPut("DeniedRequests")]
        public IActionResult Denied(
            [FromHeader] Guid id,
            [FromHeader] string reason)
        {
            var context = new ServiceBotContext();
            var request = context.RequestsForDays
                .Where(x => x.Id == id)
                .FirstOrDefault();
            request.RequestStatus = DAL.Models.Enums.RequestStatus.Закрыто;
            request.Responce = reason;
            context.RequestsForDays.Add(request);

            return Ok();
        }
    }
}
