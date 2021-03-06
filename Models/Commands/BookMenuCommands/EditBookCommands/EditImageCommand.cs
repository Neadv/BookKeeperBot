using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class EditImageCommand : Command
    {
        public EditImageCommand() : base("/image", CommandState.EditBookMenu, true)
        {
            Alias = new[] { "/remove_image" };
        }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string removeMessage = Localizer["BookEditImageRemove"];
            string editMessage = Localizer["BookEditImageSuccess"];
            string enterMessage = Localizer["BookEditImageEnter"];

            var book = context.SelectedBook;
            if (book == null)
                return;

            var message = enterMessage;

            if (context.CommandName == Alias[0])
            {
                book.ImageId = null;
                book.ImageUrl = null;
                message = removeMessage;
            }
            else if (context.Message.Photo?.Length > 0)
            {
                book.ImageId = context.Message.Photo[0].FileId;
                message = editMessage;
            }

            await BotClient.SendTextMessageAsync(context.Message.Chat, message);
        }

        public override bool Check(CommandString command)
            => base.Check(command) || (command.IsAuthorized && command.State == State && command.PreviosCommand == Name);
    }
}