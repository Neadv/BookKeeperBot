using System.Collections.Generic;
using Telegram.Bot.Types;

namespace BookKeeperBot.Models.Commands
{
    public class CommandContext
    {
        public string CommandName { get; set; }
        public string Parameters { get; set; }
        public string Data { get; set; }
        public bool IsCallback { get; set; }
        public Message Message { get; set; }
        public User User { get; set; }

        public string PreviousCommand => User?.PreviousCommand;

        public CommandState State
        {
            get
            {
                if (User != null)
                    return User.State;
                return CommandState.NoContext;
            }
        }

        public Bookshelf SelectedBookshelf
        {
            get => User?.SelectedBookshelf;
            set
            {
                if (User != null)
                    User.SelectedBookshelf = value;
            }
        }
        public Book SelectedBook
        {
            get => User?.SelectedBook;
            set
            {
                if (User != null)
                    User.SelectedBook = value;
            }
        }
        public List<Bookshelf> Bookshelves => User?.Bookshelves;

        public void ChangeState(CommandState state)
        {
            if (User != null)
                User.State = state;
        }

        public void AddBookshelf(Bookshelf bookshelf)
        {
            if (User != null)
            {
                if (User.Bookshelves == null)
                    User.Bookshelves = new List<Bookshelf>();

                User.Bookshelves.Add(bookshelf);
            }
        }

        public void RemoveBookshelf(Bookshelf bookshelf)
        {
            if (User != null && User.Bookshelves != null)
            {
                User.Bookshelves.Remove(bookshelf);
            }
        }

        public void AddBook(Book book)
        {
            if (SelectedBookshelf != null)
            {
                if (SelectedBookshelf.Books == null)
                    SelectedBookshelf.Books = new List<Book>();

                SelectedBookshelf.Books.Add(book);
            }
        }

        public void RemoveBook(Book book)
        {
            if (SelectedBookshelf != null && SelectedBookshelf.Books != null)
            {
                SelectedBookshelf.Books.Remove(book);
            }
        }

        private string redirectCommandName;
        private string redirectParameters;
        private bool redirectCallback;
        private CommandState redirectState;
        
        public void RedirectToCommand(string commandName)
            => RedirectToCommand(commandName, State);

        public void RedirectToCommand(string commandName, string parameters)
            => RedirectToCommand(commandName, State, parameters);

        public void RedirectToCommand(string commandName, CommandState state, string parameters = null, bool isCallback = false)
        {
            redirectCommandName = commandName;
            redirectState = state;
            redirectParameters = parameters;
            redirectCallback = isCallback;
        }

        public CommandContext GetRedirectContext()
        {
            if (redirectCommandName == null)
                return null;
            
            User.State = redirectState;
            return new CommandContext
            {
                CommandName = redirectCommandName,
                User = User,
                Message = Message,
                Parameters = redirectParameters,
                IsCallback = redirectCallback
            };
        }

        public CommandString GetCommandString()
        {
            return new CommandString
            {
                CommandName = CommandName,
                ContainData = !string.IsNullOrEmpty(Data),
                PreviosCommand = PreviousCommand,
                State = State,
                IsAuthorized = User != null
            };
        }
    }
}