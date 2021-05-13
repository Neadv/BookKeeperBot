using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookshelfCommand : InputBookshelfCommand
    {
        public RemoveBookshelfCommand() 
            : base ("/remove") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            EnterMessage = Localizer["RemoveBookshelfEnter"];
            NoExitstMessage = Localizer["RemoveBookshelfError"];
            ExistMessage = Localizer["RemoveBookshelfSuccess"];

            Message = ExistMessage;
            IReplyMarkup keyboard = new ReplyKeyboardRemove();

            var bookshelf = context.SelectedBookshelf;
            if (bookshelf != null || InputData(context, out bookshelf))
            {
                if (bookshelf != null)
                {
                    context.RemoveBookshelf(bookshelf);
                    context.CommandName = null;
                                        
                    keyboard = CommandKeyboards.GetMainMenu(Localizer);
                }
                else
                {
                    context.CommandName = Name;
                }
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, Message, replyMarkup: keyboard);
        }
    }
}