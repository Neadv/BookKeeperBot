using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookCommand : FindBookCommand
    {
        private string selectedMessage = "Select book";
        private string errorMessage = "Error. There is no book with this name or id.";

        public SelectBookCommand() : base("/select") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (context.IsCallback)
            {
                await ProcessCallback(context);
                return;
            }

            Book book = FindItem(context);

            if (book != null)
            {
                context.SelectedBook = book;
                IReplyMarkup keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[] 
                    { 
                        InlineKeyboardButton.WithCallbackData("Edit", $"/select {book.Id}"),
                        InlineKeyboardButton.WithCallbackData("Remove", "/select")
                    }); 
                await BotClient.SendTextMessageAsync(context.Message.Chat, selectedMessage, replyMarkup: keyboard);
            }
            else
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, errorMessage);
            }
        }

        private async Task ProcessCallback(CommandContext context)
        {
            if (context.Message != null)
            {
                await  BotClient.EditMessageReplyMarkupAsync(context.Message.Chat, context.Message.MessageId, InlineKeyboardMarkup.Empty());
                string command = string.IsNullOrEmpty(context.Parameters) ? "/remove" : $"/edit{context.Parameters}";
                context.RedirectToCommand(command);
            }
        }
    }
}