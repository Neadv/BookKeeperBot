using System.Text;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class ListBookshelfCommand : Command
    {
        public ListBookshelfCommand()
        {
            Name = "/list";
            State = CommandState.MainMenu;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            var stringBuilder = new StringBuilder();
            foreach (var bookshelf in context.Bookshelves)
            {
                stringBuilder.AppendLine($"{bookshelf.Name} - /select{bookshelf.Id}, /edit{bookshelf.Id}");
            } 
            await BotClient.SendTextMessageAsync(context.Message.Chat, stringBuilder.ToString());
        }
    }
}