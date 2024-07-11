using LameEngine;

namespace TestGame
{
    public static class Program
    {
        public static void Main()
        {
            Engine.Initialize();
            Engine.Run(new Game());
        }
    }
}