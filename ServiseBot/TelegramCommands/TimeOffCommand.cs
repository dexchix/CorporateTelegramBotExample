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
    internal class TimeOffCommand
    {
        public class RecyclingRequestCommand
        {
            //[ReplyMenuHandler("Заявка на отгул")]
            //public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
            //{
            //    update.GetCacheData<OperationCache>().Operation = DAL.Models.Enums.RequestType.Отгул;
            //    await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату начала отгула в формате - ДД.ММ.ГГГГ ЧЧ.ММ.СС:");
            //    update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
            //}

            //public static async Task ReceivingDateStart(ITelegramBotClient botClient, Update update, CustomParameters args)
            //{
            //    DateTime dateTimeReceiving;
            //    if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            //    {
            //        update.GetCacheData<OperationCache>().DateStart = dateTimeReceiving;
            //        update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateEnd));
            //        await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату конца отгула в формате - ДД.ММ.ГГГГ ЧЧ.ММ.СС:");
            //    }
            //    else
            //    {
            //        await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
            //        update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
            //    }
            //}

            //public static async Task ReceivingDateEnd(ITelegramBotClient botClient, Update update, CustomParameters args)
            //{
            //    DateTime dateTimeReceiving;
            //    if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            //    {
            //        update.GetCacheData<OperationCache>().DateEnd = dateTimeReceiving;
            //        update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingSubstantiation));
            //        await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите описание:");
            //    }
            //    else
            //    {
            //        await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
            //        update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
            //    }
            //}


            public static async Task ReceivingSubstantiation(ITelegramBotClient botClient, Update update, CustomParameters args)
            {
                update.GetCacheData<OperationCache>().Description = update.Message.Text;
                var message = @$"
Ваша заявка #324324. 
{update.GetCacheData<OperationCache>().Operation}.           
{update.GetCacheData<OperationCache>().DateStart} - {update.GetCacheData<OperationCache>().DateEnd}.
Обоснованиее: {update.GetCacheData<OperationCache>().Description}";


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
                }
                else
                {
                    var message = $@"
Ваша заявка #4324234 отменена.";
                    await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
                }
                update.ClearStepUser();
                await MenuCommand.Menu(botClient, update);
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
}
