using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookshelfCommand : FindBookshelfCommand
    {
        private string selectedMessage = "Select bookshelf";
        private string errorMessage = "Error. There is no bookshelf with this name or id.";

        public SelectBookshelfCommand() : base("/select") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Bookshelf bookshelf = FindItem(context);
            IReplyMarkup keyboard = null;

            string message = errorMessage;
            if (bookshelf != null)
            {
                message = selectedMessage;
                context.SelectedBookshelf = bookshelf;
                context.ChangeState(CommandState.BookMenu);

                keyboard = CommandKeyboards.BookMenuKeyboard;
            }

            if (keyboard == null)
                await BotClient.SendTextMessageAsync(context.Message.Chat, message);
            else
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, replyMarkup: keyboard);
        }
    }
}