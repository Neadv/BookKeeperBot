using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public static class CommandKeyboards
    {
        private static IReplyMarkup mainMenuKeyboard;
        public static IReplyMarkup MainMenuKeyboad
        {
            get
            {
                return mainMenuKeyboard ??= new ReplyKeyboardMarkup{
                    OneTimeKeyboard = false,
                    ResizeKeyboard = true,
                    Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "/list" },
                        new KeyboardButton[] { "/add" },
                        new KeyboardButton[] { "/remove" },
                        new KeyboardButton[] { "/help", "/settings" },
                    }
                };
            }
        }

        private static IReplyMarkup bookMenuKeyboard;
        public static IReplyMarkup BookMenuKeyboard
        {
            get
            {
                return bookMenuKeyboard ??= new ReplyKeyboardMarkup{
                    OneTimeKeyboard = false,
                    ResizeKeyboard = true,
                    Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "/list" },
                        new KeyboardButton[] { "/planned", "/in_progress", "/completed" },
                        new KeyboardButton[] { "/add" },
                        new KeyboardButton[] { "/remove" },
                        new KeyboardButton[] { "/help", "/back" },
                    }
                };
            }
        }
    }
}