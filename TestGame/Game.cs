using LameEngine;
using TestGame.Resources;

namespace TestGame;

public class Game : GameTemplate
{
    private TestObject testObject = new TestObject();

    public Game()
    {
    }
    
    public override void Load()
    {
        // awesomeFace = new Texture("Resources/Sprites/awesomeface.png");
    }

    public override void Render()
    {
        // Rectangle.Draw(awesomeFace);
    }
}