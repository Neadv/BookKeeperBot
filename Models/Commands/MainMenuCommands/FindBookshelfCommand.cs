namespace BookKeeperBot.Models.Commands
{
    public abstract class FindBookshelfCommand : FindCommand<Bookshelf>
    {
        public FindBookshelfCommand(string name) 
            : base(name, CommandState.MainMenu) { } 

        protected override Bookshelf GetById(CommandContext context, int id)
            => context.Bookshelves.Find(b => b.Id == id);

        protected override Bookshelf GetByName(CommandContext context, string input)
            => context.Bookshelves.Find(b => b.Name.ToLower() == input.ToLower());
    }
}