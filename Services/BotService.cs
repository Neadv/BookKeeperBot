using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace BookKeeperBot.Services
{
    public class BotService : IBotService
    {
        public ITelegramBotClient Client { get; }

        private readonly BotConfiguration config;

        public BotService(IOptions<BotConfiguration> botConfig)
        {
            config = botConfig.Value;
            Client = new TelegramBotClient(config.Token);
        }

        public void SetWebhook()
        {
            Client.SetWebhookAsync(config.WebhookUrl).GetAwaiter().GetResult();
        }
    }
}