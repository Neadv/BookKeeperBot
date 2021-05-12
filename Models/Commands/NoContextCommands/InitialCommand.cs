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
            context.AddBookshelf(new Bookshelf { Name = "Books" });
            await BotClient.SendTextMessageAsync(context.Message.Chat, "I added a new bookshelf for you, it's empty for now, so fill it up ");
            
            context.RedirectToCommand("/back");
        }
    }
}