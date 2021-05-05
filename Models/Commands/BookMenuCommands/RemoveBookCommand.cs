using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookCommand : Command
    {
        private string enterMessage = "Enter the title of the book";
        private string removedMessage = "The book has removed";
        private string errorMessage = "Error. There is no book with that title. Enter the title of an existing book.";

        public RemoveBookCommand()
        {
            Name = "/remove";
            State = CommandState.BookMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (string.IsNullOrEmpty(context.Data) && string.IsNullOrEmpty(context.Parameters))
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, enterMessage);
            }
            else
            {
                string title = context.Parameters ?? context.Data;
                string message;
                var book = context.SelectedBookshelf.Books.Find(b => b.Title.ToLower() == title.ToLower());
                if (book != null)
                {
                    context.RemoveBook(book);
                    message = removedMessage;
                    context.CommandName = null;
                }
                else
                {
                    message = errorMessage;
                    context.CommandName = Name;
                }
                await BotClient.SendTextMessageAsync(context.Message.Chat, message);
            }
        }

        public override bool Check(CommandString command)
        {
            if (base.Check(command))
                return true;

            if (command.State != State)
                return false;

            if (command.CommandName != null)
                return false;

            return command.PreviosCommand == Name && command.ContainData;
        }
    }
}