using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Models.Commands
{
    public class AboutCommand : Command
    {
        private string mainMenuMessage = "<em>Main menu command list:</em>";
        private string bookMenuMessage = "<em>Book menu command list:</em>";
        private string editBookMenuMessage = "<em>Edit book menu command list:</em>";

        public AboutCommand()
        {
            Name = "/about";
            Alias = new string[] { "/help", "/commands" };
            State = CommandState.NoContext;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = context.State switch
            {
                CommandState.MainMenu => mainMenuMessage,
                CommandState.BookMenu => bookMenuMessage,
                CommandState.EditBookMenu => editBookMenuMessage,
                _ => null
            };

            if (message != null)
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html);
        }
    }
}