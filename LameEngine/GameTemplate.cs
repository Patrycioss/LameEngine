namespace LameEngine;

public class GameTemplate
{
    protected readonly Engine Engine;
    
    protected GameTemplate()
    {
        Engine = Engine.I;
    }
    
    public virtual void Load()
    {
    }

    public virtual void Update()
    {
    }
    
    public virtual void Close()
    {
    }
}