namespace BookKeeperBot.Models.Commands
{
    public abstract class InputBookCommand : Command
    {
        protected string EnterMessage = "Enter";
        protected string ExistMessage = "Exist";
        protected string NoExitstMessage = "NoExist";
        protected string Message;

        protected bool InputBook(CommandContext context, out Book book)
        {
            if (string.IsNullOrEmpty(context.Data) && string.IsNullOrEmpty(context.Parameters))
            {
                Message = EnterMessage;

                book = null;
                return false;
            }
            string title = context.Parameters ?? context.Data;
            book = context.SelectedBookshelf.Books.Find(b => b.Title.ToLower() == title.ToLower());
            
            Message = book != null ? ExistMessage : NoExitstMessage;

            return true;
        }

        public override bool Check(CommandString command)
        {
            if (base.Check(command))
                return true;

            if (command.State != State)
                return false;

            if (command.CommandName != null)
                return false;

            return command.PreviosCommand == Name && command.ContainData;
        }
    }
}