using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookKeeperBot.Models.Commands
{
    public class LanguageCommand : InputCommand<string>
    {
        private string[] availableLanguages = new[] { "en", "ru" };

        public LanguageCommand()
            : base("/language", CommandState.Settings) { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            if (InputData(context, out string input))
            {
                if (context.IsCallback)
                    await BotClient.EditMessageReplyMarkupAsync(context.Message.Chat, context.Message.MessageId, InlineKeyboardMarkup.Empty());

                string message = Localizer["LanguageError"];
                if (availableLanguages.Any(a => a == input))
                {
                    context.User.Language = input;
                    message = Localizer["LanguageSuccess"];
                }

                await BotClient.SendTextMessageAsync(context.Message.Chat, message);
            }
            else
            {
                List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                foreach (var language in availableLanguages)
                {
                    var row = new List<InlineKeyboardButton>();
                    row.Add(language);
                    buttons.Add(row);
                }
                InlineKeyboardMarkup markup = new InlineKeyboardMarkup(buttons);

                await BotClient.SendTextMessageAsync(context.Message.Chat, Localizer["LanguageEnter"], replyMarkup: markup);
            }
        }

        protected override string GetItem(CommandContext context, string input)
            => input;
    }
}