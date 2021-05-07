using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookCommand : InputBookCommand
    {
        public RemoveBookCommand() : base("/remove")
        {
            EnterMessage = "Enter the tile of the book";
            ExistMessage = "The book has removed";
            NoExitstMessage = "There is no book with that title.\nEnter the title of an existing book";
            Message = ExistMessage;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            var book = context.SelectedBook;
            IReplyMarkup keyboard = new ReplyKeyboardRemove();
            if (book != null || InputData(context, out book))
            {
                if (book != null)
                {
                    context.RemoveBook(book);
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