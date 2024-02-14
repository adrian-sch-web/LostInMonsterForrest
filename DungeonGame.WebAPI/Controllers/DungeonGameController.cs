using DungeonGame.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DungeonGame.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DungeonGameController(IMemoryCache _cache) : ControllerBase
    {
        private readonly Mapper mapper = new();


        [HttpGet("Board")]
        public IActionResult GetBoard()
        {
            Game game = CheckCache();
            FloorType[,] board = game.Map.Board;
            var mappedBoard = mapper.Map(board);
            return Ok(mappedBoard);
        }

        [HttpGet("State")]
        public IActionResult GetState()
        {
            Game game = CheckCache();
            var mappedUpdate = mapper.Map(game);
            return Ok(mappedUpdate);
        }

        [HttpPost("Fight")]
        public IActionResult Attack(RiskyDto risky)
        {
            Game game = CheckCache();
            if (game.FightMode == -1)
            {
                return BadRequest();
            }
            game.FightTurn(risky.Risky);
            var mappedUpdate = mapper.Map(game);
            return Ok(mappedUpdate);
        }

        [HttpPost("Move")]
        public IActionResult Move(DirectionDto direction)
        {
            Game game = CheckCache();
            if (game.FightMode != -1)
            {
                return BadRequest();
            }
            game.MoveTurn(direction.Direction);
            var mappedUpdate = mapper.Map(game);
            return Ok(mappedUpdate);
        }

        [HttpDelete("Restart")]
        public IActionResult Restart()
        {
            _cache.Set("key", new Game());
            return Ok();
        }

        [HttpGet("Leaderboard")]
        public IActionResult GetLeaderBoard()
        {
            var leaderboard = Leaderboard.GetLeaderBoard();
            List<LbEntryDto> mappedLb = new();
            foreach(var entry in leaderboard)
            {
                mappedLb.Add(mapper.Map(entry));
            }
            return Ok(mappedLb);
        }

        [HttpPost("SubmitScore")]
        public IActionResult AddEntry(LbEntryDto dto)
        {
            var game = CheckCache();
            if (!game.recordSubmitted)
            {
                var entry = mapper.Map(dto);
                Leaderboard.SaveRecord(entry);
                game.recordSubmitted = true;
            }
            _cache.Set("key", game);
            return GetLeaderBoard();
        }

        private Game CheckCache()
        {
            _cache.TryGetValue("key", out Game? game);
            if (game is null)
            {
                game = new();
                _cache.Set("key", game);
            }
            return game;
        }

        [HttpGet("Image/{name}")]
        public IActionResult GetImage(string name)
        {
            byte[] b;
            try
            {
                b = System.IO.File.ReadAllBytes("./Assets/Images/" + name + ".png");
            }
            catch
            {
                return NotFound();
            }
            return File(b, "image/png");
        }
    }
}
