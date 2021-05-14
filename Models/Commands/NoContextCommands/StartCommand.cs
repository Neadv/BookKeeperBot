using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Models.Commands
{
    public class StartCommand : Command
    {
        private string messageNewUser = "<strong>{0}</strong>\n\n" +
                                        "/init - <em>{1}</em>\n" +
                                        "/menu - <em>{2}</em>";

        private string restartMessage = "<strong>{0}</strong>";

        public StartCommand()
        {
            Name = "/start";
            Authorized = false;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (context.User == null)
            {
                var message = string.Format(messageNewUser, Localizer["Start"], Localizer["InitDescription"], Localizer["MenuDescription"]);

                var telegramUser = context.Message.From;
                var user = new User
                {
                    Id = telegramUser.Id,
                    Username = telegramUser.Username,
                    Language = telegramUser.LanguageCode
                };
                context.User = user;
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html);
            }
            else
            {
                context.ChangeState(CommandState.MainMenu);
                await BotClient.SendTextMessageAsync(context.Message.Chat, string.Format(restartMessage, Localizer["Restart"]), ParseMode.Html, replyMarkup: CommandKeyboards.GetMainMenu(Localizer));
            }
        }
    }
}