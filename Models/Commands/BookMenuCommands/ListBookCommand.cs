using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
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

            if (books.Count() == 0)
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, "It's still empty here");
            }

            if (context.CommandName == Name || context.CommandName == inProgress)
                await SendBooks("In progress:", context.SelectedBookshelf.GetInProgress(), context.Message.Chat);
            if (context.CommandName == Name || context.CommandName == completed)
                await SendBooks("Completed:", context.SelectedBookshelf.GetCompleted(), context.Message.Chat);
            if (context.CommandName == Name || context.CommandName == planned)
                await SendBooks("Planned:", context.SelectedBookshelf.GetPlanned(), context.Message.Chat);
        }

        private async Task SendBooks(string message, IEnumerable<Book> books, ChatId chatId)
        {
            if (books.Count() > 0)
            {
                List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

                foreach (var book in books)
                {
                    var row = new List<InlineKeyboardButton>();
                    row.Add(InlineKeyboardButton.WithCallbackData(book.Title, $"/list {book.Id}"));
                    buttons.Add(row);
                }

                IReplyMarkup keyboard = new InlineKeyboardMarkup(buttons);
                await BotClient.SendTextMessageAsync(chatId, message, replyMarkup: keyboard);
            }
        }

        private async Task EditCallbackMessage(CommandContext context)
        {
            if (context.Message != null && context.Message.ReplyMarkup != InlineKeyboardMarkup.Empty())
                await BotClient.EditMessageReplyMarkupAsync(context.Message.Chat, context.Message.MessageId, InlineKeyboardMarkup.Empty());
            context.RedirectToCommand($"/select{context.Parameters}");
        }
    }
}