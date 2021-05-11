using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class EditBookCommand : FindBookCommand
    {
        private string selectedMessage = "Edit book";
        private string editMessage = "Edit:";
        private string errorMessage = "Error. There is no book with this name or id.";

        public EditBookCommand() : base("/edit") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Book book = FindItem(context);

            if (book != null)
            {
                context.SelectedBook = book;
                context.ChangeState(CommandState.EditBookMenu);

                InlineKeyboardButton[][] buttons = new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Title", "/title"), InlineKeyboardButton.WithCallbackData("Description", "/description")},
                    new[] { InlineKeyboardButton.WithCallbackData("Set image", "/image"), InlineKeyboardButton.WithCallbackData("Remove image", "/remove_image")},
                    new[] { InlineKeyboardButton.WithCallbackData("In progress", "/category0"), InlineKeyboardButton.WithCallbackData("Completed", "/category1"), InlineKeyboardButton.WithCallbackData("Planned", "/category2") },
                    new[] { InlineKeyboardButton.WithCallbackData("Back", "/back")},
                };

                var keyboard = new InlineKeyboardMarkup(buttons);
                await BotClient.SendTextMessageAsync(context.Message.Chat, selectedMessage, replyMarkup: new ReplyKeyboardRemove());
                await BotClient.SendTextMessageAsync(context.Message.Chat, editMessage, replyMarkup: keyboard);
            }
            else
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, errorMessage);
            }
        }
    }
}