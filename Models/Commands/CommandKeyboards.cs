using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public static class CommandKeyboards
    {
        public static IReplyMarkup GetBookMenu(IStringLocalizer localizer)
        {
            return new ReplyKeyboardMarkup
            {
                OneTimeKeyboard = false,
                ResizeKeyboard = true,
                Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[] { localizer["KeyboardBooks"].Value },
                        new KeyboardButton[] { localizer["KeyboardPlanned"].Value, localizer["KeyboardInProgress"].Value, localizer["KeyboardCompleted"].Value },
                        new KeyboardButton[] { localizer["KeyboardAddBook"].Value, localizer["KeyboardSearch"].Value },
                        new KeyboardButton[] { localizer["KeyboardRemoveBook"].Value },
                        new KeyboardButton[] { localizer["KeyboardHelp"].Value, localizer["KeyboardBack"].Value },
                    }
            };
        }

        public static IReplyMarkup GetMainMenu(IStringLocalizer localizer)
        {
            return new ReplyKeyboardMarkup
            {
                OneTimeKeyboard = false,
                ResizeKeyboard = true,
                Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[] { localizer["KeyboardBookshelves"].Value },
                        new KeyboardButton[] { localizer["KeyboardAddBookshelf"].Value },
                        new KeyboardButton[] { localizer["KeyboardRemoveBookshelf"].Value },
                        new KeyboardButton[] { localizer["KeyboardHelp"].Value, localizer["KeyboardSettings"].Value },
                    }
            };
        }

        public static string SubstitutedCommand(IStringLocalizer localizer, string command)
        {
            if (command == localizer["KeyboardBookshelves"] || command == localizer["KeyboardBooks"])
                return "/list";
            if (command == localizer["KeyboardAddBookshelf"] || command == localizer["KeyboardAddBook"])
                return "/add";
            if (command == localizer["KeyboardRemoveBookshelf"] || command == localizer["KeyboardRemoveBook"])
                return "/remove";
            if (command == localizer["KeyboardHelp"])
                return "/help";
            if (command == localizer["KeyboardSettings"])
                return "/settings";
            if (command == localizer["KeyboardSearch"])
                return "/add_search";
            if (command == localizer["KeyboardPlanned"])
                return "/planned";
            if (command == localizer["KeyboardInProgress"])
                return "/in_progress";
            if (command == localizer["KeyboardCompleted"])
                return "/completed";
            if (command == localizer["KeyboardBack"])
                return "/back";

            return null;
        }
    }
}