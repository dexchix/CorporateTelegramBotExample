using Telegram.Bot;
using Telegram.Bot.Types;

namespace ServiseBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var client = new TelegramBotClient("6858392505:AAHXlxagKKKFiZE0N5XUGbRwTnYxJa6Az-A");
            client.StartReceiving(Update, Error);


            Console.ReadLine();
        }

        async private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
           
        }

        async private static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var dawd = client.
            var message = update.Message; 
            if(message.Text != null)
            {
               await client.SendTextMessageAsync(message.Chat.Id, "Привет");
            }
        }
    }
}
