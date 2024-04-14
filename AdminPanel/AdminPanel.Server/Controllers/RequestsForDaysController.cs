﻿using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestsForDaysController : ControllerBase
    {

        [HttpGet("GetActiveRequests")]
        public IEnumerable<RequestsForDays> GetActive()
        {
            var context = new ServiceBotContext();
            var requests = context.RequestsForDays
                .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Рассматривается)
                .ToArray();

            if (requests.Length > 0)
                return requests;
            else
                return null;
        }

        //[HttpGet(Name = "GetAllRequests")]
        //public IActionResult GetAll()
        //{
        //    var context = new ServiceBotContext();
        //    var requests = context.RequestsForDays
        //        .ToArray();

        //    if (requests.Length > 0)
        //        return Ok(requests);
        //    else
        //        return NotFound(context);
        //}
        //[HttpGet(Name = "GetClosedRequests")]
        //public IActionResult GetClosed()
        //{
        //    var context = new ServiceBotContext();
        //    var requests = context.RequestsForDays
        //        .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Закрыто)
        //        .ToArray();

        //    if (requests.Length > 0)
        //        return Ok(requests);
        //    else
        //        return NotFound(context);
        //}
        //[HttpGet(Name = "GetApprovedRequests")]
        //public IActionResult GetAproved()
        //{
        //    var context = new ServiceBotContext();
        //    var requests = context.RequestsForDays
        //        .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Одобрено)
        //        .ToArray();

        //    if (requests.Length > 0)
        //        return Ok(requests);
        //    else
        //        return NotFound(context);
        //}

        //[HttpGet(Name = "GetNotApprovedRequests")]
        //public IActionResult GetNotAproved()
        //{
        //    var context = new ServiceBotContext();
        //    var requests = context.RequestsForDays
        //        .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Неодобрено)
        //        .ToArray();

        //    if (requests.Length > 0)
        //        return Ok(requests);
        //    else
        //        return NotFound(context);
        //}


        //[HttpPut(Name = "AproveRequest")]
        //public IActionResult Aprove([FromHeader] Guid id)
        //{        
        //    var context = new ServiceBotContext();
        //    var request = context.RequestsForDays
        //        .Where(x => x.Id == id) 
        //        .FirstOrDefault();
        //    request.RequestStatus = DAL.Models.Enums.RequestStatus.Одобрено;
        //    context.RequestsForDays.Add(request);

        //    return Ok();
        //}

        //[HttpPut(Name = "DeniedRequests")]
        //public IActionResult Denied(
        //    [FromHeader] Guid id, 
        //    [FromHeader] string reason)
        //{
        //    var context = new ServiceBotContext();
        //    var request = context.RequestsForDays
        //        .Where(x => x.Id == id)
        //        .FirstOrDefault();
        //    request.RequestStatus = DAL.Models.Enums.RequestStatus.Закрыто;
        //    request.Responce = reason; 
        //    context.RequestsForDays.Add(request);

        //    return Ok();
        //}
    }
}