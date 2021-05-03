using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class SelectBookshelfCommand : Command
    {
        private string selectedMessage = "Select bookshelf";
        private string errorMessage = "Error. There is no bookshelf with this name or id.";

        public SelectBookshelfCommand()
        {
            Name = "/select";
            State = CommandState.MainMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Bookshelf bookshelf = null;

            if (!string.IsNullOrEmpty(context.Parameters))
            {
                bookshelf = context.Bookshelves.Find(b => b.Name.ToLower() == context.Parameters.ToLower());
            }
            else
            {
                string value = context.CommandName.Replace(Name, "");
                if (int.TryParse(value, out int id))
                {
                    bookshelf = context.Bookshelves.Find(b => b.Id == id);
                }
            }

            string message;
            if (bookshelf != null)
            {
                context.SelectedBookshelf = bookshelf;
                message = selectedMessage;
                context.ChangeState(CommandState.BookMenu);
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