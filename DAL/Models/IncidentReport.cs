using DAL.Models.Interfaces;

namespace DAL.Models
{
    public class IncidentReport : IAppeal
    {
        public int Id { get; set; }
        public int TelegramChatId { get; set; }
        public DateTime IncidentDate { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public Employe Employe { get; set; }
    }
}
