using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ServiseBot.TelegramCommands
{
    internal class HotlineCommand
    {
        [ReplyMenuHandler("Горячая линия")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Номер: 88005553535");
        }
    }
}
