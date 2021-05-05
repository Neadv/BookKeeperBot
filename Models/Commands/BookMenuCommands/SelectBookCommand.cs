using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookCommand : FindBookCommand
    {
        private string selectedMessage = "Select book";
        private string errorMessage = "Error. There is no book with this name or id.";

        public SelectBookCommand() : base("/select") {}

        public async override Task ExecuteAsync(CommandContext context)
        {
            Book book = FindItem(context);
            
            string message;
            if (book != null)
            {
                context.SelectedBook = book;
                message = selectedMessage;
            }
            else
            {
                message = errorMessage;
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, message);
        }
    }
}