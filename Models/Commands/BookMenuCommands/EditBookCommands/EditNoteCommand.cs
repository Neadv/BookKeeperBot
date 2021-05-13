using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditNoteCommand : InputStringCommand
    {
        public EditNoteCommand() 
            : base("/note") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            ExistMessage = Localizer["BookEditSuccess"];
            EnterMessage = Localizer["BookEditNoteEnter"];

            if (InputData(context, out string note))
            {
                if (note != null && context.SelectedBook != null)
                {
                    context.SelectedBook.Note = note;
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