public static class Program
{
  private static breakout_mg.Game1 game;

  static void Main(string[] args)
  {
    NewGame();
  }

  public static void NewGame()
  {
    Program.game = new breakout_mg.Game1();
    Program.game.Run();

  }
}

