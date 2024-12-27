using ChatModels;
using DemoChatApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace DemoChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController (ChatRepo chatRepo): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetChatsAsync()
            => Ok(await chatRepo.GetChatsAsync());


        [HttpGet("users")]
        public async Task<ActionResult<List<Chat>>> GetAvailableUsersAsync()
            => Ok(await chatRepo.GetAvailableUsersAsync());
    }
}
