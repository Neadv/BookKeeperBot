using Telegram.Bot;

namespace BookKeeperBot.Models.Commands
{
    public abstract class Command
    {
        public string Name { get; set; } 
        public CommandState State { get; set; } = CommandState.NoContext;
        public ITelegramBotClient BotClient { get; set; }

        public abstract void Execute(CommandContext context);

        public virtual bool Check(CommandString command)
        {
            return command.State == State && command.CommandName == Name;
        }
    }

    public enum CommandState
    {
        Bookshelf,
        Book,
        NoContext
    }
}