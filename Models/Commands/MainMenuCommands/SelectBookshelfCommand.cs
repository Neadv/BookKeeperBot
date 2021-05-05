using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookshelfCommand : FindBookshelfCommand
    {
        private string selectedMessage = "Select bookshelf";
        private string errorMessage = "Error. There is no bookshelf with this name or id.";

        public SelectBookshelfCommand()
        {
            Name = "/select";
            State = CommandState.MainMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Bookshelf bookshelf = FindBookshelf(context);

            string message = errorMessage;
            if (bookshelf != null)
            {
                message = selectedMessage;
                context.SelectedBookshelf = bookshelf;
                context.ChangeState(CommandState.BookMenu);
            }

            await BotClient.SendTextMessageAsync(context.Message.Chat, message);
        }
    }
}