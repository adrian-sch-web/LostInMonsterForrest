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

        [HttpGet("Size")]
        public IActionResult GetSize()
        {
            Game game = CheckCache();
            Position boardSize = game.Map.Size;
            var mappedBoardSize = mapper.Map(boardSize);
            return Ok(mappedBoardSize);
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
        public IActionResult Delete()
        {
            _cache.Remove("key");
            return Ok();
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



        [HttpGet("Tree")]
        public IActionResult GetTree()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Tree.png");
            return File(b, "image/png");
        }

        [HttpGet("Floor")]
        public IActionResult GetFloor()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Floor.png");
            return File(b, "image/png");
        }

        [HttpGet("Empty")]
        public IActionResult GetEmpty()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Empty.png");
            return File(b, "image/png");
        }

        [HttpGet("Player")]
        public IActionResult GetPlayerImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Player.png");
            return File(b, "image/png");
        }

        [HttpGet("PlayerInFight")]
        public IActionResult GetPlayerFightImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/PlayerInFight.png");
            return File(b, "image/png");
        }

        [HttpGet("NormaloInFight")]
        public IActionResult GetNormaloFightImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/NormaloInFight.png");
            return File(b, "image/png");
        }

        [HttpGet("AttackoInFight")]
        public IActionResult GetAttackoFightImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/AttackoInFight.png");
            return File(b, "image/png");
        }

        [HttpGet("GigantoInFight")]
        public IActionResult GetGigantoFightImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/GigantoInFight.png");
            return File(b, "image/png");
        }

        [HttpGet("Door")]
        public IActionResult GetDoorImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Door.png");
            return File(b, "image/png");
        }

        [HttpGet("Normalo")]
        public IActionResult GetNormaloImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Normalo.png");
            return File(b, "image/png");
        }

        [HttpGet("Attacko")]
        public IActionResult GetAttackoImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Attacko.png");
            return File(b, "image/png");
        }

        [HttpGet("Giganto")]
        public IActionResult GetGigantoImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Giganto.png");
            return File(b, "image/png");
        }

        [HttpGet("DamageUp")]
        public IActionResult GetDanageUpImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/DamageUp.png");
            return File(b, "image/png");
        }

        [HttpGet("CritUp")]
        public IActionResult GetCritUpImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/CritUp.png");
            return File(b, "image/png");
        }

        [HttpGet("Heal")]
        public IActionResult GetHealImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Heal.png");
            return File(b, "image/png");
        }

        [HttpGet("Thomb")]
        public IActionResult GetThombImage()
        {
            byte[] b = System.IO.File.ReadAllBytes("./Assets/Images/Thomb.png");
            return File(b, "image/png");
        }
    }
}
