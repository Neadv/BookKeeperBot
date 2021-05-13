using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class BackToMenuCommand : Command
    {
        public BackToMenuCommand()
        {
            Name = "/back";
            State = CommandState.NoContext;
            Alias = new[] { "/menu", "/return" };
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (context.IsCallback && context.Message != null)
                await BotClient.EditMessageReplyMarkupAsync(context.Message.Chat, context.Message.MessageId, InlineKeyboardMarkup.Empty());

            CommandState newState = CommandState.MainMenu;
            string message = Localizer["BackMainMenu"];
            var keyboard = CommandKeyboards.GetMainMenu(Localizer);

            if (context.State == CommandState.EditBookMenu)
            {
                context.SelectedBook = null;
                newState = CommandState.BookMenu;
                message = Localizer["BackBookMenu"];
                keyboard = CommandKeyboards.GetBookMenu(Localizer);
            }
            else
            {
                context.SelectedBook = null;
                context.SelectedBookshelf = null;
            }

            context.ChangeState(newState);
            await BotClient.SendTextMessageAsync(context.Message.Chat, message, replyMarkup: keyboard);
            context.RedirectToCommand("/help");
        }
    }
}