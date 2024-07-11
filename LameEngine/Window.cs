using Silk.NET.GLFW;
using Silk.NET.Windowing;
using Monitor = Silk.NET.GLFW.Monitor;

namespace LameEngine;

// public struct WindowSettings()
// {
//     public int Width = 600;
//     public int Height = 600;
//     public string Title = "My Window";
//     public IMonitor Monitor = Silk.NET.Windowing.Monitor.GetMainMonitor(null);
//     public IWindow Share;
// }

public class Window
{
    public IWindow Handle { get; }
    public readonly WindowOptions Options;
    
    public unsafe Window(WindowOptions pWindowOptions)
    {
        Options = pWindowOptions;
        Handle = Silk.NET.Windowing.Window.Create(Options);
    }
}