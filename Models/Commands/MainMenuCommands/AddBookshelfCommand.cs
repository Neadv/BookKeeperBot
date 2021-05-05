using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class AddBookshelfCommand : InputBookshelfCommand
    {
        public AddBookshelfCommand()
        {
            Name = "/add";
            State = CommandState.MainMenu;

            EnterMessage = "Enter a name for the new bookshelf";
            NoExitstMessage = "The bookshelf has added";
            ExistMessage = "There is bookshelf with that name.\nEnter a unique name";
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (InputBookshelf(context, out Bookshelf bookshelf))
            {
                if (bookshelf == null)
                {
                    bookshelf = new Bookshelf { Name = context.Parameters ?? context.Data, User = context.User };
                    context.AddBookshelf(bookshelf);
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