using System.Collections.Generic;

namespace BookKeeperBot.Models
{
    public class Bookshelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public IEnumerable<Book> GetInProgress()
            => GetBooks(BookState.InProgress);
        
        public IEnumerable<Book> GetCompleted()
            => GetBooks(BookState.Completed);

        public IEnumerable<Book> GetPlanned()
            => GetBooks(BookState.Planned);
        
        public IEnumerable<Book> GetBooks(BookState state)
            => Books.FindAll(book => book.State == state);
    }
}