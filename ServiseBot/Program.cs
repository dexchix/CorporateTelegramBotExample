using DAL;
using DAL.Models;
using PRTelegramBot.Core;
using System.Data.Entity;


//var employe = new Employe()
//{
//    Id = Guid.NewGuid(),
//    Department = "ОРПО",
//    FullName = "Тестов Тест Тестович",
//    IsAutorized = false,
//    Login = "test",
//    Phone = "79803391975",
//};
//var requestForDay = new RequestsForDays()
//{
//    Id = 0,
//    Description = "Отгул в связи с сессией",
//    CreateDate = DateTime.UtcNow,
//    Employee = employe,
//    EndDate = DateTime.UtcNow,
//    RequestStatus = DAL.Models.Enums.RequestStatus.Pending,
//    RequestType = DAL.Models.Enums.RequestType.TimeOf,
//    TelegramChatId = 454353,
//    StartDate = DateTime.UtcNow
//};
using var context = new ServiceBotContext();
//context.Add(requestForDay);
//context.SaveChanges();  

//Команда для выхода
const string EXIT_COMMAND = "exit";

var employe = context.RequestsForDays.Include(x=> x.Employee).First();

//Телеграм бот с id 0 
var telegram = new PRBot(option =>
{
    // Токен телеграм бота берется из BotFather
    option.Token = "6858392505:AAHXlxagKKKFiZE0N5XUGbRwTnYxJa6Az-A";
    //Перед запуском очищает список обновлений, которые накопились когда бот не работал.
    option.ClearUpdatesOnStart = true;
    // Если есть хоть 1 идентификатор телеграм пользователя, могут пользоваться только эти пользователи
    option.WhiteListUsers = new List<long>() { };
    // Идентификатор телеграм пользователя
    option.Admins = new List<long>() { };
    // Уникальных идентификатор для бота, используется, чтобы в одном приложение запускать несколько ботов
    option.BotId = 0;
});

//Телеграм бот с id 1
var telegramx = new PRBot(option =>
{
    // Токен телеграм бота берется из BotFather
    option.Token = "";
    //Перед запуском очищает список обновлений, которые накопились когда бот не работал.
    option.ClearUpdatesOnStart = true;
    // Если есть хоть 1 идентификатор телеграм пользователя, могут пользоваться только эти пользователи
    option.WhiteListUsers = new List<long>() { };
    // Идентификатор телеграм пользователя
    option.Admins = new List<long>() { };
    // Уникальных идентификатор для бота, используется, чтобы в одном приложение запускать несколько ботов
    option.BotId = 1;
});



//Подписка на логи бота 0
telegram.OnLogCommon += Telegram_OnLogCommon;
telegram.OnLogError += Telegram_OnLogError;

//Подписка на логи бота 1
telegramx.OnLogCommon += Telegram_OnLogCommon;
telegramx.OnLogError += Telegram_OnLogError;

//Запуск бота id 0
await telegram.Start();
//Запуск бота id 1
await telegramx.Start();

//События логов ошибок
void Telegram_OnLogError(Exception ex, long? id)
{
    Console.ForegroundColor = ConsoleColor.Red;
    string errorMsg = $"{DateTime.Now}:{ex}";
    Console.WriteLine(errorMsg);
    Console.ResetColor();
}

//Событие обычных логов
void Telegram_OnLogCommon(string msg, PRBot.TelegramEvents typeEvent, ConsoleColor color)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    string message = $"{DateTime.Now}:{msg}";
    Console.WriteLine(message);
    Console.ResetColor();
}

while (true)
{
    var result = Console.ReadLine();
    if (result.ToLower() == EXIT_COMMAND)
    {
        Environment.Exit(0);
    }
}


