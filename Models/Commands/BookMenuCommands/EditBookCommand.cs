using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditBookCommand : FindBookCommands
    {
        private string selectedMessage = "Edit book\n/description\n/title\n/image\n/note\n/category";
        private string errorMessage = "Error. There is no book with this name or id.";

        public EditBookCommand()
        {
            Name = "/edit";
            State = CommandState.BookMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Book book = FindBook(context);

            string message;
            if (book != null)
            {
                context.SelectedBook = book;
                message = selectedMessage;
                context.ChangeState(CommandState.EditBookMenu);
            }
            else
            {
                message = errorMessage;
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, message);
        }
    }
}