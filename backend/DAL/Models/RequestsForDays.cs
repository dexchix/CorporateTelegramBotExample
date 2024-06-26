﻿using DAL.Models.Enums;
using DAL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class RequestsForDays : IRequest
    {
        public Guid Id { get; set; }
        public string? EmployeFullName {  get; set; }    
        public int? Number { get; set; }
        public long TelegramChatId { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Responce { get; set; }
        public RequestStatus? RequestStatus { get; set; }
        public Guid EmployeeId { get; set; }
        [JsonIgnore]
        public virtual Employe? Employee { get; set; }
        public RequestType? RequestType { get; set; }
        int IRequest.Number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        Guid? IAppeal.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        long? IAppeal.TelegramChatId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
