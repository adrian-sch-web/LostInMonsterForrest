using DungeonGame.Core;

namespace DungeonGame.WebAPI
{
    public class Mapper
    {
        public PositionDto Map(Position position)
        {
            var dto = new PositionDto(position.X, position.Y);
            return dto;
        }

        public GameUpdateDto Map(Game game)
        {
            var dto = new GameUpdateDto()
            {
                Attacks = new AttackDto[game.Attacks.Length],
                State = Map(game.Stats, game.IsRunning, game.FightMode),
                Board = Map(game.Map)
            };
            for (int i = 0; i < game.Attacks.Length; i++)
            {
                dto.Attacks[i] = Map(game.Attacks[i]);
            }
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

        private GameStateDto Map(ProgressStats stats, bool isRunning, int fightMode)
        {
            var dto = new GameStateDto()
            {
                Floor = stats.Floor,
                Kills = stats.Kills,
                Steps = stats.Steps,
                Running = isRunning,
                FightMode = fightMode,
            };
            return dto;
        }

        private MonsterDto Map(Monster monster)
        {
            var dto = new MonsterDto(monster.Id, monster.Type, monster.Hp, monster.Damage, monster.CritChance, monster.Position.X, monster.Position.Y);
            return dto;
        }

        private ItemDto Map(Item item)
        {
            var dto = new ItemDto(item.Id, item.Type, item.Position.X, item.Position.Y);
            return dto;
        }

        private FighterDto Map(Fighter fighter)
        {
            var dto = new FighterDto(fighter.Hp, fighter.Damage, fighter.CritChance, fighter.Position.X, fighter.Position.Y);
            return dto;
        }

        private PlaceableDto Map(Placeable placeable)
        {
            var dto = new PlaceableDto(placeable.Position.X, placeable.Position.Y);
            return dto;
        }
    }
}
