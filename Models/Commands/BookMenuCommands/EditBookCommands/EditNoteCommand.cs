using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditNoteCommand : InputStringCommand
    {
        public EditNoteCommand() : base("/note")
        {  
            ExistMessage = "The book has edited";
            EnterMessage = "Enter a new note";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
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