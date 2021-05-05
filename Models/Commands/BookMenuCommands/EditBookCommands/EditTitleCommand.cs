using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditTitleCommand : InputStringCommand
    {
        public EditTitleCommand() : base("/title")
        {
            ExistMessage = "The book has edited";
            EnterMessage = "Enter a new description";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (InputData(context, out string title))
            {
                if (title != null && context.SelectedBook != null)
                {
                    context.SelectedBook.Title = title;
                    context.CommandName = null;
                }
                else
                {
                    context.CommandName = Name;
                }
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, Message);
        }
    }
}