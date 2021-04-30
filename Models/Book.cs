namespace BookKeeperBot.Models
{
    public class Book
    {   
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public BookState State { get; set; }
        public string ImageUrl { get; set; }
        public string ImageId { get; set; }

        public Bookshelf Bookshelf { get; set; }
        public int BookshelfId { get; set; }
    }

    public enum BookState
    {
        InProgress,
        Completed,
        Planned
    }
}