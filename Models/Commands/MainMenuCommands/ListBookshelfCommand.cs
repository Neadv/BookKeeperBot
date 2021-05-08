using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class ListBookshelfCommand : Command
    {
        public ListBookshelfCommand()
        {
            Name = "/list";
            State = CommandState.MainMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (context.IsCallback)
            {
                await EditCallbackMessage(context);
                return;
            }

            var message = "Bookshelves:";
            IReplyMarkup keyboard = null;
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

            foreach (var bookshelf in context.Bookshelves)
            {
                var row = new List<InlineKeyboardButton>();
                row.Add(InlineKeyboardButton.WithCallbackData(bookshelf.Name, $"/list {bookshelf.Id}"));
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
            if (context.Message != null)
            {
                var message = new StringBuilder("Bookshelves:\n");
                foreach (var bookshelf in context.Bookshelves)
                {
                    message.AppendLine(bookshelf.Name);
                }

                await BotClient.EditMessageTextAsync(
                    context.Message.Chat,
                    context.Message.MessageId,
                    message.ToString(),
                    replyMarkup: InlineKeyboardMarkup.Empty()
                );
            }
            context.RedirectToCommand($"/select{context.Parameters}");
        }
    }
}