using DungeonGame.Core;
using DungeonGame.UI;

Game game = new();
game.Setup();
UserInterface UI = new(game.Map.Size);
UI.Refresh(game);
while (game.IsRunning)
{
    game.Press(UI.WaitUserInput());
    UI.Refresh(game);
}
UI.Refresh(game);