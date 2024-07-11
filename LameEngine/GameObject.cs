using System.Reflection;

namespace LameEngine;

public class GameObject
{
    private readonly HashSet<Component> components = new HashSet<Component>();

    private static MethodInfo? registerMethod = null;
    private static bool registerMethodFound = false;

    public GameObject()
    {
        if (!registerMethodFound)
        {
            Type engineType = typeof(Engine);
            registerMethod = engineType.GetMethod("RegisterObject", BindingFlags.Static | BindingFlags.NonPublic);
            if (registerMethod == null)
            {
                throw new Exception($"Can't find register method in {nameof(Engine)} class!");
            }

            registerMethodFound = true;
        }

        registerMethod?.Invoke(null, new object?[] { this });
    }
    
  
    
    public T AddComponent<T>(T pComponent) where T : Component
    {
        if (components.Add(pComponent))
        {
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
        foreach (Component component in components)
        {
            component.InternalUpdate();
        }
        
        Update();
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