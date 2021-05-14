using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookKeeperBot.Models;
using BookKeeperBot.Models.Commands;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookKeeperBot.Services
{
    public class CommandSelector : ICommandSelector
    {
        private readonly IRepository<Models.User> userRepo;
        private readonly ICommandStorage commands;
        private readonly IStringLocalizer localizer;
        private Models.User user = null;

        public CommandSelector(IRepository<Models.User> userRepository, ICommandStorage commandStorage, IStringLocalizer<Command> stringLocalizer)
        {
            userRepo = userRepository;
            commands = commandStorage;
            localizer = stringLocalizer;
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

            var redirectContext = context.GetRedirectContext();
            if (redirectContext != null)
            {
                await SelectAsync(redirectContext);
            }
        }

        private Command FindCommand(CommandContext context)
        {
            SubstituteCommand(context);

            Command command = null;
            if (context != null)
            {
                command = commands.Find(context.GetCommandString());
            }
            return command;
        }

        private void SubstituteCommand(CommandContext context)
        {
            if (!string.IsNullOrEmpty(context.Data))
            {
                var substitutedCommand = CommandKeyboards.SubstitutedCommand(localizer, context.Data);
                if (substitutedCommand != null)
                {
                    context.CommandName = substitutedCommand;
                    context.Data = null;
                }
            }
        }
    }
}