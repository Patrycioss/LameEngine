using LameEngine;
using Silk.NET.Input;
using Silk.NET.Maths;

namespace TestGame;

public class TestObject : GameObject
{
    private readonly IKeyboard keyboard;
    private readonly float speed;
    
    public TestObject(Window pWindow, float pSpeed) : base(new Vector2D<float>(200, 200), pTarget: pWindow)
    {
        keyboard = pWindow.InputContext.Keyboards[0];
        speed = pSpeed;
        
        SpriteSettings spriteSettings = new SpriteSettings("Resources/Sprites/awesomeface.png")
        {
            Resolution = new Vector2D<int>(200,200),
        };

        AddComponent(new Sprite(spriteSettings));
    }

    protected override void Update()
    {
        if (!Target.HasFocus)
        {
            return;
        }
        
        Vector2D<float> direction = new Vector2D<float>(0, 0);

        if (!keyboard.IsConnected)
        {
            Console.WriteLine($"jsjadsfk;afj;asf");
            return;
        }

        if (keyboard.IsKeyPressed(Key.W))
        {
            direction.Y--;
        }

        if (keyboard.IsKeyPressed(Key.S))
        {
            direction.Y++;
        }

        if (keyboard.IsKeyPressed(Key.A))
        {
            direction.X--;
        }

        if (keyboard.IsKeyPressed(Key.D))
        {
            direction.X++;
        }

        Transform.Position += Engine.DeltaTime * direction * speed;
    }
}