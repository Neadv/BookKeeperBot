using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class SettingsCommand : Command
    {
        public SettingsCommand() 
            : base("/settings", CommandState.MainMenu, true) { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            await BotClient.SendTextMessageAsync(context.Message.Chat, Localizer["SettingsMessage"], replyMarkup: new ReplyKeyboardRemove());
            context.ChangeState(CommandState.Settings);
            context.RedirectToCommand("/help");
        }
    }
}