using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookshelfCommand : Command
    {
        private string enterMessage = "Enter the name of the bookshelf";
        private string removedMessage = "The bookshelf has removed";
        private string errorMessage = "Error. There is no bookshelf with that name. Enter the name of an existing bookshelf.";

        public RemoveBookshelfCommand()
        {
            Name = "/remove";
            State = CommandState.MainMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (string.IsNullOrEmpty(context.Data) && string.IsNullOrEmpty(context.Parameters))
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, enterMessage);
            }
            else
            {
                string name = context.Parameters ?? context.Data;
                string message;
                var bookshelf = context.Bookshelves.Find(b => b.Name.ToLower() == name.ToLower());
                if (bookshelf != null)
                {
                    context.RemoveBookshelf(bookshelf);
                    message = removedMessage;
                    context.CommandName = null;
                }
                else
                {
                    message = errorMessage;
                    context.CommandName = Name;
                }
                await BotClient.SendTextMessageAsync(context.Message.Chat, message);
            }
        }

        public override bool Check(CommandString command)
        {
            if (base.Check(command))
                return true;

            if (command.State != State)
                return false;

            if (command.CommandName != null)
                return false;

            return command.PreviosCommand == Name && command.ContainData;
        }
    }
}