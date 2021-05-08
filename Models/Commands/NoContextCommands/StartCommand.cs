using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Models.Commands
{
    public class StartCommand : Command
    {
        private string messageNewUser = "<strong>There will be information about the bot.</strong>\n\n" +
                                        "/init - <em>Create an initial structure</em>\n" +
                                        "/menu - <em>Open menu</em>";

        private string restartMessage = "<strong>There will be information about the bot.</strong>";

        public StartCommand()
        {
            Name = "/start";
            Authorized = false;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message;
            if (context.User == null)
            {
                var telegramUser = context.Message.From;
                var user = new User
                {
                    Id = telegramUser.Id,
                    Username = telegramUser.Username
                };
                context.User = user;
                message = messageNewUser;
            }
            else
            {
                context.ChangeState(CommandState.MainMenu);
                message = restartMessage;
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html);
        }
    }
}