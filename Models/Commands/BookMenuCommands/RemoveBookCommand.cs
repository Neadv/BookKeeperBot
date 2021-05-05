using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookCommand : InputBookCommand
    {
        public RemoveBookCommand()
        {
            Name = "/remove";
            State = CommandState.BookMenu;

            EnterMessage = "Enter the tile of the book";
            ExistMessage = "The book has removed";
            NoExitstMessage = "There is no book with that title.\nEnter the title of an existing book";
            Message = ExistMessage;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            var book = context.SelectedBook;
            if (book != null || InputBook(context, out book))
            {
                if (book != null)
                {
                    context.RemoveBook(book);
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