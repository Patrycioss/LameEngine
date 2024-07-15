using System.Diagnostics;

namespace LameEngine;

public abstract class Component
{
    protected GameObject gameObject;
    protected Transform transform;

    protected virtual void Start()
    {
        
    }
    
    protected virtual void Update()
    {
    }

    protected virtual void Render()
    {
    }

    internal void InternalStart(GameObject pGameObject, Transform pTransform)
    {
        gameObject = pGameObject;
        transform = pTransform;
        Start();
    }

    internal void InternalUpdate()
    {
        Update();
    }

    internal void InternalRender()
    {
        Render();
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode();
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