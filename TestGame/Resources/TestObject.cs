using LameEngine;
using Silk.NET.Maths;

namespace TestGame.Resources;

public class TestObject : GameObject
{
    public TestObject() : base(new Vector2D<float>(200,200))
    {
        SpriteSettings spriteSettings = new SpriteSettings()
        {
            Resolution = new Vector2D<int>(200, 200),
        };
        AddComponent(new Sprite("Resources/Sprites/awesomeface.png", spriteSettings));
    }
}