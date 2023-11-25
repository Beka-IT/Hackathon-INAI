using Telegram.Bot;

namespace WebApi
{
    public class TelegramBotService
    {
        private const string token = "6955458394:AAGlGwUiX52scsnv1Y8IfMNwap3RGE8fR44";
        private const string chatId = "-4081441656";

        public async Task SendMessage(string message)
        {
            TelegramBotClient bot = new TelegramBotClient(token);
            await bot.SendTextMessageAsync(chatId, message);
        }
    }
}
