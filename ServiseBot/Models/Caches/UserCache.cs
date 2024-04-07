using PRTelegramBot.Models;

namespace ServiseBot.Models.Caches
{
    public abstract class UserCache : TelegramCache
    {
        /// <summary>
        /// Данные
        /// </summary>
        public abstract string Data { get; set; }

        /// <summary>
        /// Очистка данных
        /// </summary>
        public abstract void ClearData();
    }
}
