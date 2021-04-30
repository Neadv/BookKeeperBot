using System.Collections.Generic;

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

        public CommandString GetCommandString()
        {
            return new CommandString
            {
                CommandName = CommandName,
                ContainData = !string.IsNullOrEmpty(Data),
                PreviosCommand = PreviousCommand,
                State = State
            };
        }
    }
}