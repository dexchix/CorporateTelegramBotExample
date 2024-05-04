using DAL;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using System.Data.Entity;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ServiseBot.TelegramCommands
{
    public class GetActiveRequestsCommand
    {

        [ReplyMenuHandler("Получить список активных заявок")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            
            var context = new ServiceBotContext();
            var activeRequests =  context.RequestsForDays
                .Where(x => x.RequestStatus == DAL.Models.Enums.RequestStatus.Рассматривается)
                .Where(x => x.TelegramChatId == update.Message.Chat.Id)
                .ToArray();

            StringBuilder response = new StringBuilder();
            if (activeRequests.Count() == 0)
            {
                response = response.Append("У Вас нет активных заявок");
                await PRTelegramBot.Helpers.Message.Send(botClient, update, response.ToString());
                return;
            } 

            response = new StringBuilder(@"Список заявок находящихся в рассмотрении:");
            foreach (var request in activeRequests)
            {
                response.Append(@$"
 - Заявка номер: {request.Number}
    Тип: {request.RequestType}
    Дата создания: {request.CreateDate}
    Статус: {request.RequestStatus}");
            }

            await PRTelegramBot.Helpers.Message.Send(botClient, update, response.ToString());
        }
    }
}
