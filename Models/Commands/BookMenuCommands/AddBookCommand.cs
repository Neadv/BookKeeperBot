using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookCommand : InputBookCommand
    {
        public AddBookCommand() : base("/add")
        {
            Alias = new[] { "/add_search" };
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            EnterMessage = Localizer["AddBookEnter"];
            NoExitstMessage = Localizer["AddBookSuccess"];
            ExistMessage = Localizer["AddBookError"];

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
                            await BotClient.SendTextMessageAsync(context.Message.Chat, Localizer["AddBookSearchError"]);
                    }
                    else if (context.PreviousCommand == Alias[0])
                    {
                        await BotClient.SendTextMessageAsync(context.Message.Chat, Localizer["AddBookSearchSuccess"]);
                    }

                    book.Bookshelf = context.SelectedBookshelf;

                    context.AddBook(book);
                    context.CommandName = null;

                    keyboard = CommandKeyboards.GetBookMenu(Localizer);

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