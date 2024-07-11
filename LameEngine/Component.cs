namespace LameEngine;

public abstract class Component
{
    public override int GetHashCode()
    {
        return GetType().GetHashCode();
    }

    public virtual void Update()
    {
        
    }

    public virtual void Render()
    {
        
    }

    public static bool operator ==(Component pFirst, Component pSecond)
    {
        return pFirst.GetType() == pSecond.GetType();
    }

    public static bool operator !=(Component pFirst, Component pSecond)
    {
        return !(pFirst == pSecond);
    }
}