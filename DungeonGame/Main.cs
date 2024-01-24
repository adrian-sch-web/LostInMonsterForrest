using DungeonGame;

Game game = new();
game.Setup();
UserInterface UI = new(game.BoardSize);
UI.Refresh(game);
while (game.IsRunning)
{
    game.Press(UI.WaitUserInput());
    UI.Refresh(game);
}
UI.Refresh(game);