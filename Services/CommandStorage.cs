using System.Collections.Generic;
using BookKeeperBot.Models.Commands;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookKeeperBot.Services
{
    public class CommandStorage : ICommandStorage
    {
        private readonly ITelegramBotClient botClient;
        private readonly IStringLocalizer<Command> localizer;

        private List<Command> commands;

        public CommandStorage(IBotService bot, IStringLocalizer<Command> stringLocalizer)
        {
            botClient = bot.Client;
            localizer = stringLocalizer;
            commands = new List<Command>();
            RegisterCommand();
            ConfigureCommands();
        }

        private void RegisterCommand()
        {
            botClient.SetMyCommandsAsync(
                new BotCommand[]
                {
                    new BotCommand { Command = "help", Description = "Information about what I can do and command list" },
                    new BotCommand { Command = "back", Description = "Return to previous menu" },
                    new BotCommand { Command = "settings", Description = "Bot settings" },
                }
            ).GetAwaiter().GetResult();
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
            Register(new EditImageCommand());
        }

        public Command Find(CommandString commandString)
            => commands.Find(command => command.Check(commandString));

        public Command GetByName(string name)
            => commands.Find(command => command.Name == name);

        public void Register(Command newCommand)
        {
            newCommand.BotClient = botClient;
            newCommand.Localizer = localizer;
            commands.Add(newCommand);
        }
    }
}