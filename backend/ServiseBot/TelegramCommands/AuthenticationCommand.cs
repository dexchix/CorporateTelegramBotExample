using DAL;
using DAL.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using ServiseBot.Models;
using System.Data.Entity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ServiseBot.TelegramCommands
{
    public class AuthenticationCommand
    {
        [ReplyMenuHandler("/start")]
        public static async Task ReceivingOperation(ITelegramBotClient botClient, Update update)
        {
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(CheckEmployee));
            await PRTelegramBot.Helpers.Message.Send(botClient, update,
                @"
Введите ваш логин для работы с ботом.
Для отмены напишите /cancel
");
        }

        public static async Task CheckEmployee(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            if (update.Message.Text == "Нет")
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
            employe.TelegramId = update.Message.From.Id;
            employe.TelegramLogin = update.Message.Chat.Username;
            employe.IsAutorized = true;
            await context.SaveChangesAsync();


            message = @"
Введите SMS код, который пришел вам на телефон.
";

            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(CheckSmsCode));
            await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            //await PRTelegramBot.Helpers.Message.Send(botClient, update, message);

        }

        public static async Task CheckSmsCode(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            var context = new ServiceBotContext();
            string message;

            bool resultSucces = false;
            if (true)
            {
                var employe =  context.Employes
                    .Where(x=> update.Message.Chat.Username == x.TelegramLogin)
                    .FirstOrDefault();
                employe.IsAutorized = true;
                update.GetCacheData<OperationCache>().EmployeId = employe.Id;
                context.SaveChanges();



                message = @"
Поздравляем! 
Регистрация прошла успешно!
";
                update.ClearStepUser();
                await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
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
