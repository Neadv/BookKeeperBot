using System.Threading.Tasks;
using BookKeeperBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BookKeeperBot.Controllers
{
    [ApiController]
    [Route("api/update")]
    public class UpdateController : ControllerBase
    {
        private readonly ICommandSelector selector;
        private readonly IContextFactory factory;
        private readonly IBotLocalizer botLocalizer;

        public UpdateController(ICommandSelector commandSelector, IContextFactory contextFactory, IBotLocalizer localizer)
        {
            selector = commandSelector;
            factory = contextFactory;
            botLocalizer = localizer;
        }

        [HttpPost]
        public async Task<ActionResult> Update(Update update)
        {
            var context = await factory.CreateContextAsync(update);
            botLocalizer.Localize(context);
            await selector.SelectAsync(context);
            return Ok();
        } 
    }
}