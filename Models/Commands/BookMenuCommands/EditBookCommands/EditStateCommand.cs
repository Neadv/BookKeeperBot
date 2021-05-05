using System.Linq;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditStateCommand : Command
    {
        private string enterMessage = "Enter a new state";
        private string editedMessage = "The book has edited";
        private string errorMessage = "Error. There is no book with that title. Enter the title of an existing book.";

        public EditStateCommand()
        {
            Name = "/category";
            State = CommandState.EditBookMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (string.IsNullOrEmpty(context.Data) && string.IsNullOrEmpty(context.Parameters))
            {
                await BotClient.SendTextMessageAsync(context.Message.Chat, enterMessage);
            }
            else
            {
                string message = errorMessage;
                string stateString = context.Parameters ?? context.Data;

                if (int.TryParse(stateString, out int state) && context.SelectedBook != null)
                {
                    var values = BookState.GetValues<BookState>();
                    if (values.Any(v => (int)v == state))
                    {
                        context.SelectedBook.State = (BookState)state;
                        message = editedMessage;
                    }
                    else
                    {
                        message = "Incorrect value";
                    }
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