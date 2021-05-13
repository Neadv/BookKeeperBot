using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public static class CommandKeyboards
    {
        public static Dictionary<string, string> ButtonCommands { get; } = new Dictionary<string, string>
        {
            { "Bookshelves", "/list" },
            { "Books", "/list" },
            { "Add new bookshelf", "/add" },
            { "Add new book", "/add" },
            { "Remove existing bookshelf", "/remove" },
            { "Remove existing book", "/remove" },
            { "Help", "/help" },
            { "Back", "/back" },
            { "Search for a book", "/add_search" },
            { "Planned", "/planned" },
            { "In progress", "/in_progress" },
            { "Completed", "/completed" },
        };

        private static IReplyMarkup mainMenuKeyboard;
        public static IReplyMarkup MainMenuKeyboad
        {
            get
            {
                return mainMenuKeyboard ??= new ReplyKeyboardMarkup
                {
                    OneTimeKeyboard = false,
                    ResizeKeyboard = true,
                    Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "Bookshelves" },
                        new KeyboardButton[] { "Add new bookshelf" },
                        new KeyboardButton[] { "Remove existing bookshelf" },
                        new KeyboardButton[] { "Help", "Settings" },
                    }
                };
            }
        }

        private static IReplyMarkup bookMenuKeyboard;
        public static IReplyMarkup BookMenuKeyboard
        {
            get
            {
                return bookMenuKeyboard ??= new ReplyKeyboardMarkup
                {
                    OneTimeKeyboard = false,
                    ResizeKeyboard = true,
                    Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "Books" },
                        new KeyboardButton[] { "Planned", "In progress", "Completed" },
                        new KeyboardButton[] { "Add new book", "Search for a book" },
                        new KeyboardButton[] { "Remove existing book" },
                        new KeyboardButton[] { "Help", "Back" },
                    }
                };
            }
        }
    }
}