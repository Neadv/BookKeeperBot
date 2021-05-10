using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BookKeeperBot.Models.Commands
{
    public abstract class Command
    {
        public string Name { get; set; }
        public CommandState State { get; set; }
        public ITelegramBotClient BotClient { get; set; }

        protected bool Authorized { get; set; }
        protected string[] Alias { get; set; }

        public Command(string name, CommandState state, bool authorized)
        {
            Name = name;
            State = state;
            Authorized = authorized;
            Alias = Array.Empty<string>();
        }

        public Command() 
            : this(string.Empty, CommandState.NoContext, true) { }

        public abstract Task ExecuteAsync(CommandContext context);

        public virtual bool Check(CommandString command)
        {
            if (Authorized && !command.IsAuthorized)
                return false;

            if (State != CommandState.NoContext && State != command.State)
                return false;  

            if (Alias?.Length > 0 && Alias.Any(a => a == command.CommandName))
                return true;

            if (Name == command.CommandName)
                return true;

            return false;
        }
    }

    public enum CommandState
    {
        Initial,
        MainMenu,
        BookMenu,
        EditBookMenu,
        NoContext
    }
}