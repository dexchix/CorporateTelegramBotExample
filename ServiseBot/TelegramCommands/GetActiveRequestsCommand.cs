using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ServiseBot.TelegramCommands
{
    public class GetActiveRequestsCommand
    {

        [ReplyMenuHandler("Получить список активных заявок")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, @"
Заявка 1
Заявка 2
Заявка 3
");
        }
    }
}
