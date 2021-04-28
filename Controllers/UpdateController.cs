using Microsoft.AspNetCore.Mvc;

namespace BookKeeperBot.Controllers
{
    [ApiController]
    [Route("api/update")]
    public class UpdateController : ControllerBase
    {
        [HttpPost]
        public ActionResult Update()
        {
            return Ok();
        } 
    }
}