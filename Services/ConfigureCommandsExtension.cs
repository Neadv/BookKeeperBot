using BookKeeperBot.Models.Commands;

namespace BookKeeperBot.Services
{
    public static class ConfigureCommandsExtension
    {
        public static ICommandSelector ConfigureCommands(this ICommandSelector commandSelector, params Command[] commands)
        {
            // Add new commands
            commandSelector.AddCommand(new StartCommand());   
            commandSelector.AddCommand(new BackToMenuCommand());
            commandSelector.AddCommand(new InitialCommand());         
            commandSelector.AddCommand(new AboutCommand());

            foreach (var command in commands)
            {
                commandSelector.AddCommand(command);
            }

            return commandSelector;
        }
    }
}