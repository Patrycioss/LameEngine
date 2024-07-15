using Silk.NET.Windowing;
using SilkWindow = Silk.NET.Windowing.Window;

namespace LameEngine;

public class Engine
{
    public static Engine I { get; private set; }
    
    public Window MainWindow => windows[0];
    public float DeltaTime { get; private set; }

    private readonly List<Window> windows = new List<Window>();
    private GameTemplate? gameTemplate;

    private double lastTime;

    public Engine(WindowOptions pWindowOptions, WindowSettings pWindowSettings)
    {
        I = this;
        
        // Prioritize GLFW as it plays better with multiple windows and input.
        SilkWindow.PrioritizeGlfw();

        CreateWindow(pWindowOptions, pWindowSettings);
    }
    
    public Window CreateWindow(WindowOptions pWindowOptions, WindowSettings pWindowSettings)
    {
        pWindowOptions.IsEventDriven = false;
        pWindowOptions.ShouldSwapAutomatically = false;
        pWindowOptions.VideoMode = new VideoMode(pWindowSettings.Resolution);
        Window window = new Window(pWindowOptions, pWindowSettings);
        windows.Add(window);
        return window;
    }

    public void CloseWindow(Window pWindow)
    {
        for (int i = windows.Count - 1; i >= 0; i--)
        {
            int index = windows.IndexOf(pWindow);
            if (index != -1)
            {
                windows.RemoveAt(index);
            }
        }
    }

    public void CloseAllAdditionalWindows()
    {
        for (int i = windows.Count - 1; i >= 0; i--)
        {
            windows.RemoveAt(i);
        }
    }

    public void Run(GameTemplate pGameTemplate)
    {
        gameTemplate = pGameTemplate;
        gameTemplate!.Load();
        
        while (!MainWindow.Handle.IsClosing)
        {
            foreach (Window window in windows)
            {
                window.Handle.DoEvents();
            }
            
            gameTemplate!.Update();
            
            DeltaTime = (float)(MainWindow.Handle.Time - lastTime);
            lastTime = MainWindow.Handle.Time;

            foreach (Window window in windows)
            {
                window.Process();
            }
        }
    }
}