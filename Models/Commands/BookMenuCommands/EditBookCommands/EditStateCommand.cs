using System.Linq;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditStateCommand : Command
    {
        public EditStateCommand() : base("/category", CommandState.EditBookMenu, true)
        { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string existMessage = Localizer["BookEditSuccess"];
            string errorMessage = Localizer["BookEditCategoryError"];

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