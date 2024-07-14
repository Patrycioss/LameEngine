namespace LameEngine;

public class GameTemplate
{
    protected readonly Engine engine;
    
    protected GameTemplate()
    {
        engine = Engine.I;
    }
    
    public virtual void Load()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Render()
    {
    }

    public virtual void Close()
    {
    }
}