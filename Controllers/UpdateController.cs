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

        public UpdateController(ICommandSelector commandSelector)
        {
            selector = commandSelector;
        }

        [HttpPost]
        public async Task<ActionResult> Update(Update update)
        {
            await selector.SelectAsync(update);
            return Ok();
        } 
    }
}