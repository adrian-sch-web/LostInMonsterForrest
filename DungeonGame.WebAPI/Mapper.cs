using DungeonGame.Core;

namespace DungeonGame.WebAPI
{
    public class Mapper
    {

        public LeaderboardEntry Map(LbEntryDto dto)
        {
            return (new LeaderboardEntry(dto.ID, dto.Name, dto.Floor, dto.Kills));
        }

        public LbEntryDto Map(LeaderboardEntry entry)
        {
            var dto = new LbEntryDto(entry.ID, entry.Name, entry.Floor, entry.Kills);
            return dto;
        }

        public GameUpdateDto Map(Game game)
        {
            var dto = new GameUpdateDto()
            {
                Attacks = new AttackDto[game.Attacks.Length],
                State = Map(game.Stats, game.IsRunning, game.FightMode, game.recordSubmitted),
                Board = Map(game.Map)
            };
            for (int i = 0; i < game.Attacks.Length; i++)
            {
                dto.Attacks[i] = Map(game.Attacks[i]);
            }
            return dto;
        }

        public BoardFloorDto Map(FloorType[,] board)
        {
            var dto = new BoardFloorDto(board);
            return dto;
        }

        private AttackDto Map(Attack attack)
        {
            var dto = new AttackDto()
            {
                Attacker = attack.Attacker,
                MonsterId = attack.Monster.Id,
                Risky = attack.Risky,
                CriticalStrike = attack.CriticalStrike,
                Kill = attack.Kill,
                Damage = attack.Damage,
            };
            return dto;
        }

        private BoardDto Map(Map map)
        {
            var dto = new BoardDto()
            {
                Player = Map(map.Player),
                Door = Map(map.Door),
                Monsters = new MonsterDto[map.Monsters.Count],
                Items = new ItemDto[map.Items.Count]
            };

            for (int i = 0; i < dto.Monsters.Length; i++)
            {
                dto.Monsters[i] = Map(map.Monsters[i]);
            }

            for (int i = 0; i < dto.Items.Length; i++)
            {
                dto.Items[i] = Map(map.Items[i]);
            }

            return dto;
        }

        private GameStateDto Map(ProgressStats stats, bool isRunning, int fightMode, bool recordSubmitted)
        {
            var dto = new GameStateDto()
            {
                Floor = stats.Floor,
                Kills = stats.Kills,
                Steps = stats.Steps,
                Running = isRunning,
                FightMode = fightMode,
                RecordSubmitted = recordSubmitted
            };
            return dto;
        }

        private MonsterDto Map(Monster monster)
        {
            var dto = new MonsterDto(monster.Id, monster.Type, monster.MaxHp, monster.Hp, monster.Damage, monster.CritChance, monster.Position.X, monster.Position.Y);
            return dto;
        }

        private ItemDto Map(Item item)
        {
            var dto = new ItemDto(item.Id, item.Type, item.Position.X, item.Position.Y);
            return dto;
        }

        private FighterDto Map(Fighter fighter)
        {
            var dto = new FighterDto(fighter.MaxHp, fighter.Hp, fighter.Damage, fighter.CritChance, fighter.Position.X, fighter.Position.Y);
            return dto;
        }

        private PlaceableDto Map(Placeable placeable)
        {
            var dto = new PlaceableDto(placeable.Position.X, placeable.Position.Y);
            return dto;
        }
    }
}
