using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class InitialCommand : Command
    {
        public InitialCommand()
        {
            Name = "/init";
            State = CommandState.Initial;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            context.AddBookshelf(new Bookshelf { Name = Localizer["InitBookshelfName"] });
            await BotClient.SendTextMessageAsync(context.Message.Chat, Localizer["Init"]);
            
            context.RedirectToCommand("/back");
        }
    }
}