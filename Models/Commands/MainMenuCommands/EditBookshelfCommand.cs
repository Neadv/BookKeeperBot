using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class EditBookshelfCommand : FindBookshelfCommand
    {
        private string enterMessage = "Enter a new name for the selected bookshelf.";
        private string editedMessage = "The bookshelf has edited.";
        private string errorMessage = "Error. There is no bookshelf with this name or id.";
        private string errorNameMessage = "Error. There is bookshelf with this name.";

        public EditBookshelfCommand() : base("/edit") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = enterMessage;
            IReplyMarkup keyboard = CommandKeyboards.MainMenuKeyboad;
            if (context.SelectedBookshelf == null || CheckParameters(context))
            {
                context.SelectedBookshelf = FindItem(context);
                if (context.SelectedBookshelf == null)
                {
                    message = errorMessage;
                }
            }
            else if (context.Data != null && context.SelectedBookshelf != null)
            {
                if (!context.Bookshelves.Any(b => b.Name == context.Data))
                {
                    context.SelectedBookshelf.Name = context.Data;
                    context.SelectedBookshelf = null;

                    message = editedMessage;
                }
                else
                {
                    message = errorNameMessage;
                }
            }
            
            if(message == enterMessage)
                keyboard = new ReplyKeyboardRemove();

            await BotClient.SendTextMessageAsync(context.Message.Chat, message, replyMarkup: keyboard);
        }

        private bool CheckParameters(CommandContext context)
            => context.CommandName != null && (string.IsNullOrEmpty(context.Parameters) || context.CommandName.Length > Name.Length);

        public override bool Check(CommandString command)
            => base.Check(command) || (command.PreviosCommand != null && command.PreviosCommand.StartsWith(Name) && command.ContainData);
    }
}