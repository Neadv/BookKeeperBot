using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class EditBookCommand : FindBookCommand
    {
        private string selectedMessage = "Edit book\n/description\n/title\n/image\n/note\n/category\n/back";
        private string errorMessage = "Error. There is no book with this name or id.";

        public EditBookCommand() : base("/edit") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Book book = FindItem(context);
            IReplyMarkup keyboard = null;

            string message;
            if (book != null)
            {
                context.SelectedBook = book;
                message = selectedMessage;
                context.ChangeState(CommandState.EditBookMenu);

                keyboard = new ReplyKeyboardRemove();
            }
            else
            {
                message = errorMessage;
            }

            if (keyboard == null)
                await BotClient.SendTextMessageAsync(context.Message.Chat, message);
            else
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, replyMarkup: keyboard);
        }
    }
}