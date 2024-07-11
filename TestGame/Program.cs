using LameEngine;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Rectangle = LameEngine.Rectangle;
using Texture = LameEngine.Texture;

namespace TestGame
{
    public static class Program
    {
        public static void Main()
        {
            WindowOptions windowOptions = WindowOptions.Default;
            windowOptions.Size = new Vector2D<int>(400, 400);
            windowOptions.WindowBorder = WindowBorder.Hidden;
            windowOptions.TransparentFramebuffer = true;


            WindowManager.CreateMainWindow(windowOptions);
            Rectangle.Initialize();

            // Vector2D<int> position = windowOptions.Position;
            // position.X = 500;
            // windowOptions.Position = position;
            // windowOptions.SharedContext = WindowManager.MainWindow.SharedContext;
            // IWindow addWindow = WindowManager.CreateWindow(windowOptions);

            IKeyboard keyboard = WindowManager.InputContext.Keyboards.FirstOrDefault();
            Random random = new Random();

            WindowManager.MainWindow.TopMost = true;

            Texture texture = new Texture("Resources/Sprites/awesomeface.png");

            WindowManager.MainWindow.Update += pD =>
            {
                if (keyboard.IsKeyPressed(Key.Space))
                {
                    windowOptions.Position = new Vector2D<int>(random.Next(0, 1000), random.Next(0, 1000));
                    WindowManager.CreateWindow(windowOptions);
                }

                if (keyboard.IsKeyPressed(Key.C))
                {
                    WindowManager.CloseAllAdditionalWindows();
                }
            };


            WindowManager.MainWindow.Render += pD =>
            {
                WindowManager.GL.Clear((uint) (ClearBufferMask.ColorBufferBit));
                Console.WriteLine("ha");
                Rectangle.Draw(texture);
                // WindowManager.MainWindow.SwapBuffers();
            };

            // WindowManager.MainWindow.Run();
            
            WindowManager.Run();
        }
    }
}