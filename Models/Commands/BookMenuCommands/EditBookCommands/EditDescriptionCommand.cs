using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditDescriptionCommand : InputStringCommand
    {
        public EditDescriptionCommand() 
            : base("/description") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            ExistMessage = Localizer["BookEditSuccess"];
            EnterMessage = Localizer["BookEditDescEnter"];

            if (InputData(context, out string description))
            {
                if (description != null && context.SelectedBook != null)
                {
                    context.SelectedBook.Description = description;
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