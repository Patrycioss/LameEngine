using LameEngine;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Window = LameEngine.Window;

namespace TestGame;

public class Game : GameTemplate
{
    public Game()
    {
        WindowOptions windowOptions = WindowOptions.Default;
        windowOptions.Size = new Vector2D<int>(500, 500);
        windowOptions.Title = "SubWindow";
        Vector2D<int> position = windowOptions.Position;
        position.X = 500;
        windowOptions.Position = position; 
        WindowSettings windowSettings = new WindowSettings(new Vector2D<int>(500, 500));


        Window side = Engine.CreateWindow(windowOptions, windowSettings);
        TestObject object1 = new TestObject(side, 200);
        TestObject object2 = new TestObject(Engine.MainWindow, 200);
    }
}