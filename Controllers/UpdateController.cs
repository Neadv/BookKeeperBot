using System.Threading.Tasks;
using BookKeeperBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookKeeperBot.Controllers
{
    [ApiController]
    [Route("api/update")]
    public class UpdateController : ControllerBase
    {
        private readonly ITelegramBotClient botClient;

        public UpdateController(IBotService botService)
        {
            botClient = botService.Client;
        }

        [HttpPost]
        public async Task<ActionResult> Update(Update update)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, update.Message.Text);
            return Ok();
        } 
    }
}