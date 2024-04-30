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
    public class RequestForDaysCommand
    {
        [ReplyMenuHandler("Заявка на переработку", "Заявка на отпуск", "Заявка на отгул", "Заявка на больничный")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            var context = new ServiceBotContext();
            var employe = context.Employes
                .Where(x => update.Message.Chat.Username == x.TelegramLogin)
                .FirstOrDefault();
            update.GetCacheData<OperationCache>().EmployeId = employe.Id;
            update.GetCacheData<OperationCache>().Operation = Helper.GetOperationTypeToEnum(update.Message.Text);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату начала периода  в формате - ДД.ММ.ГГГГ ЧЧ:ММ:СС:");
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
        }

        public static async Task ReceivingDateStart(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            DateTime dateTimeReceiving;
            if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            {
                update.GetCacheData<OperationCache>().DateStart = dateTimeReceiving.ToUniversalTime();
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateEnd));
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату конца периода в формате - ДД.ММ.ГГГГ ЧЧ.ММ.СС:");
            }
            else
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
            }
        }

        public static async Task ReceivingDateEnd(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            DateTime dateTimeReceiving;
            if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            {
                update.GetCacheData<OperationCache>().DateEnd = dateTimeReceiving.ToUniversalTime();
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingSubstantiation));
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите описание:");
            }
            else
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
            }
        }


        public static async Task ReceivingSubstantiation(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            update.GetCacheData<OperationCache>().Description = update.Message.Text;
            var message = @$"
Тип операции: {Helper.GetOperationTypeToString(update.GetCacheData<OperationCache>().Operation)}           
    Дата/время переработки: {update.GetCacheData<OperationCache>().DateStart} - {update.GetCacheData<OperationCache>().DateEnd}.
    Обоснованиее: {update.GetCacheData<OperationCache>().Description}.";


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
                var employe = context.Employes.FirstOrDefault(x => x.TelegramId == update.Message.From.Id);
                var id = Guid.NewGuid();
                var request = new RequestsForDays()
                {
                    Id = id,
                    CreateDate = DateTime.UtcNow,
                    Description = update.GetCacheData<OperationCache>().Description,
                    EmployeeId = update.GetCacheData<OperationCache>().EmployeId,
                    EndDate = update.GetCacheData<OperationCache>().DateEnd,
                    StartDate = update.GetCacheData<OperationCache>().DateStart,
                    Number = Helper.GuidToInt(id),
                    RequestStatus = DAL.Models.Enums.RequestStatus.Рассматривается,
                    RequestType = update.GetCacheData<OperationCache>().Operation,
                    TelegramChatId = update.Message.Chat.Id,
                    Employee = employe
                };
                context.RequestsForDays.Add(request);
                context.SaveChanges();
                var message = $@"
Ваша заявка №{request.Number} успешно создана. Статус: Рассматривается.";

                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
                await MenuCommand.Menu(botClient, update);
            }
            else
            {
                var message = $@"
Ваша заявка #4324234 отменена.";
                await MenuCommand.Menu(botClient, update);
            }
            update.ClearStepUser();
        }

        [ReplyMenuHandler("ignore")]
        public static async Task IgnoreStep(ITelegramBotClient botClient, Update update)
        {
            string msg = "";
            //Есть шаг в цепочке
            if (update.HasStep())
            {
                msg = "Следующий шаг проигнорирован";
                update.ClearStepUser();
            }
            else //Нет шага в цепочке
            {
                msg = "Следующий шаг отсутсвует";
            }
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
