using System.Linq;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditStateCommand : InputStringCommand
    {
        private string errorMessage;

        public EditStateCommand() : base("/category")
        {
            ExistMessage = "The book has edited";
            EnterMessage = "Enter a new state";
            errorMessage = "Incorrect value. Enter a correct state";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (InputData(context, out string stateString))
            {
                if (int.TryParse(stateString, out int state) && context.SelectedBook != null)
                {
                    var values = BookState.GetValues<BookState>();
                    if (values.Any(v => (int)v == state))
                    {
                        context.SelectedBook.State = (BookState)state;
                        context.CommandName = null;
                        Message = ExistMessage;
                    }
                    else
                    {
                        Message = errorMessage;
                        context.CommandName = Name;
                    }
                }
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, Message);
        }
    }
}