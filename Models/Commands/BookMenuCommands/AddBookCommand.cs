using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookCommand : InputBookCommand
    {
        public AddBookCommand()
        {
            Name = "/add";
            State = CommandState.BookMenu;

            EnterMessage = "Enter a title for the new book";
            NoExitstMessage = "The book has added";
            ExistMessage = "There is book with that name.\nEnter a unique name";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (InputBook(context, out Book book))
            {
                if (book == null)
                {
                    book = new Book { Title = context.Parameters ?? context.Data, Bookshelf = context.SelectedBookshelf };
                    context.AddBook(book);
                    context.CommandName = null;
                }
                else
                {
                    context.CommandName = Name;
                }
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, Message);
        }
    }
}