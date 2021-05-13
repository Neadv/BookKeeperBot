using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class EditBookshelfCommand : FindBookshelfCommand
    {
        public EditBookshelfCommand() : base("/edit") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = Localizer["Enter a new name for the selected bookshelf:"];
            IReplyMarkup keyboard = CommandKeyboards.GetMainMenu(Localizer);
            if (context.SelectedBookshelf == null || CheckParameters(context))
            {
                context.SelectedBookshelf = FindItem(context);
                if (context.SelectedBookshelf == null)
                {
                    message = Localizer["EditBookshelfNoExist"];
                }
            }
            else if (context.Data != null && context.SelectedBookshelf != null)
            {
                if (!context.Bookshelves.Any(b => b.Name == context.Data))
                {
                    context.SelectedBookshelf.Name = context.Data;
                    context.SelectedBookshelf = null;

                    message = Localizer["EditBookshelfSuccess"];
                }
                else
                {
                    message = Localizer["EditBookshelfErrorName"];
                }
            }
            
            if(message == Localizer["Enter a new name for the selected bookshelf:"])
                keyboard = new ReplyKeyboardRemove();

            await BotClient.SendTextMessageAsync(context.Message.Chat, message, replyMarkup: keyboard);
        }

        private bool CheckParameters(CommandContext context)
            => context.CommandName != null && (string.IsNullOrEmpty(context.Parameters) || context.CommandName.Length > Name.Length);

        public override bool Check(CommandString command)
            => base.Check(command) || (command.PreviosCommand != null && command.PreviosCommand.StartsWith(Name) && command.ContainData);
    }
}