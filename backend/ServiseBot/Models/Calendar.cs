using CalendarPicker.CalendarControl;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ServiseBot.Models
{
    internal class Calendar
    {
        /// <summary>
        /// Русский формат текста даты
        /// </summary>
        public static DateTimeFormatInfo dtfi = CultureInfo.GetCultureInfo("ru-RU", false).DateTimeFormat;

        /// <summary>
        /// Точка входа в календарь
        /// Напишите в чате Calendar
        /// </summary>
        [ReplyMenuHandler("Calendar")]
        public static async Task CalendarPick(ITelegramBotClient botClient, Update update)
        {
            var calendar = Markup.Calendar(DateTime.Now, dtfi);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = calendar;
            await PRTelegramBot.Helpers.Message.Send(botClient, update.GetChatId(), "Укажите дату:", option);

        }

        /// <summary>
        /// Выбор года или месяц
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.YearMonthPicker)]
        public static async Task PickYearMonth(ITelegramBotClient botClient, Update update)
        {
            var commnad = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (commnad != null)
            {
                var data = Markup.PickMonthYear(commnad.Data.Date, dtfi);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = data;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        /// <summary>
        /// Выбор конкретного месяца
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.PickMonth)]
        public static async Task PickMonth(ITelegramBotClient botClient, Update update)
        {
            var commnad = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (commnad != null)
            {
                var data = Markup.PickMonth(commnad.Data.Date, dtfi);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = data;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        /// <summary>
        /// Выбор года
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.PickYear)]
        public static async Task PickYear(ITelegramBotClient botClient, Update update)
        {
            var commnad = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (commnad != null)
            {
                var data = Markup.PickYear(commnad.Data.Date, dtfi);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = data;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        /// <summary>
        /// Главное меню календаря
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.ChangeTo)]
        public static async Task ChangeHandler(ITelegramBotClient botClient, Update update)
        {
            var commnad = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (commnad != null)
            {
                var data = Markup.Calendar(commnad.Data.Date, dtfi);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = data;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        /// <summary>
        /// Выбор даты
        /// </summary>
        [InlineCallbackHandler<THeader>(THeader.PickDate)]
        public static async Task PickDate(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                var date = command.Data.Date;
                await PRTelegramBot.Helpers.Message.Edit(botClient, update, $"Дата:{date}");
            }
        }
    }
}
