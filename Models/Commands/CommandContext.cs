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
        public User User { get; set; }
        public CommandState State { get; set; }
        public string PreviousCommand { get; set; }
        public List<Bookshelf> Bookshelves { get; set; }
        public Bookshelf SelectedBookshelf { get; set; }
        public Book SelectedBook { get; set; }
        public Message Message { get; set; }

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

        public void AddBook(Book book)
        {
            if (SelectedBookshelf != null)
            {
                if (SelectedBookshelf.Books == null)
                    SelectedBookshelf.Books = new List<Book>();
                
                SelectedBookshelf.Books.Add(book);
            }
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