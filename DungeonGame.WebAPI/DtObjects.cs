using DungeonGame.Core;

namespace DungeonGame.WebAPI
{

    public class LbEntryDto(int id, string name, int floor, int kills)
    {
        public int ID { get; set; } = id;
        public string Name { get; set; } = name;
        public int Floor { get; set; } = floor;
        public int Kills { get; set; } = kills;
    }
    public class RiskyDto(bool risky)
    {
        public bool Risky { get; set; } = risky;
    }
    public class DirectionDto(Direction direction)
    {
        public Direction Direction { get; set; } = direction; 
    }
    
    public class PositionDto(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
    }

    public class GameUpdateDto
    {
        public AttackDto[] Attacks { get; set; }
        public GameStateDto State { get; set; }
        public BoardDto Board { get; set; }
    }

    public class AttackDto
    {
        public bool Attacker { get; set; }
        public int MonsterId { get; set; }
        public bool Risky { get; set; }
        public bool CriticalStrike { get; set; }
        public bool Kill { get; set; }
        public int Damage { get; set; }
    }

    public class GameStateDto
    {
        public int Floor { get; set; }
        public int Kills { get; set; }
        public int Steps { get; set; }
        public bool Running { get; set; }
        public int FightMode { get; set; }
        public bool RecordSubmitted { get;set; }
    }

    public class BoardDto
    {
        public FighterDto Player { get; set; }
        public PlaceableDto Door { get; set; }
        public MonsterDto[] Monsters { get; set; }
        public ItemDto[] Items { get; set; }

    }

    public class ItemDto(int id, ItemType type, int x, int y) : PlaceableDto(x, y)
    {
        public int Id { get; set; } = id;
        public ItemType Type { get; set; } = type;
    }
    public class MonsterDto(int id, MonsterType type, int hp, int dmg, int crit, int x, int y) : FighterDto(hp, dmg, crit, x, y)
    {
        public int Id { get; set; } = id;
        public MonsterType Type { get; set; } = type;
    }

    public class FighterDto(int hp, int dmg, int crit, int x, int y) : PlaceableDto(x, y)
    {
        public int Hp { get; set; } = hp;
        public int Damage { get; set; } = dmg;
        public int CritChance { get; set; } = crit;
    }

    public class PlaceableDto(int x, int y)
    {
        public PositionDto Position { get; set; } = new PositionDto(x, y);
    }
}
