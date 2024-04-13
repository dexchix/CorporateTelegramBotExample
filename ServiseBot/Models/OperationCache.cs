using DAL.Models.Enums;
using PRTelegramBot.Models;

namespace ServiseBot.Models
{
    internal class OperationCache : TelegramCache
    {
        public string? Login { get; set; }
        public string? Data { get; set; }
        public RequestType Operation { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? Description { get; set; }
        public bool? Decision { get; set; }
        public string? TokenCode { get; set; }
        public Guid EmployeId { get; set; }
        public void ClearData()
        {
            Login = null;
            Data = null;
            Operation = RequestType.Отгул;
            DateStart = DateTime.MinValue;
            DateEnd = DateTime.MinValue;
            Description = null;
            Decision = null;
            TokenCode = null;
            //EmployeId = Guid.Empty;
        }
    }
}
