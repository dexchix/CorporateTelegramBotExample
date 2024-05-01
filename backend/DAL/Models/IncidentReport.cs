using DAL.Models.Interfaces;

namespace DAL.Models
{
    public class IncidentReport : IAppeal
    {
        public Guid Id { get; set; }
        public int? Number { get; set; }
        public long? TelegramChatId { get; set; }
        public DateTime? IncidentDate { get; set; }
        public string? Description { get; set; }
        public Guid? EmployeId { get; set; } 
        public virtual Employe? Employe { get; set; }
        Guid? IAppeal.Id { get; set; }
    }
}
