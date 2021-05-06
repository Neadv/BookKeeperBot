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

        public UpdateController(ICommandSelector commandSelector, IContextFactory contextFactory)
        {
            selector = commandSelector;
            factory = contextFactory;
        }

        [HttpPost]
        public async Task<ActionResult> Update(Update update)
        {
            var context = await factory.CreateContextAsync(update);
            await selector.SelectAsync(context);
            return Ok();
        } 
    }
}