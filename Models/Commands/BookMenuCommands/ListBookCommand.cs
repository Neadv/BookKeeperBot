using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class ListBookCommand : Command
    {
        private string inProgress = "/in_progress";
        private string completed = "/completed";
        private string planned = "/planned";

        public ListBookCommand()
        {
            Name = "/list";
            State = CommandState.BookMenu;
            Alias = new string[] { inProgress, completed, planned };
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            IEnumerable<Book> books = context.SelectedBookshelf.Books;

            if (context.CommandName == inProgress)
                books = context.SelectedBookshelf.GetInProgress();
            else if (context.CommandName == completed)
                books = context.SelectedBookshelf.GetCompleted();
            else if (context.CommandName == planned)
                books = context.SelectedBookshelf.GetPlanned();

            var stringBuilder = new StringBuilder("Books:\n");
            foreach (var book in books)
            {
                stringBuilder.AppendLine($"{book.Title} - /edit{book.Id}, /select{book.Id}, /remove{book.Id}");
            }
            await BotClient.SendTextMessageAsync(context.Message.Chat, stringBuilder.ToString());
        }
    }
}