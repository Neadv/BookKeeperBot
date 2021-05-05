namespace BookKeeperBot.Models.Commands
{
    public abstract class InputCommand<T> : Command where T: class
    {
        protected string EnterMessage = "Enter";
        protected string ExistMessage = "Exist";
        protected string NoExitstMessage = "NoExist";
        protected string Message;

        public InputCommand(string name, CommandState state)
            : base(name, state, true) { }

        protected bool InputData(CommandContext context, out T entity)
        {
            if (string.IsNullOrEmpty(context.Data) && string.IsNullOrEmpty(context.Parameters))
            {
                Message = EnterMessage;

                entity = default;
                return false;
            }
            string input = context.Parameters ?? context.Data;
            entity = GetItem(context, input);
            
            Message = entity != null ? ExistMessage : NoExitstMessage;

            return true;
        }

        protected abstract T GetItem(CommandContext context, string input);

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