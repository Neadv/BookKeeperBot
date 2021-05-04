using BookKeeperBot.Models.Commands;

namespace BookKeeperBot.Services
{
    public static class ConfigureCommandsExtension
    {
        public static ICommandSelector ConfigureCommands(this ICommandSelector commandSelector, params Command[] commands)
        {
            // Initialization commands
            commandSelector.AddCommand(new InitialCommand());         
            commandSelector.AddCommand(new StartCommand());   

            // No-Context commands
            commandSelector.AddCommand(new BackToMenuCommand());
            commandSelector.AddCommand(new AboutCommand());
            
            // Main menu commands
            commandSelector.AddCommand(new AddBookshelfCommand());
            commandSelector.AddCommand(new RemoveBookshelfCommand());
            commandSelector.AddCommand(new ListBookshelfCommand());
            commandSelector.AddCommand(new SelectBookshelfCommand());
            commandSelector.AddCommand(new EditBookshelfCommand());

            // Book menu commands
            commandSelector.AddCommand(new ListBookCommand());
            commandSelector.AddCommand(new SelectBookCommand());
            commandSelector.AddCommand(new RemoveBookCommand());
            commandSelector.AddCommand(new EditBookCommand());
            commandSelector.AddCommand(new AddBookCommand());

            // Parameters commands
            foreach (var command in commands)
            {
                commandSelector.AddCommand(command);
            }

            return commandSelector;
        }
    }
}