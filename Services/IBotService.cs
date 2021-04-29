using Telegram.Bot;

namespace BookKeeperBot.Services
{
    public interface IBotService
    {
        ITelegramBotClient Client { get; }

        void SetWebhook();
    }
}