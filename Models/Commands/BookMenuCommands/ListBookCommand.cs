using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class ListBookCommand : Command
    {
        private string inProgress = "/in_progress";
        private string completed = "/completed";
        private string planned = "/planned";

        public ListBookCommand()
        {
            Name = "/list";
            State = CommandState.BookMenu;
            Alias = new string[] { inProgress, completed, planned };
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (context.IsCallback)
            {
                await EditCallbackMessage(context);
                return;
            }

            IEnumerable<Book> books = context.SelectedBookshelf.Books;

            if (context.CommandName == inProgress)
                books = context.SelectedBookshelf.GetInProgress();
            else if (context.CommandName == completed)
                books = context.SelectedBookshelf.GetCompleted();
            else if (context.CommandName == planned)
                books = context.SelectedBookshelf.GetPlanned();

            var message = "Books:";
            IReplyMarkup keyboard = null;
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

            foreach (var book in books)
            {
                var row = new List<InlineKeyboardButton>();
                row.Add(InlineKeyboardButton.WithCallbackData(book.Title, $"/list {book.Id}"));
                buttons.Add(row);
            }

            if (buttons.Count > 0)
                keyboard = new InlineKeyboardMarkup(buttons);

            if (keyboard == null)
                await BotClient.SendTextMessageAsync(context.Message.Chat, message);
            else
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, replyMarkup: keyboard);
        }

        private async Task EditCallbackMessage(CommandContext context)
        {
            if (context.Message != null && context.Message.ReplyMarkup != InlineKeyboardMarkup.Empty())
                await BotClient.EditMessageReplyMarkupAsync(context.Message.Chat, context.Message.MessageId, InlineKeyboardMarkup.Empty());
            context.RedirectToCommand($"/select{context.Parameters}");
        }
    }
}