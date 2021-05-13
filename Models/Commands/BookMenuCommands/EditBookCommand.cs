using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class EditBookCommand : FindBookCommand
    {
        private string selectedMessage = "<em>{0}</em> {1}";
        private string editMessage = "<strong>{0}</strong>";

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
                    new[] { InlineKeyboardButton.WithCallbackData(Localizer["EditBookTitle"], "/title"), InlineKeyboardButton.WithCallbackData(Localizer["EditBookDesc"], "/description")},
                    new[] { InlineKeyboardButton.WithCallbackData(Localizer["EditBookSetImage"], "/image"), InlineKeyboardButton.WithCallbackData(Localizer["EditBookRemoveImage"], "/remove_image")},
                    new[] { InlineKeyboardButton.WithCallbackData(Localizer["EditBookInProgress"], "/category0"), InlineKeyboardButton.WithCallbackData(Localizer["EditBookCompleted"], "/category1"), InlineKeyboardButton.WithCallbackData(Localizer["EditBookPlanned"], "/category2") },
                    new[] { InlineKeyboardButton.WithCallbackData(Localizer["EditBookBack"], "/back")},
                };

                var keyboard = new InlineKeyboardMarkup(buttons);
                await BotClient.SendTextMessageAsync(context.Message.Chat, string.Format(selectedMessage, book.Title, Localizer["EditBookSelected"]), ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
                await BotClient.SendTextMessageAsync(context.Message.Chat, string.Format(editMessage, Localizer["EditBookEdit"]), ParseMode.Html, replyMarkup: keyboard);
            }
            else
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, Localizer["EditBookError"]);
            }
        }
    }
}