using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditTitleCommand : Command
    {
         private string enterMessage = "Enter a new title";
        private string editedMessage = "The book has edited";
        private string errorMessage = "Error. There is no book with that title. Enter the title of an existing book.";

        public EditTitleCommand()
        {
            Name = "/title";
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
                string title = context.Parameters ?? context.Data;
                if (title != null && context.SelectedBook != null)
                {
                    message = editedMessage;
                    context.SelectedBook.Title = title;
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