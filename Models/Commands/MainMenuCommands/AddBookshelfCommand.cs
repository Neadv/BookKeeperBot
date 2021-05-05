using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookshelfCommand : Command
    {
        private string enterMessage = "Enter a name for new bookshelf";
        private string addedMessage = "The bookshelf has added";
        private string errorMessage = "Error. There is a bookshelf with that name. Enter a new unique name";

        public AddBookshelfCommand()
        {
            Name = "/add";
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
                if (context.Bookshelves.Find(b => b.Name.ToLower() == name.ToLower()) == null)
                {
                    var bookshelf = new Bookshelf { Name = name, User = context.User };
                    context.AddBookshelf(bookshelf);
                    
                    message = addedMessage;
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