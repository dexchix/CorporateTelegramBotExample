using PRTelegramBot.Models;

namespace ServiseBot.Models.Caches
{
    internal class SickLeaveCache : TelegramCache
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Substantiation { get; set; }
        public string Operation { get; set; }
    }
}
