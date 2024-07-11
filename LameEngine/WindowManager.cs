using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using static Silk.NET.Windowing.Window;

namespace LameEngine;

public static class WindowManager
{        
    public static GL GL;
    public static IWindow MainWindow { get; private set; }
    public static IInputContext InputContext;

    private static readonly List<IWindow> additionalWindows = new List<IWindow>();
    
    public static void CreateMainWindow(WindowOptions pMainWindowOptions)
    {
        MainWindow = Create(pMainWindowOptions);
        MainWindow.Initialize();
        InputContext = MainWindow.CreateInput();

        GL = GL.GetApi(MainWindow.GLContext);
    }

    public static IWindow CreateWindow(WindowOptions pMainWindowOptions)
    {
        IWindow window = Create(pMainWindowOptions);
        additionalWindows.Add(window);
        window.Initialize();
        return window;
    }

    public static void CloseWindow(IWindow pWindow)
    {
        for (int i = additionalWindows.Count - 1; i >= 0; i--)
        {
            int index = additionalWindows.IndexOf(pWindow);
            if (index != -1)
            {
                additionalWindows[index].Close();
                additionalWindows[index].Dispose();
                additionalWindows.RemoveAt(index);
            }
        }
    }

    public static void CloseAllAdditionalWindows()
    {
        for (int i = additionalWindows.Count - 1; i >= 0; i--)
        {
            additionalWindows[i].Close();
            additionalWindows[i].Dispose();
            additionalWindows.RemoveAt(i);
        }
    }
    
    public static void Run()
    {
        while (!MainWindow.IsClosing)
        {
            MainWindow.DoEvents();
            MainWindow.DoUpdate();
            MainWindow.DoRender();

            foreach (IWindow? window in additionalWindows)
            {
                if (!window.IsClosing)
                {
                    window.DoEvents();
                    window.DoUpdate();
                    window.DoRender();
                }
            }
        }
    }
}