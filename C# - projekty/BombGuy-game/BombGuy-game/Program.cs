namespace BombGuy_game
{
    //Game BOMBGUY
    //Mat�j Mezera, I. ro�n�k, kruh 33
    //letn� semestr 2021/2022
    //Programov�n� 2 NPRG031
    internal static class Program
    { 
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        public static Game game;
        public static GameMenu menu = new GameMenu();
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(Program.menu);
        }
    }
}