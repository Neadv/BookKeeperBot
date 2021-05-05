namespace BookKeeperBot.Models.Commands
{
    public abstract class InputStringCommand : InputCommand<string>
    {
        protected InputStringCommand(string name) 
            : base(name, CommandState.EditBookMenu) { }

        protected override string GetItem(CommandContext context, string input) => input;
    }
}