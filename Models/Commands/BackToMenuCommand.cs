using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class BackToMenuCommand : Command
    {
        public BackToMenuCommand()
        {
            Name = "/back";
            State = CommandState.NoContext;
            Alias = new[] { "/menu", "/return" };
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            CommandState newState = CommandState.MainMenu;
            string message = "Main menu";

            if (context.State == CommandState.EditBookMenu)
            {
                context.SelectedBook = null;
                newState = CommandState.BookMenu;
                message = "Book menu";
            }
            else
            {
                context.SelectedBook = null;
                context.SelectedBookshelf = null;
            }

            context.ChangeState(newState);
            await BotClient.SendTextMessageAsync(context.Message.Chat, message);
        }
    }
}