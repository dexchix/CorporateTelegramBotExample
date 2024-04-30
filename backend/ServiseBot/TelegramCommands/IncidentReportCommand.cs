using DAL;
using DAL.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using ServiseBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ServiseBot.TelegramCommands
{
    public class IncidentReportCommand
    {
        [ReplyMenuHandler("Сообщение об инциденте")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату инцидента в формате - ДД.ММ.ГГГГ ЧЧ:ММ:СС:");
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDate));
        }


        public static async Task ReceivingDate(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            DateTime dateTimeReceiving;
            if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            {
                update.GetCacheData<OperationCache>().DateStart = dateTimeReceiving;
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingSubstantiation));
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите описание:");
            }
            else
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDate));
            }
        }


        public static async Task ReceivingSubstantiation(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            update.GetCacheData<OperationCache>().Description = update.Message.Text;

            var context = new ServiceBotContext();

            var message = @$"
Сообщение об инциденте:           
    Дата: {update.GetCacheData<OperationCache>().DateStart}.
    Описание: {update.GetCacheData<OperationCache>().Description}";
             

            var menuList = new List<KeyboardButton>();
            menuList.Add(new KeyboardButton("Подтвердить"));
            menuList.Add(new KeyboardButton("Отмена"));

            var menu = MenuGenerator.ReplyKeyboard(1, menuList);
            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(CreateReceivingRequest));
        }

        public static async Task CreateReceivingRequest(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            var context = new ServiceBotContext();
            if (update.Message.Text == "Подтвердить")
            {
                var id = Guid.NewGuid();
                var indcidentReport = new IncidentReport()
                {
                    Id = id,
                    Description = update.GetCacheData<OperationCache>().Description,
                    EmployeId = update.GetCacheData<OperationCache>().EmployeId,
                    IncidentDate = DateTime.UtcNow,
                    Number = Helper.GuidToInt(id),
                    TelegramChatId = update.Message.Chat.Id
                };
                context.IncidentReports.Add(indcidentReport);
                context.SaveChanges();
                var message = $@"
Сообщение об инциденте №{indcidentReport.Number} успешно создано.";

                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
                await MenuCommand.Menu(botClient, update);
            }
            else
            {
                var message = $@"
Ваша заявка отменена.";
                await MenuCommand.Menu(botClient, update);
            }
            update.ClearStepUser();
        }
    }
}
