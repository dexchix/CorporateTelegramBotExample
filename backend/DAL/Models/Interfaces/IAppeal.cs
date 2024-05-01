namespace DAL.Models.Interfaces
{
    public interface IAppeal
    {
        public Guid? Id { get; set; } 
        public long? TelegramChatId { get; set; }
    }
}
