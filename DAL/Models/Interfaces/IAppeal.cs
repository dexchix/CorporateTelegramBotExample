namespace DAL.Models.Interfaces
{
    public interface IAppeal
    {
        public int Id { get; set; } 
        public int TelegramChatId { get; set; }
    }
}
