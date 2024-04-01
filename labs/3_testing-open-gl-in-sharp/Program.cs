namespace TestOpenGL
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (Game game = new Game(1280, 768, "Testing OpenTK"))
            {
                game.Run();
            }
        }
    }
}