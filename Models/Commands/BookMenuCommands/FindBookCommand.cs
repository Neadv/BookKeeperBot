namespace BookKeeperBot.Models.Commands
{
    public abstract class FindBookCommand : FindCommand<Book>
    {
        protected FindBookCommand(string name) 
            : base(name, CommandState.BookMenu) { }

        protected override Book GetById(CommandContext context, int id)
            => context.SelectedBookshelf.Books.Find(b => b.Id == id);

        protected override Book GetByName(CommandContext context, string input)
            => context.SelectedBookshelf.Books.Find(b => b.Title.ToLower() == input.ToLower());
    }
}