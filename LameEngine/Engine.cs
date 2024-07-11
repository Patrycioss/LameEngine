using System.ComponentModel;
using Silk.NET.OpenGL;

namespace LameEngine;

public static class Engine
{
    // public static MyWindow MainMyWindow { get; private set; }

    private static GameTemplate gameTemplate;
    // private static NativeWindowSettings nativeWindowSettings;

    private static readonly List<GameObject> gameObjects = new List<GameObject>();


    // public static void Initialize(WindowSettings pMainWindowSettings)
    // {
    //     WindowManager.CreateMainWindow(pMainWindowSettings);
    // }
    //
    // public static void Run(GameTemplate pGameTemplate)
    // {
    //     gameTemplate = pGameTemplate;
    //     WindowManager.Run();
    // }

    private static void RegisterObject(GameObject pGameObject)
    {
        gameObjects.Add(pGameObject);        
    }

    internal static void InternalLoad()
    {
        gameTemplate.Load();
    }

    internal static void InternalUpdate()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalUpdate();
        }
        
        gameTemplate.Update();
    }

    internal static void InternalRender()
    {
        WindowManager.GL.ClearColor(0.39f, 0.58f, 0.93f, 1.0f);
        WindowManager.GL.Clear(ClearBufferMask.ColorBufferBit);

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalRender();
        }
        
        gameTemplate.Render();
    }

    internal static void InternalClosing(CancelEventArgs pCancelEventArgs)
    {
        gameTemplate.Close();
    }
}