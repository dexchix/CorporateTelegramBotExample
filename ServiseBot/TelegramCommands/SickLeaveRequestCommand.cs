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
    public class SickLeaveRequestCommand
    {
        [ReplyMenuHandler("Заявка на больничный")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<OperationCache>().Operation = update.Message.Text;
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату начала больничного в формате - ДД.ММ.ГГГГ:");
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingStartDate));
        }

        public static async Task ReceivingStartDate(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            DateTime dateTimeReceiving;
            if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            {
                update.GetCacheData<OperationCache>().DateStart = dateTimeReceiving;
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingEndtDate));
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату конца больничного в формате - ДД.ММ.ГГГГ:");
            }
            else
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingStartDate));
            }
        }
        public static async Task ReceivingEndtDate(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            DateTime dateTimeReceiving;
            if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            {
                update.GetCacheData<OperationCache>().DateEnd = dateTimeReceiving;
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingSubstantiation));
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите обоснование:");
            }
            else
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingStartDate));
            }
        }


        public static async Task ReceivingSubstantiation(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            update.GetCacheData<OperationCache>().Substantiation = update.Message.Text;
            var message = @$"
Ваша заявка #324324. 
{update.GetCacheData<OperationCache>().Operation}.           
{update.GetCacheData<OperationCache>().DateStart} - {update.GetCacheData<OperationCache>().DateEnd}.
Обоснованиее: {update.GetCacheData<OperationCache>().Substantiation}";


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
            if (update.Message.Text == "Подтвердить")
            {
                //сохранение в бд
                var message = $@"
Ваша заявка #4324234 успешно создана. Статус: Рассматривается.";

                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
                await MenuCommand.Menu(botClient, update);
                //await PRTelegramBot.Helpers.Message.Send(botClient, update, "Меню");
            }
            else
            {
                var message = $@"
Ваша заявка #4324234 отменена.";
                await MenuCommand.Menu(botClient, update);
            }
            update.ClearStepUser();
        }
    }
}
