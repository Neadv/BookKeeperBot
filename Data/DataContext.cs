using BookKeeperBot.Models;
using Microsoft.EntityFrameworkCore;

namespace BookKeeperBot.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bookshelf> Bookshelves { get; set; }
        public DbSet<Book> Books { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }
    }
}