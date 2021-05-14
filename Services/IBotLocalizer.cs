using System.Collections.Generic;
using BookKeeperBot.Models.Commands;

namespace BookKeeperBot.Services
{
    public interface IBotLocalizer
    {
        void Localize(CommandContext context);
        IEnumerable<string> GetAvailableCultures();
    }
}