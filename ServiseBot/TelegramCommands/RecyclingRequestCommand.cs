using PRTelegramBot.Attributes;
using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;
using ServiseBot.Models.Caches;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Helpers;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.VisualBasic.FileIO;

namespace ServiseBot.TelegramCommands
{
    internal class RecyclingRequestCommand
    {
        [ReplyMenuHandler("Заявка на отгул")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<RecyclingCache>().Operation = update.Message.Text;
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите дату в формате - ДД.ММ.ГГГГ:");
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDate));
        }

        /// <summary>
        /// Обработка данных шага 1
        /// </summary>
        public static async Task ReceivingDate(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            DateTime dateTimeReceiving;
            if(DateTime.TryParse(update.Message.Text, out dateTimeReceiving))
            {
                update.GetCacheData<RecyclingCache>().DateRecycling = dateTimeReceiving;
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingSubstantiation));
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Введите обоснование:");
            }
            else
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Дата некорректна. Повторите попытку.");
                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(ReceivingDate));
            }
        }

        /// <summary>
        /// Обработка данных шага 2
        /// </summary>
        public static async Task ReceivingSubstantiation(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            update.GetCacheData<RecyclingCache>().Substantiation = update.Message.Text;
            var message = @$"
Ваша заявка #324324. 
{update.GetCacheData<RecyclingCache>().Operation}.           
{update.GetCacheData<RecyclingCache>().DateRecycling}.
Обоснованиее: {update.GetCacheData<RecyclingCache>().Substantiation}";
            
            
            var menuList = new List<KeyboardButton>();
            menuList.Add(new KeyboardButton("Подтвердить"));
            menuList.Add(new KeyboardButton("Отмена"));

            //Генерация меню в 2 столбца
            var menu = MenuGenerator.ReplyKeyboard(1, menuList);
            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(CreateReceivingRequest));
        }

        public static async Task CreateReceivingRequest(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            if(update.Message.Text == "Подтвердить")
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

        /// <summary>
        /// Игнорирование пошагового выполнения команд
        /// Напиши в боте ignore
        /// </summary>
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
