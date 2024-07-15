using LameEngine;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace TestGame
{
    public static class Program
    {
        public static void Main()
        {
            WindowOptions windowOptions = WindowOptions.Default;
            windowOptions.Size = new Vector2D<int>(400, 400);
      
            WindowSettings windowSettings = new WindowSettings(new Vector2D<int>(400, 400));

            Engine engine = new Engine(windowOptions, windowSettings);
            engine.Run(new Game());
        }
    }
}