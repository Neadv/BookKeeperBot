using System.Threading.Tasks;
using BookKeeperBot.Models.Commands;
using Telegram.Bot.Types;

namespace BookKeeperBot.Services
{
    public interface ICommandSelector
    {
        Task SelectAsync(CommandContext context);
        void AddCommand(Command command);
    }
}