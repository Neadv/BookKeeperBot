using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Models.Commands
{
    public class AboutCommand : Command
    {
        private string helpMessage = "{0}\n<em>{1}</em>\n{2}";

        public AboutCommand()
        {
            Name = "/about";
            Alias = new string[] { "/help", "/commands" };
            State = CommandState.NoContext;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = context.State switch
            {
                CommandState.MainMenu => string.Format(
                    helpMessage, 
                    Localizer["HelpMainMenu"], 
                    Localizer["HelpMainMenuCommands"], 
                    string.Format(Localizer["HelpMainCommands"], "\n")
                    ),
                CommandState.BookMenu => string.Format(
                    helpMessage, 
                    Localizer["HelpBookMenu"],
                    Localizer["HelpBookMenuCommands"],
                    string.Format(Localizer["HelpBookCommands"], "\n")
                    ),
                CommandState.EditBookMenu => string.Format(
                    helpMessage,
                    Localizer["HelpEditBookMenu"],
                    Localizer["HelpEditCommands"],
                    string.Format(Localizer["HelpEditBookCommands"], "\n")
                    ),
                CommandState.Settings => string.Format(
                    helpMessage,
                    Localizer["HelpSettings"],
                    Localizer["HelpSettingsCommandList"],
                    string.Format(Localizer["HelpSettingsCommands"], "\n")
                ),
                _ => null
            };

            if (message != null)
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html);
        }
    }
}