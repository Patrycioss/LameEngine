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
            windowOptions.TopMost = true;
            
            // windowOptions.WindowBorder = WindowBorder.Hidden;
            // windowOptions.TransparentFramebuffer = true;

            Engine engine = new Engine(windowOptions);
            engine.Run(new Game());
        }
    }
}