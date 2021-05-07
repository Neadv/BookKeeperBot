using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookshelfCommand : InputBookshelfCommand
    {
        public RemoveBookshelfCommand() : base ("/remove")
        {
            EnterMessage = "Enter the name of the bookshelf";
            ExistMessage = "The bookshelf has removed";
            NoExitstMessage = "There is no bookshelf with that name.\nEnter the name of an existing bookshelf";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            Message = ExistMessage;
            IReplyMarkup keyboard = new ReplyKeyboardRemove();

            var bookshelf = context.SelectedBookshelf;
            if (bookshelf != null || InputData(context, out bookshelf))
            {
                if (bookshelf != null)
                {
                    context.RemoveBookshelf(bookshelf);
                    context.CommandName = null;
                                        
                    keyboard = CommandKeyboards.MainMenuKeyboad;
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