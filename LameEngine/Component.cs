namespace LameEngine;

public abstract class Component
{
    public override int GetHashCode()
    {
        return GetType().GetHashCode();
    }

    protected virtual void Update()
    {
    }

    protected virtual void Render()
    {
    }

    internal void InternalUpdate()
    {
        Update();
    }

    internal void InternalRender()
    {
        Render();
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