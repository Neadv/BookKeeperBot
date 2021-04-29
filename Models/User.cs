using System.Collections.Generic;

namespace BookKeeperBot.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Bookshelf> Bookshelves { get; set; }

        public string PreviousCommand { get; set; }

        public int? SelectedBookshelfId { get; set; }
        public Bookshelf SelectedBookshelf { get; set; }
        
        public int? SelectedBookId { get; set; }
        public Book SelectedBook { get; set; }

        public Bookshelf GetBookshelf(string name)
            => Bookshelves.Find(bs => bs.Name == name);
    }
}