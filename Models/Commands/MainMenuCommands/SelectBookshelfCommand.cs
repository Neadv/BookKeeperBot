using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookshelfCommand : FindBookshelfCommand
    {
        private string selectedMessage = "<em>{0}</em> {1}";

        public SelectBookshelfCommand() : base("/select") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (context.IsCallback)
            {
                await ProcessCallback(context);
                return;
            }

            Bookshelf bookshelf = FindItem(context);
            IReplyMarkup keyboard = null;

            string message = Localizer["SelectBookshelfError"];
            if (bookshelf != null)
            {
                message = string.Format(selectedMessage, bookshelf.Name, Localizer["SelectBookshelfSelected"]);
                context.SelectedBookshelf = bookshelf;
                context.ChangeState(CommandState.BookMenu);

                keyboard = CommandKeyboards.GetBookMenu(Localizer);
            }

            if (keyboard == null)
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html);
            }
            else
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html, replyMarkup: keyboard);

                keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(Localizer["EditButton"], $"/change1 /select {bookshelf.Id}"),
                        InlineKeyboardButton.WithCallbackData(Localizer["RemoveButton"], $"/change1 /select")
                    }
                );
                await BotClient.SendTextMessageAsync(context.Message.Chat, bookshelf.Name, replyMarkup: keyboard);

            }
        }

        private async Task ProcessCallback(CommandContext context)
        {
            if (context.Message != null && context.Message.ReplyMarkup != InlineKeyboardMarkup.Empty())
            {
                await BotClient.EditMessageReplyMarkupAsync(context.Message.Chat, context.Message.MessageId, InlineKeyboardMarkup.Empty());
                
                string command;
                if(string.IsNullOrEmpty(context.Parameters))
                    command = "/remove";
                else
                    command = $"/edit{context.Parameters}";

                context.RedirectToCommand(command);
            }
        }
    }
}