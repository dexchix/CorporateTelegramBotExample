using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace AdminPanel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        [HttpGet("GetIncidents")]
        public IActionResult GetIncidents()
        {
            var context = new ServiceBotContext();

            var result = context.IncidentReports
                .Include(x => x.Employe)
                .Select(x => new
                {
                    Id = x.Id.ToString(),
                    number = x.Number,
                    Date = x.IncidentDate.ToString("yyyy-mm-dd"),
                    Fio = x.Employe.FullName,
                    Description = x.Description
                })
                .ToArray();
            return Ok(result);
        }
    }
}
