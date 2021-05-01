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
            context.ChangeState(CommandState.MainMenu);
            await BotClient.SendTextMessageAsync(context.Message.Chat, "Main menu");
        }
    }
}