using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenseCapitalTask.Data;
using SenseCapitalTask.Models;

namespace SenseCapitalTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvitationController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public InvitationController(IRepository repository, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvitations()
        {
            var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            
            var result = await _repository.GetAll<Invitation>().Where(i => i.UserId == user.Id).ToArrayAsync();
            return Ok(result);
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendInvitation(string userName)
        {
            var secondPlayer = await _userManager.FindByNameAsync(userName);
            if (secondPlayer == null) { return BadRequest(new { Error = $"User {userName} not found" }); }

            var host = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);

            var game = new Game() { FirstPlayerId = host.Id, SecondPlayerId = secondPlayer.Id };

            await _repository.AddAsync(game);

            var invitation = new Invitation() { FromUser = host.UserName, UserId = secondPlayer.Id, GameId = game.Id };

            await _repository.AddAsync(invitation);

            return Ok();
        }

        [HttpPost("Accept")]
        public async Task<IActionResult> AcceptInvitation(int invitationId)
        {
            var invitation = await _repository.GetByIdAsync<Invitation>(invitationId);
            if (invitation == null) { return BadRequest(new { Error = $"There is no inventation with {invitationId} id" }); }

            var game = await _repository.GetByIdAsync<Game>(invitation.GameId);
            game.IsStarted = true;
            await _repository.UpdateAsync();

            await _repository.RemoveByIdAsync<Invitation>(invitationId);
            return Ok();
        }

        [HttpPost("Cancel")]
        public async Task<IActionResult> CancelInvitation(int invitationId)
        {
            try
            {
                var invitation = await _repository.GetByIdAsync<Invitation>(invitationId);
                await _repository.RemoveByIdAsync<Game>(invitation.GameId);
                await _repository.RemoveByIdAsync<Invitation>(invitationId);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
