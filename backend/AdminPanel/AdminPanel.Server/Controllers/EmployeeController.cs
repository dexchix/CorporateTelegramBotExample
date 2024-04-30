using AdminPanel.Server.Models;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("GetEmployes")]
        public IActionResult GetEmployees()
        {
            var context = new ServiceBotContext();
            var result = context.Employes.Select(x=> new
            {
                Id = x.Id.ToString(),
                Login = x.Login,
                FullName = x.FullName,
                Department = x.Department,
                IsAutorized = x.IsAutorized,
                Phone = x.Phone,
            }).ToArray();
            return Ok(result);
        }

        [HttpPost("CreateEmploye")]
        public IActionResult CreateUpdateEmploye([FromBody] Employe employeRequest)
        {
            var context = new ServiceBotContext();
            if (!string.IsNullOrWhiteSpace(employeRequest.Id))
            {
                var employeGuid = Guid.Parse(employeRequest.Id);
                var employe = context.Employes.FirstOrDefault(x => x.Id == employeGuid);

                employe.Department = employeRequest.Department;
                employe.FullName = employeRequest.FullName;
                employe.Phone = employeRequest.Phone;   
                employe.Login = employeRequest.Login;

                context.Update(employe);
            }
            else
            {
                var newEmploye = new DAL.Models.Employe()
                {
                    Id = Guid.NewGuid(),
                    Department = employeRequest.Department,
                    FullName = employeRequest.FullName,
                    Login = employeRequest.Login,
                    Phone = employeRequest.Phone,
                };
                context.Add(newEmploye);
            }
            context.SaveChanges();
            return Ok();
        }
    }
}
