using LameEngine;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using TestGame.Resources;

namespace TestGame;

public class Game : GameTemplate
{
    private IKeyboard keyBoard;
    private TestObject testObject;

    public Game()
    {
        // Vector2D<int> position = windowOptions.Position;
        // position.X = 500;
        // windowOptions.Position = position;
        // windowOptions.SharedContext = WindowManager.MainWindow.SharedContext;
        // IWindow addWindow = WindowManager.CreateWindow(windowOptions);
        
        // windowOptions.WindowBorder = WindowBorder.Hidden;
        // windowOptions.TransparentFramebuffer = true;

        keyBoard = engine.InputContext.Keyboards.FirstOrDefault();
        Random random = new Random();

        testObject = new TestObject();
        
        
            
        // WindowManager.MainWindow.Update += pD =>
        // {
        //     if (keyboard.IsKeyPressed(Key.Space))
        //     {
        //         windowOptions.Position = new Vector2D<int>(random.Next(0, 1000), random.Next(0, 1000));
        //         WindowManager.CreateWindow(windowOptions);
        //     }
        //
        //     if (keyboard.IsKeyPressed(Key.C))
        //     {
        //         WindowManager.CloseAllAdditionalWindows();
        //     }
        // };


        // WindowManager.MainWindow.Render += pD =>
        // {
        //     WindowManager.GL.Clear((uint) (ClearBufferMask.ColorBufferBit));
        //     // Rectangle.Draw(new Rectangle.RenderSettings());
        //     // WindowManager.MainWindow.SwapBuffers();
        // };
    }
    
    public override void Load()
    {
        
    }

    private const float SPEED = 200;
    

    public override void Update()
    {
        Vector2D<float> direction = new Vector2D<float>(0, 0);
        
        if (keyBoard.IsKeyPressed(Key.W))
        {
            direction.Y--;
        }
        
        if (keyBoard.IsKeyPressed(Key.S))
        {
            direction.Y++;
        }
        
        if (keyBoard.IsKeyPressed(Key.A))
        {
            direction.X--;
        }
        
        if (keyBoard.IsKeyPressed(Key.D))
        {
            direction.X++;
        }

        // Console.WriteLine($"Delta Time: {WindowManager.DeltaTime}");
        
        testObject.Transform.Position += engine.WindowManager.DeltaTime * direction * SPEED;
    }


    public override void Render()
    {
        
    }
}