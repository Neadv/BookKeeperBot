using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookKeeperBot.Models.Commands
{
    public class ChangeStateCommand : Command
    {
        public ChangeStateCommand()
            : base("/change", CommandState.NoContext, true) { }

        public async override Task ExecuteAsync(CommandContext context)
        {
            string input = context.CommandName.Replace(Name, string.Empty);
            if (int.TryParse(input, out int state))
            {
                var states = CommandState.GetValues<CommandState>();
                if (states.Any(s => (int)s == state))
                {
                    var commandState = (CommandState)state;
                    await ChangeState(context, commandState);
                }
            }
        }

        private async Task ChangeState(CommandContext context, CommandState commandState)
        {
            if (!string.IsNullOrEmpty(context.Parameters))
            {
                var command = (new Regex(@"^/\w+", RegexOptions.IgnoreCase)).Match(context.Parameters).Value;
                if (!string.IsNullOrEmpty(command))
                {
                    context.RedirectToCommand(command, commandState, context.Parameters.Replace(command, string.Empty), true);
                }
            }
            else
            {
                context.ChangeState(commandState);
                await BotClient.SendTextMessageAsync(context.Message.Chat, Localizer["ChangeState"]);
            }
        }

        public override bool Check(CommandString command)
            => !string.IsNullOrEmpty(command.CommandName) && command.CommandName.StartsWith(Name) && command.CommandName.Length > Name.Length;
    }
}