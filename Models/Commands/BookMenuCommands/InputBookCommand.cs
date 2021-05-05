namespace BookKeeperBot.Models.Commands
{
    public abstract class InputBookCommand : InputCommand<Book>
    {
        protected InputBookCommand(string name) 
            : base(name, CommandState.BookMenu) { }

        protected override Book GetItem(CommandContext context, string input)
            => context.SelectedBookshelf.Books.Find(b => b.Title.ToLower() == input.ToLower());
    }
}