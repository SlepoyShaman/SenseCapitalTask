using Microsoft.AspNetCore.Authorization;
using SenseCapitalTask.Models;
using Microsoft.AspNetCore.Mvc;
using SenseCapitalTask.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace SenseCapitalTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly Referee _referee;
        public GameController(IHttpContextAccessor contextAccessor, IRepository repository, UserManager<User> userManager, Referee referee)
        {
            _contextAccessor = contextAccessor;
            _repository = repository;
            _userManager = userManager;
            _referee = referee;
        }

        [HttpPost("MakeTurn")]
        public async Task<IActionResult> MakeTurn(int position)
        {
            if (position < 0 || position > 8) return BadRequest(new { Error = "Select a valid position"  });

            var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);

            var game = await _repository.GetAll<Game>().Where(g => g.FirstPlayerId == user.Id || g.SecondPlayerId == user.Id)
                .FirstOrDefaultAsync(g => g.IsStarted);

            bool isIFirst = game.FirstPlayerId == user.Id;
            var sbField = new StringBuilder(game.fienld);

            if (sbField[position] != '*') return BadRequest(new { Error = $"Position {position} is taken"});

            if (isIFirst && game.IsFirstPlayerTurn) { sbField[position] = 'x'; }
            else if (!isIFirst && !game.IsFirstPlayerTurn) { sbField[position] = 'o'; }
            else { return BadRequest(new { Error = "Wait for enemy turn"}); }

            game.IsFirstPlayerTurn = !game.IsFirstPlayerTurn;
            game.fienld = sbField.ToString();

            if (_referee.IsItWin(game.fienld))
            {
                if(game.IsFirstPlayerTurn)
                {
                    game.SecondPlayerPoints++;
                }
                else
                {
                    game.FirstPlayerPoints++;
                }

                game.IsFirstPlayerTurn = true;
                game.fienld = "*********";
            }

            await _repository.UpdateAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);

            var res = await _repository.GetAll<Game>().Where(g => g.FirstPlayerId == user.Id || g.SecondPlayerId == user.Id)
                .FirstOrDefaultAsync(g => g.IsStarted);

            if (res == null) return BadRequest(new { Error = "You have no started games" });
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Leave()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                var game = await _repository.GetAll<Game>().Where(g => g.FirstPlayerId == user.Id || g.SecondPlayerId == user.Id)
                .FirstOrDefaultAsync(g => g.IsStarted);

                await _repository.RemoveByIdAsync<Game>(game.Id);

                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(new { Error = ex.Message });
            }
            
        }

    }
}
