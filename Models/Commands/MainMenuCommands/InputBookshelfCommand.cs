using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public abstract class InputBookshelfCommand : InputCommand<Bookshelf>
    {
        public InputBookshelfCommand(string name) 
            : base(name, CommandState.MainMenu) { } 

        protected override Bookshelf GetItem(CommandContext context, string input)
            => context.Bookshelves.Find(b => b.Name.ToLower() == input.ToLower());
    }
}