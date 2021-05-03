using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookKeeperBot.Models;
using BookKeeperBot.Models.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookKeeperBot.Services
{
    public class CommandSelector : ICommandSelector
    {
        private readonly IRepository<Models.User> userRepo;
        private readonly IContextFactory factory;
        private readonly ITelegramBotClient botClient;
        
        private List<Command> commands = new List<Command>();
        private CommandContext context = null;
        private Models.User user = null;

        public CommandSelector(IRepository<Models.User> userRepository, IContextFactory contextFactory, IBotService botService)
        {
            userRepo = userRepository;
            factory = contextFactory;
            botClient = botService.Client;
        }

        public async Task SelectAsync(Update update)
        {
            context = await factory.CreateContextAsync(update);
            var command = FindCommand();
            user = context.User;

            if (command != null)
            {
                user = context.User;

                await command.ExecuteAsync(context);

                await UpdateContext(context, command);
            }
        }

        private async Task UpdateContext(CommandContext context, Command command)
        {
            if (context.User != null)
            {
                context.User.PreviousCommand = context.CommandName;
                
                if (user == null)
                {
                    await userRepo.AddAsync(context.User);
                }
                else
                {
                    await userRepo.UpdateAsync(context.User);
                }
            }
            else if (user != null)
            {
                await userRepo.DeleteAsync(user);
            }
        }

        public void AddCommand(Command command)
        {
            command.BotClient = botClient;
            commands.Add(command);
        }

        private Command FindCommand()
        {
            Command command = null;
            if (context != null)
            {
                var commandString = context.GetCommandString();
                command = commands.FirstOrDefault(command => command.Check(commandString));
            }
            return command;
        }
    }
}