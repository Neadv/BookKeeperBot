using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace BookKeeperBot.Models.Commands
{
    public class AboutCommand : Command
    {
        private string mainMenuMessage = "From the main menu you can view, create and delete bookshelves\n<em>Main menu command list:</em>" + 
                                         "\n/add - add new bookshelf\n/remove - remove existing bookshelf\n/list - show all bookshelves";
        private string bookMenuMessage = "From the book menu you can view, create and delete books\n<em>Book menu command list:</em>" +
                                         "\n/add - add new book\n/add_search - search book by title\n/remove - remove existing book\n/list - show all books" +
                                         "\n/in_progress - show books in the process of reading\n/completed - show completed books\n/planned - show planned books";
        private string editBookMenuMessage = "From the edit book menu you can edit all properties selected book<em>Edit book menu command list:</em>" + 
                                             "\n/title - edit book title\n/description - edit book description\n/note - edit note\n/category - change category\n/image - change image\n/image_remove - remove image";

        public AboutCommand()
        {
            Name = "/about";
            Alias = new string[] { "/help", "/commands" };
            State = CommandState.NoContext;
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string message = context.State switch
            {
                CommandState.MainMenu => mainMenuMessage,
                CommandState.BookMenu => bookMenuMessage,
                CommandState.EditBookMenu => editBookMenuMessage,
                _ => null
            };

            if (message != null)
                await BotClient.SendTextMessageAsync(context.Message.Chat, message, ParseMode.Html);
        }
    }
}