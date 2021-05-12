using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookCommand : InputBookCommand
    {
        public AddBookCommand() : base("/add")
        {
            Alias = new[] { "/add_search" };
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
                    string title = context.Parameters ?? context.Data;

                    var bookAccessor = new BookDAO();

                    if (context.PreviousCommand == Alias[0])
                        book = await bookAccessor.GetBookAsync(title);

                    if (book == null)
                    {
                        book = new Book { Title = title };
                        if (context.PreviousCommand == Alias[0])
                            await BotClient.SendTextMessageAsync(context.Message.Chat, "I couldn't find this book :(, but you can fill it out yourself.");
                    }
                    else if (context.PreviousCommand == Alias[0])
                    {
                        await BotClient.SendTextMessageAsync(context.Message.Chat, "I found this book. I hope this is the book you wanted.");
                    }

                    book.Bookshelf = context.SelectedBookshelf;

                    context.AddBook(book);
                    context.CommandName = null;

                    keyboard = CommandKeyboards.BookMenuKeyboard;

                    context.RedirectToCommand("/select", book.Title);
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