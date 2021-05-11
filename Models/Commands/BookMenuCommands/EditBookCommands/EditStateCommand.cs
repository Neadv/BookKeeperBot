using System.Linq;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditStateCommand : Command
    {
        private string errorMessage;
        private string existMessage;

        public EditStateCommand() : base("/category", CommandState.EditBookMenu, true)
        {
            existMessage = "The book has edited";
            errorMessage = "Incorrect value. Enter a correct state";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = errorMessage;

            string input = context.Parameters;
            if (string.IsNullOrEmpty(input))
                input = context.CommandName.Replace(Name, string.Empty);

            if (int.TryParse(input, out int state))
            {
                var values = BookState.GetValues<BookState>();
                if (values.Any(v => (int)v == state))
                {
                    context.SelectedBook.State = (BookState)state;
                    context.CommandName = null;
                    message = existMessage;
                }
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