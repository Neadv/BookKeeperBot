using System;
using System.Collections.Generic;
using BookKeeperBot.Models.Commands;
using Telegram.Bot;

namespace BookKeeperBot.Services
{
    public class CommandStorage : ICommandStorage
    {
        private ITelegramBotClient botClient;
        private List<Command> commands;

        public CommandStorage(IBotService bot)
        {
            botClient = bot.Client;
            commands = new List<Command>();
            ConfigureCommands();
        }

        private void ConfigureCommands()
        {
            // Initialization commands
            Register(new InitialCommand());         
            Register(new StartCommand());   

            // No-Context commands
            Register(new BackToMenuCommand());
            Register(new AboutCommand());
            Register(new ChangeStateCommand());
            
            // Main menu commands
            Register(new AddBookshelfCommand());
            Register(new RemoveBookshelfCommand());
            Register(new ListBookshelfCommand());
            Register(new SelectBookshelfCommand());
            Register(new EditBookshelfCommand());

            // Book menu commands
            Register(new ListBookCommand());
            Register(new SelectBookCommand());
            Register(new RemoveBookCommand());
            Register(new EditBookCommand());
            Register(new AddBookCommand());

            //Book edit menu commands
            Register(new EditDescriptionCommand());
            Register(new EditTitleCommand());
            Register(new EditNoteCommand());
            Register(new EditStateCommand());
        }

        public Command Find(CommandString commandString)
            => commands.Find(command => command.Check(commandString));

        public Command GetByName(string name)
            => commands.Find(command => command.Name == name);

        public void Register(Command newCommand)
        {
            newCommand.BotClient = botClient;
            commands.Add(newCommand);
        }
    }
}