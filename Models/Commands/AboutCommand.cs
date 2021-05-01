using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Models.Commands
{
    public class AboutCommand : Command
    {
        private string mainMenuMessage = "<em>Main menu command list:</em>";
        private string bookMenuMessage = "<em>Book menu command list:</em>";

        public AboutCommand()
        {
            Name = "/about";
            State = CommandState.NoContext;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = context.State switch
            {
                CommandState.MainMenu => mainMenuMessage,
                CommandState.BookMenu => bookMenuMessage,
                _ => null
            };

            if (message != null)
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html);
        }
    }
}