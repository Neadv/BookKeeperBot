using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookKeeperBot.Models;
using BookKeeperBot.Models.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Services
{
    public class ContextFactory : IContextFactory
    {
        private readonly IRepository<Models.User> userRepo;
        private readonly IRepository<Bookshelf> bookshelfRepo;

        public ContextFactory(IRepository<Models.User> userRepository, IRepository<Bookshelf> bookshelfRepository)
        {
            userRepo = userRepository;
            bookshelfRepo = bookshelfRepository;
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

                context.State = state;
                context.PreviousCommand = user.PreviousCommand;

                if (state == CommandState.MainMenu)
                {
                    context.Bookshelves = (await bookshelfRepo.GetAllAsync(bs => bs.UserId == user.Id)).ToList();
                    context.SelectedBookshelf = user.SelectedBookshelf;
                }
                else if (state == CommandState.BookMenu)
                {
                    await userRepo.LoadProperty(user, user => user.SelectedBook);
                    context.SelectedBook = user.SelectedBook;

                    context.SelectedBookshelf = await bookshelfRepo.GetWithIncludeAsync(b => b.Id == user.SelectedBookshelfId, b => b.Books);
                }
                else if (state == CommandState.EditBookMenu)
                {
                    await userRepo.LoadProperty(user, user => user.SelectedBook);
                    context.SelectedBook = user.SelectedBook;

                    await userRepo.LoadProperty(user, user => user.SelectedBookshelf);
                    context.SelectedBookshelf = user.SelectedBookshelf;
                }
            }
        }

        private CommandContext ParseUpdate(Update update)
        {
            CommandContext context = new CommandContext();

            context.IsCallback = update.Type == UpdateType.CallbackQuery;
            if (context.IsCallback)
            {
                context.Data = update.CallbackQuery.Data;
            }
            else if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                if (message.Text != null)
                {
                    Regex commandPattern = new Regex(@"^/\w+", RegexOptions.IgnoreCase);
                    var command = commandPattern.Match(message.Text).Value;

                    if (!string.IsNullOrEmpty(command))
                    {
                        context.CommandName = command.ToLower();
                        context.Parameters = message.Text.Replace(command, "").Trim();
                    }
                    else
                    {
                        context.Data = message.Text;
                    }
                }
                context.Message = message;
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