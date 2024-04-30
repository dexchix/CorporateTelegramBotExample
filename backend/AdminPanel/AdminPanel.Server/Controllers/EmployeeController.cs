using AdminPanel.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpPost("CreateEmploye")]
        public IActionResult CreateEmploye([FromBody] Employe employe)
        {
            return Ok();
        }
    }
}
