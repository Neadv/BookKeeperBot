using System.Threading.Tasks;
using Telegram.Bot;

namespace BookKeeperBot.Models.Commands
{
    public abstract class Command
    {
        public string Name { get; set; } 
        public CommandState State { get; set; }
        public ITelegramBotClient BotClient { get; set; }

        public Command(string name, CommandState state)
        {
            Name = name;
            State = state;
        }

        public Command()
        {
            Name = string.Empty;
            State = CommandState.NoContext;
        }

        public abstract Task ExecuteAsync(CommandContext context);

        public virtual bool Check(CommandString command)
        {
            return command.State == State && command.CommandName == Name;
        }
    }

    public enum CommandState
    {
        NoContext,
        Bookshelf,
        Book
    }
}