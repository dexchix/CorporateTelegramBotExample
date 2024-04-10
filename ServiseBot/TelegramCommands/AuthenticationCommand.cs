using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using ServiseBot.Models;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using PRTelegramBot.Extensions;
using DAL;
using System.Data.Entity;
using PRTelegramBot.Helpers;

namespace ServiseBot.TelegramCommands
{
    public class AuthenticationCommand
    {
        [ReplyMenuHandler("/start")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<OperationCache>().Operation = update.Message.Text;
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(CheckEmployee));
            await PRTelegramBot.Helpers.Message.Send(botClient, update,
                @"
Введите ваш логин для работы с ботом.
Для отмены напишите /cancel
");
        }

        public static async Task CheckEmployee(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            if(update.Message.Text == "Нет")
            {
                return;
            }

            using var context = new ServiceBotContext();
            var employes = context.Employes
                .Where(x => x.Login == update.Message.Text)
                .ToList();
            string message;

            if (employes.Count > 1)
            {
                 message = @"
Логин не идентичен. 
Обратитесь к администратору по номеру +70001112233.
";
                update.ClearStepUser();
                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            }
            else if (employes.Count == 0)
            {
                 message = @"
Ваша учетная запись не найдена в системе. 
Обратитесь к администратору по номеру +70001112233.
";
                update.ClearStepUser();
                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            }
            var employe = employes[0];

            if (string.IsNullOrWhiteSpace(employe.Phone))
            {
                 message = @"
К вашей учетной записи не привязан номер телефона. 
Обратитесь к администратору по номеру +70001112233.
";
                update.ClearStepUser();
                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            }
            update.GetCacheData<OperationCache>().Login = update.Message.Text;
            // какая то  то логика по отправки SMS
            message = @"
Введите SMS код, который пришел вам на телефон.
";

            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(CheckSmsCode));
            await PRTelegramBot.Helpers.Message.Send(botClient, update, message);

        }

        public static async Task CheckSmsCode(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            string message;
            bool resultSucces = false;
            if (resultSucces)
            {
                message = @"
Поздравляем! 
Регистрация прошла успешно!
";
               await MenuCommand.Menu(botClient, update);
            }
            else
            {
                message = @"
Вы ввели неверный SMS код. 
Отправить повторно?
";

                update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(CheckEmployee));
                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);


                var menuList = new List<KeyboardButton>();
                //Создаем список string 
                var menuListString = new List<string>();

                menuList.Add(new KeyboardButton("Да"));
                menuList.Add(new KeyboardButton("Нет"));

                //Генерация меню в 2 столбца
                var menu = MenuGenerator.ReplyKeyboard(1, menuList);

                var option = new OptionMessage();
                option.MenuReplyKeyboardMarkup = menu;

                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
            }
        }
    }
}
