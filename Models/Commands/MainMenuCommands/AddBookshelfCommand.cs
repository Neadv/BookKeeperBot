using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookshelfCommand : InputBookshelfCommand
    {
        public AddBookshelfCommand() 
            : base("/add") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            EnterMessage = Localizer["AddBookshelfEnter"];
            NoExitstMessage = Localizer["AddBookshelfSuccess"];
            ExistMessage = Localizer["AddBookshelfError"];

            IReplyMarkup keyboard = new ReplyKeyboardRemove();
            if (InputData(context, out Bookshelf bookshelf))
            {
                if (bookshelf == null)
                {
                    bookshelf = new Bookshelf { Name = context.Parameters ?? context.Data, User = context.User };
                    context.AddBookshelf(bookshelf);
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