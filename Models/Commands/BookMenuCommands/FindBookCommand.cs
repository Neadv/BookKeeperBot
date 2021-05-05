namespace BookKeeperBot.Models.Commands
{
    public abstract class FindBookCommands : Command
    {
        
        protected Book FindBook(CommandContext context)
        {
            Book book = context.SelectedBook;
            if (book == null)
            {
                if (!string.IsNullOrEmpty(context.Parameters))
                {
                    book = context.SelectedBookshelf.Books.Find(b => b.Title.ToLower() == context.Parameters.ToLower());
                }
                else
                {
                    string value = context.CommandName.Replace(Name, "");
                    if (int.TryParse(value, out int id))
                    {
                        book = context.SelectedBookshelf.Books.Find(b => b.Id == id);
                    }
                }
            }
            return book;
        }

        public override bool Check(CommandString command)
        {
            if (base.Check(command))
                return true;

            if (!command.IsAuthorized)
                return false;

            if (command.State != State)
                return false;

            return command.CommandName != null && command.CommandName.StartsWith(Name);
        }
    }
}