using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public abstract class InputBookshelfCommand : Command
    {
        protected string EnterMessage = "Enter";
        protected string ExistMessage = "Exist";
        protected string NoExitstMessage = "NoExist";
        protected string Message;

        protected bool InputBookshelf(CommandContext context, out Bookshelf bookshelf)
        {
            if (string.IsNullOrEmpty(context.Data) && string.IsNullOrEmpty(context.Parameters))
            {
                Message = EnterMessage;

                bookshelf = null;
                return false;
            }
            string name = context.Parameters ?? context.Data;
            bookshelf = context.Bookshelves.Find(b => b.Name.ToLower() == name.ToLower());
            
            Message = bookshelf != null ? ExistMessage : NoExitstMessage;

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