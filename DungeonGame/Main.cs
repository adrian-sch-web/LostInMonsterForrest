using DungeonGame.Core;
using DungeonGame.UI;

Game game = new();
UserInterface UI = new(game.Map.Size);
UI.Refresh(game);
while (game.IsRunning)
{
    game.Action(UI.WaitUserInput(game));
    UI.Refresh(game);
}
UI.Refresh(game);