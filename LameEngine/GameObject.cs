using Silk.NET.Maths;

namespace LameEngine;

public class GameObject
{
    private readonly HashSet<Component> components = new HashSet<Component>();
    public Transform Transform { get; private set; }
    public Window Target { get; private set; }
    
    protected Engine Engine { get; private set; }

    public GameObject(Vector2D<float> pPosition, float pAngle = 0, Window? pTarget = null)
    {
        Transform = new Transform(pPosition, pAngle);
        Engine = Engine.I;
        
        Target = Engine.MainWindow;
        Target = pTarget ?? Engine.MainWindow;
        Target.AddObject(this);
    }

    public GameObject(Vector2D<float> pPosition, Vector2D<float> pScale, float pAngle = 0, Window? pTarget = null)
    {
        Transform = new Transform(pPosition, pScale, pAngle);
        Engine = Engine.I;
        Target = pTarget ?? Engine.MainWindow;
        Target.AddObject(this);
    }
    
    public T AddComponent<T>(T pComponent) where T : Component
    {
        if (components.Add(pComponent))
        {
            pComponent.InternalStart(this, Transform);
            return pComponent;
        }

        return GetComponent<T>()!;
    }

    public void RemoveComponent<T>() where T : Component
    {
        components.RemoveWhere(pComponent => pComponent is T);
    }

    public void RemoveComponent<T>(T pComponent) where T : Component
    {
        components.Remove(pComponent);
    }

    public T? GetComponent<T>() where T : Component
    {
        Component a =  components.FirstOrDefault(pComponent => pComponent is T);
        return (T) a;
    }

    public bool TryGetComponent<T>(out T? pComponent) where T : Component
    {
        Component found =  components.FirstOrDefault(pComponent => pComponent is T);
        pComponent = (T) found;
        return found != null;
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
        
        foreach (Component component in components)
        {
            component.InternalUpdate();
        }
        
        Transform.Clean();
    }

    internal void InternalRender()
    {
        foreach (Component component in components)
        {
            component.InternalRender();
        }
        
        Render();
    }
}