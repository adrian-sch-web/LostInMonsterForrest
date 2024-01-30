using DungeonGame.Core;
using Microsoft.AspNetCore.Mvc;

namespace DungeonGame.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DungeonGameController : ControllerBase
    {
        private readonly Game game = new();
        private readonly Mapper mapper = new();
                       
        [HttpGet("BoardSize")]
        public PositionDto GetSize()
        {
            Position boardSize = game.Map.Size;
            var mappedBoardSize = mapper.Map(boardSize);
            return mappedBoardSize;
        }

        [HttpGet("GameState")]
        public GameStateDto GetStats()
        {
            var mappedStats = mapper.Map(game);
            return mappedStats;
        }

        [HttpGet("Board")]
        public BoardDto GetBoard()
        {
            Map map = game.Map;
            var mappedBoard = mapper.Map(map);
            return mappedBoard;
        }

        [HttpGet("Attack")]
        public AttackDto[] GetAttack()
        {
            Attack[] attacks = game.Attacks;
            var mappedAttacks = new AttackDto[attacks.Length];
            for (int i = 0; i < mappedAttacks.Length; i++)
            {
                mappedAttacks[i] = mapper.Map(attacks[i]);
            }
            return mappedAttacks;
        }


    }
}
