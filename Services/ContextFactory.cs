using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookKeeperBot.Models;
using BookKeeperBot.Models.Commands;
using Microsoft.Extensions.Localization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Services
{
    public class ContextFactory : IContextFactory
    {
        private readonly IRepository<Models.User> userRepo;
        private readonly IRepository<Bookshelf> bookshelfRepo;
        private readonly IStringLocalizer<Command> localizer;

        public ContextFactory(IRepository<Models.User> userRepository, IRepository<Bookshelf> bookshelfRepository, IStringLocalizer<Command> stringLocalizer)
        {
            userRepo = userRepository;
            bookshelfRepo = bookshelfRepository;
            localizer = stringLocalizer;
        }

        public async Task<CommandContext> CreateContextAsync(Update update)
        {
            CommandContext context = ParseUpdate(update);

            context.User = await GetUser(update);
            await LoadData(context);

            return context;
        }

        private async Task LoadData(CommandContext context)
        {
            var user = context.User;
            if (user != null)
            {
                var state = user.State;

                if (state == CommandState.MainMenu)
                {
                    await userRepo.LoadCollection(user, user => user.Bookshelves);
                }
                else if (state == CommandState.BookMenu)
                {
                    await userRepo.LoadProperty(user, user => user.SelectedBook);
                    await userRepo.LoadProperty(user, user => user.SelectedBookshelf);

                    if (user.SelectedBookshelf != null)
                        await bookshelfRepo.LoadCollection(user.SelectedBookshelf, b => b.Books);
                }
                else if (state == CommandState.EditBookMenu)
                {
                    await userRepo.LoadProperty(user, user => user.SelectedBook);
                    await userRepo.LoadProperty(user, user => user.SelectedBookshelf);
                }
            }
        }

        private CommandContext ParseUpdate(Update update)
        {
            CommandContext context = new CommandContext();

            Message message = update.Message;
            string text = message?.Text;

            context.IsCallback = update.Type == UpdateType.CallbackQuery;

            if (context.IsCallback)
            {
                message = update.CallbackQuery.Message;
                text = update.CallbackQuery.Data;
            }

            if (text != null)
            {
                Regex commandPattern = new Regex(@"^/\w+", RegexOptions.IgnoreCase);
                var command = commandPattern.Match(text).Value;

                if (!string.IsNullOrEmpty(command))
                {
                    context.CommandName = command.ToLower();
                    context.Parameters = text.Replace(command, "").Trim();
                }
                else
                {
                    context.Data = text;
                }
            }
            context.Message = message;

            if (!string.IsNullOrEmpty(context.Data))
            {
                var substitutedCommand = CommandKeyboards.SubstitutedCommand(localizer, context.Data);
                if (substitutedCommand != null)
                {
                    context.CommandName = substitutedCommand;
                    context.Data = null;
                }
            }

            return context;
        }

        private async Task<Models.User> GetUser(Update update)
        {
            Telegram.Bot.Types.User telegramUser = update.Type switch
            {
                UpdateType.Message => update.Message.From,
                UpdateType.CallbackQuery => update.CallbackQuery.From,
                _ => null
            };

            if (telegramUser == null)
                return null;

            return await userRepo.GetByIdAsync(telegramUser.Id);
        }
    }
}