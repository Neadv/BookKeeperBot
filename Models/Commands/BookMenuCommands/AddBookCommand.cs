using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookCommand : Command
    {
        private string enterMessage = "Enter a title for new book";
        private string addedMessage = "The book has added";
        private string errorMessage = "Error. There is a book with that name. Enter a new unique name";

        public AddBookCommand()
        {
            Name = "/add";
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
                if (context.SelectedBookshelf.Books.Find(b => b.Title.ToLower() == title.ToLower()) == null)
                {
                    var book = new Book { Title = title, Bookshelf = context.SelectedBookshelf };
                    context.AddBook(book);
                    
                    message = addedMessage;
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