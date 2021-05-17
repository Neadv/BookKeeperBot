using System.Collections.Generic;
using System.Globalization;
using BookKeeperBot.Models;
using BookKeeperBot.Models.Commands;

namespace BookKeeperBot.Services
{
    public class BotLocalizer : IBotLocalizer
    {
        private List<string> availableCultures = new List<string>
        {
            "en",
            "ru"
        };

        public IEnumerable<string> GetAvailableCultures()
            => availableCultures;

        public void Localize(CommandContext context)
        {
            string languageCode = availableCultures[0];

            var user = context.User;
            if (user != null && availableCultures.Contains(user.Language))
            {
                languageCode = user.Language;
            }
            else if (user == null && context.Message != null)
            {
                var code = context.Message.From.LanguageCode;
                if (availableCultures.Contains(code))
                    languageCode = code;
            }

            CultureInfo.CurrentCulture = new CultureInfo(languageCode);
            CultureInfo.CurrentUICulture = new CultureInfo(languageCode);
        }
    }
}