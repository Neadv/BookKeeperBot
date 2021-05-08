namespace BookKeeperBot.Models.Commands
{
    public abstract class FindCommand<T> : Command
    {
        public FindCommand(string name, CommandState state)
            : base (name, state, true) { }

        protected T FindItem(CommandContext context)
        {
            T item = default;
            if (!string.IsNullOrEmpty(context.Parameters))
            {
                item = GetByName(context, context.Parameters);
            }
            else
            {
                string value = context.CommandName.Replace(Name, "");
                if (int.TryParse(value, out int id))
                {
                    item = GetById(context, id);
                }
            }
            return item;
        }

        protected abstract T GetById(CommandContext context, int id);
        protected abstract T GetByName(CommandContext context, string input);

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