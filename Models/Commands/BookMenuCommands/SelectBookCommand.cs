using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookCommand : FindBookCommand
    {
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

                string message = $"<strong>{book.Title}</strong>\n\n{book.Description}";

                string image = null;
                if (book.ImageId != null)
                    image = book.ImageId;
                else if (book.ImageUrl != null)
                    image = book.ImageUrl;

                if (!string.IsNullOrEmpty(image))
                {
                    var photoMessage = await BotClient.SendPhotoAsync(context.Message.Chat, image, message, ParseMode.Html, replyMarkup: keyboard);
                    
                    if (book.ImageId == null){
                        book.ImageId = photoMessage.Photo?[0].FileId;
                    }
                }
                else
                {
                    await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html, replyMarkup: keyboard);
                }
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
                await BotClient.EditMessageReplyMarkupAsync(context.Message.Chat, context.Message.MessageId, InlineKeyboardMarkup.Empty());
                string command = string.IsNullOrEmpty(context.Parameters) ? "/remove" : $"/edit{context.Parameters}";
                context.RedirectToCommand(command);
            }
        }
    }
}