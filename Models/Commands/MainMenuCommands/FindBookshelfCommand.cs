namespace BookKeeperBot.Models.Commands
{
    public abstract class FindBookshelfCommand : Command
    {

        protected Bookshelf FindBookshelf(CommandContext context)
        {
            Bookshelf bookshelf = null;
            if (!string.IsNullOrEmpty(context.Parameters))
            {
                bookshelf = context.Bookshelves.Find(b => b.Name.ToLower() == context.Parameters.ToLower());
            }
            else
            {
                string value = context.CommandName.Replace(Name, "");
                if (int.TryParse(value, out int id))
                {
                    bookshelf = context.Bookshelves.Find(b => b.Id == id);
                }
            }
            return bookshelf;
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