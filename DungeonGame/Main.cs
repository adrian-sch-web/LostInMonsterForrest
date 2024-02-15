using DungeonGame.Core;
using DungeonGame.UI;

Game game = new();
UserInterface UI = new(Map.Size);
UI.Refresh(game);
while (game.IsRunning)
{
    Input input = UI.WaitUserInput(game);
    if (game.FightMode == -1)
    {
        Direction direction = InputToDirection(input);
        game.MoveTurn(direction);
    }
    else
    {
        game.FightTurn(input == Input.RiskyAttack);
    }
    UI.Refresh(game);
}
UI.Refresh(game);

static Direction InputToDirection(Input input)
{
    return input switch
    {
        Input.Left => Direction.Left,
        Input.Right => Direction.Right,
        Input.Up => Direction.Up,
        Input.Down => Direction.Down,
        _ => Direction.Idle,
    };
}