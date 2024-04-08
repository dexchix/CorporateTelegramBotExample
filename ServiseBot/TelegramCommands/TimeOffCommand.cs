using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using ServiseBot.Models.Caches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;

namespace ServiseBot.TelegramCommands
{
    internal class TimeOffCommand
    {
        public class RecyclingRequestCommand
        {
            [ReplyMenuHandler("Заявка на отгул")]
            public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
            {
                update.GetCacheData<TimeOfCache>().Operation = update.Message.Text;
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату начала отгула в формате - ДД.ММ.ГГГГ ЧЧ.ММ.СС:");
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
            }

            public static async Task ReceivingDateStart(ITelegramBotClient botClient, Update update, CustomParameters args)
            {
                DateTime dateTimeReceiving;
                if (DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
                {
                    update.GetCacheData<TimeOfCache>().DateStart = dateTimeReceiving;
                    update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateEnd));
                    await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату конца отгула в формате - ДД.ММ.ГГГГ ЧЧ.ММ.СС:");
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
                    update.GetCacheData<TimeOfCache>().DateEnd = dateTimeReceiving;
                    update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingSubstantiation));
                    await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите обоснование:");
                }
                else
                {
                    await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
                    update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDateStart));
                }
            }


            public static async Task ReceivingSubstantiation(ITelegramBotClient botClient, Update update, CustomParameters args)
            {
                update.GetCacheData<TimeOfCache>().Substantiation = update.Message.Text;
                var message = @$"
Ваша заявка #324324. 
{update.GetCacheData<TimeOfCache>().Operation}.           
{update.GetCacheData<TimeOfCache>().DateStart} - {update.GetCacheData<TimeOfCache>().DateEnd}.
Обоснованиее: {update.GetCacheData<TimeOfCache>().Substantiation}";


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
