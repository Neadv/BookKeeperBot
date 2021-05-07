using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookCommand : InputBookCommand
    {
        public AddBookCommand() : base("/add")
        {
            EnterMessage = "Enter a title for the new book";
            NoExitstMessage = "The book has added";
            ExistMessage = "There is book with that name.\nEnter a unique name";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            IReplyMarkup keyboard = new ReplyKeyboardRemove();
            if (InputData(context, out Book book))
            {
                if (book == null)
                {
                    book = new Book { Title = context.Parameters ?? context.Data, Bookshelf = context.SelectedBookshelf };
                    context.AddBook(book);
                    context.CommandName = null;

                    keyboard = CommandKeyboards.BookMenuKeyboard;
                }
                else
                {
                    context.CommandName = Name;
                }
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, Message, replyMarkup: keyboard);
        }
    }
}