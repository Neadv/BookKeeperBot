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
        private readonly ICommandStorage commands;
        private Models.User user = null;

        public CommandSelector(IRepository<Models.User> userRepository, ICommandStorage commandStorage)
        {
            userRepo = userRepository;
            commands = commandStorage;
        }

        public async Task SelectAsync(CommandContext context)
        {
            var command = FindCommand(context);
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

        private Command FindCommand(CommandContext context)
        {
            Command command = null;
            if (context != null)
            {
                command = commands.Find(context.GetCommandString());
            }
            return command;
        }
    }
}