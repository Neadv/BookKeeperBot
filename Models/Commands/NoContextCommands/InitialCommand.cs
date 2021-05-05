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
            await BotClient.SendTextMessageAsync(context.Message.Chat, "New bookshelf has been added");
            
            context.ChangeState(CommandState.MainMenu);
            await BotClient.SendTextMessageAsync(context.Message.Chat, "Main menu");
        }
    }
}