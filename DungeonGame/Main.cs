using DungeonGame;

Game game = new Game();
game.Setup();
UserInterface UI = new UserInterface(game.BoardSize);
UI.Refresh(game);
while (game.IsRunning)
{
    game.Press(UI.WaitUserInput());
    UI.Refresh(game);
}
UI.Refresh(game);