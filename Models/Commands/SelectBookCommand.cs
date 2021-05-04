using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookCommand : Command
    {
        private string selectedMessage = "Select book";
        private string errorMessage = "Error. There is no book with this name or id.";

        public SelectBookCommand()
        {
            Name = "/select";
            State = CommandState.BookMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Book book = null;

            if (!string.IsNullOrEmpty(context.Parameters))
            {
                book = context.SelectedBookshelf.Books.Find(b => b.Title.ToLower() == context.Parameters.ToLower());
            }
            else
            {
                string value = context.CommandName.Replace(Name, "");
                if (int.TryParse(value, out int id))
                {
                    book = context.SelectedBookshelf.Books.Find(b => b.Id == id);
                }
            }

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
        
        public override bool Check(CommandString command)
        {
            if (base.Check(command))
                return true;

            if (!command.IsAuthorized)
                return false;

            if (command.State != State)
                return false;

            return command.CommandName != null && command.CommandName.StartsWith(Name);
        }
    }
}