using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditBookshelfCommand : Command
    {
        private string enterMessage = "Enter a new name for the selected bookshelf.";
        private string editedMessage = "The bookshelf has edited.";
        private string errorMessage = "Error. There is no bookshelf with this name or id.";
        private string noSelectedMessage = "Error. No bookshelf selected.";

        public EditBookshelfCommand()
        {
            Name = "/edit";
            State = CommandState.MainMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = noSelectedMessage;
            string commandName = context.CommandName;
            if (!string.IsNullOrEmpty(commandName))
            {
                if ((commandName == Name && !string.IsNullOrEmpty(context.Parameters)) || commandName.Length > Name.Length)
                {
                    message = SelectBookshelf(context);
                }
                else if (commandName == Name && context.SelectedBookshelf != null)
                {
                    message = enterMessage;
                }
            }
            else if (context.SelectedBookshelf != null && !string.IsNullOrEmpty(context.Data))
            {
                context.SelectedBookshelf.Name = context.Data;
                message = editedMessage;
            }

            await BotClient.SendTextMessageAsync(context.Message.Chat, message);
        }

        private string SelectBookshelf(CommandContext context)
        {
            Bookshelf bookshelf = null;

            if (!string.IsNullOrEmpty(context.Parameters))
            {
                bookshelf = context.Bookshelves.Find(b => b.Name.ToLower() == context.Parameters.ToLower());
            }
            else if (!string.IsNullOrEmpty(context.CommandName))
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
                message = enterMessage;
            }
            else
            {
                message = errorMessage;
            }
            return message;
        }

        public override bool Check(CommandString command)
        {
            if (base.Check(command))
                return true;

            if (!command.IsAuthorized)
                return false;

            if (command.State != State)
                return false;

            if (command.CommandName != null && command.CommandName.StartsWith(Name))
                return true;

            return command.PreviosCommand != null && command.PreviosCommand.StartsWith(Name) && command.ContainData;
        }
    }
}