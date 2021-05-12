using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Models.Commands
{
    public class StartCommand : Command
    {
        private string messageNewUser = "<strong>Hi, I am BookKeeper bot. I can keep your books and help you arrange them on the bookshelves.\n" +
                                        "Also I can find the book and its descriptions by title. </strong>\n\n" +
                                        "/init - <em>Create an initial structure</em>\n" +
                                        "/menu - <em>Open menu</em>";

        private string restartMessage = "<strong>Hi again</strong>";

        public StartCommand()
        {
            Name = "/start";
            Authorized = false;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (context.User == null)
            {
                var telegramUser = context.Message.From;
                var user = new User
                {
                    Id = telegramUser.Id,
                    Username = telegramUser.Username
                };
                context.User = user;
                await BotClient.SendTextMessageAsync(context.Message.Chat, messageNewUser, ParseMode.Html);
            }
            else
            {
                context.ChangeState(CommandState.MainMenu);
                await BotClient.SendTextMessageAsync(context.Message.Chat, restartMessage, ParseMode.Html, replyMarkup: CommandKeyboards.MainMenuKeyboad);
            }
        }
    }
}