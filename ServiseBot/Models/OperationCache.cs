using PRTelegramBot.Models;

namespace ServiseBot.Models
{
    internal class OperationCache : TelegramCache
    {
        public string? Data { get; set; }
        public string? Operation { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Substantiation { get; set; }
        public bool? Decision { get; set; }
        public void ClearData()
        {
            Data = null;
            Operation = null;
            DateStart = null;
            DateEnd = null;
            Substantiation = null;
            Decision = null;
        }
    }
}
