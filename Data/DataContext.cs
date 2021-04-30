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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.SelectedBookshelf).WithOne().HasForeignKey<User>(u => u.SelectedBookshelfId);

            modelBuilder.Entity<User>().HasMany(u => u.Bookshelves).WithOne(bs => bs.User).HasForeignKey(bs => bs.UserId);
            modelBuilder.Entity<Bookshelf>().HasOne(bs => bs.User).WithMany(u => u.Bookshelves).HasForeignKey(bs => bs.UserId);
        }
    }
}