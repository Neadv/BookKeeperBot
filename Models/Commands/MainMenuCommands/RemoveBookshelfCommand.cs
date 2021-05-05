using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class RemoveBookshelfCommand : InputBookshelfCommand
    {
        public RemoveBookshelfCommand()
        {
            Name = "/remove";
            State = CommandState.MainMenu;

            EnterMessage = "Enter the name of the bookshelf";
            ExistMessage = "The bookshelf has removed";
            NoExitstMessage = "There is no bookshelf with that name.\nEnter the name of an existing bookshelf";
            Message = ExistMessage;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            var bookshelf = context.SelectedBookshelf;
            if (bookshelf != null || InputBookshelf(context, out bookshelf))
            {
                if (bookshelf != null)
                {
                    context.RemoveBookshelf(bookshelf);
                    context.CommandName = null;
                }
                else
                {
                    context.CommandName = Name;
                }
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, Message);
        }
    }
}