using System;
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
            if (context.IsCallback)
            {
                await ProcessCallback(context);
                return;
            }

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
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, message);
            }
            else
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, replyMarkup: keyboard);

                keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Edit", $"/change1 /select {bookshelf.Id}"),
                        InlineKeyboardButton.WithCallbackData("Remove", $"/change1 /select")
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