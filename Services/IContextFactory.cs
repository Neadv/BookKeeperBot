using System.Threading.Tasks;
using BookKeeperBot.Models.Commands;
using Telegram.Bot.Types;

namespace BookKeeperBot.Services
{
    public interface IContextFactory
    {
        Task<CommandContext> CreateContextAsync(Update update);
    }
}