using System.Collections.Generic;
using BookKeeperBot.Models.Commands;

namespace BookKeeperBot.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Language { get; set; }
        public List<Bookshelf> Bookshelves { get; set; }

        public CommandState State { get; set; }
        public string PreviousCommand { get; set; }

        public int? SelectedBookshelfId { get; set; }
        public Bookshelf SelectedBookshelf { get; set; }
        
        public int? SelectedBookId { get; set; }
        public Book SelectedBook { get; set; }

        public Bookshelf GetBookshelf(string name)
            => Bookshelves.Find(bs => bs.Name == name);
    }
}