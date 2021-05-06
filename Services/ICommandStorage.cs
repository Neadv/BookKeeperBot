using BookKeeperBot.Models.Commands;

namespace BookKeeperBot.Services
{
    public interface ICommandStorage
    {
        Command GetByName(string name);
        Command Find(CommandString commandString);
        void Register(Command newCommand);
    }
}