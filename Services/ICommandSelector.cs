using System.Threading.Tasks;
using BookKeeperBot.Models.Commands;
using Telegram.Bot.Types;

namespace BookKeeperBot.Services
{
    public interface ICommandSelector
    {
        Task SelectAsync(Update update);
        void AddCommand(Command command);
    }
}