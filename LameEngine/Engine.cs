using System.ComponentModel;
using System.Reflection;
using OpenTK.Graphics.OpenGL.Compatibility;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace LameEngine;

public static class Engine
{
    private static GameTemplate gameTemplate;
    private static Window window;
    private static NativeWindowSettings nativeWindowSettings;

    private static readonly List<GameObject> gameObjects = new List<GameObject>();

    public static void Initialize()
    {
        nativeWindowSettings = new NativeWindowSettings
        {
            ClientSize = new Vector2i(500, 500),
            Title = "My Game Window",
        };
        
        window = new Window(GameWindowSettings.Default, nativeWindowSettings);
    }
    
    public static void Run(GameTemplate pGameTemplate)
    {
        gameTemplate = pGameTemplate;
        
        
        window.Run();
    }

    private static void RegisterObject(GameObject pGameObject)
    {
        gameObjects.Add(pGameObject);        
    }

    private static void InternalLoad()
    {
        gameTemplate.Load();
    }

    private static void InternalUpdate(FrameEventArgs pEventArgs)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalUpdate();
        }
        
        gameTemplate.Update();
    }

    private static void InternalRender(FrameEventArgs pEventArgs)
    {
        GL.ClearColor(0.39f, 0.58f, 0.93f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalRender();
        }
        
        gameTemplate.Render();
    }

    private static void InternalClosing(CancelEventArgs pCancelEventArgs)
    {
        gameTemplate.Close();
    }
}