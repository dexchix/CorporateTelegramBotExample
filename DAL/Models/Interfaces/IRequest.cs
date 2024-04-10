using DAL.Models.Enums;

namespace DAL.Models.Interfaces
{
    public interface IRequest : IAppeal
    {
        public int Id { get; set; }
        public RequestType RequestType { get; set; }
        public int TelegramChatId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Responce { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public Guid EmployeeId { get; set; }
        public Employe Employee { get; set; }
    }
}
