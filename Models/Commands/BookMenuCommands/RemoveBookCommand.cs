using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookCommand : InputBookCommand
    {
        public RemoveBookCommand() 
            : base("/remove") { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            EnterMessage = Localizer["RemoveBookEnter"];
            NoExitstMessage = Localizer["RemoveBookError"];
            ExistMessage = Localizer["RemoveBookSuccess"];

            Message = ExistMessage;
            IReplyMarkup keyboard = new ReplyKeyboardRemove();
            if (InputData(context, out Book book))
            {
                if (book != null)
                {
                    context.RemoveBook(book);
                    context.CommandName = null;

                    keyboard = CommandKeyboards.GetBookMenu(Localizer);
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