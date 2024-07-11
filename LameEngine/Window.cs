using System.ComponentModel;
using System.Reflection;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace LameEngine;

public class Window : GameWindow
{
    private const int METHOD_AMOUNT = 4;
    private readonly MethodValid[] methods = new MethodValid[METHOD_AMOUNT];

    public Window(GameWindowSettings gameWindowSettings,
        NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
        string[] methodNames =
        {
            "InternalLoad",
            "InternalUpdate",
            "InternalRender",
            "InternalClosing",
        };

        MethodInfo[] foundMethods = typeof(Engine).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);


        foreach (MethodInfo method in foundMethods)
        {
            for (int i = 0; i < METHOD_AMOUNT; i++)
            {
                if (method.Name.Equals(methodNames[i]))
                {
                    methods[i].MethodInfo = method;
                    methods[i].Valid = true;
                    break;
                }
            }
        }
    }

    protected override void OnLoad()
    {
        if (methods[0].Valid) methods[0].MethodInfo!.Invoke(null, null);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (methods[1].Valid) methods[1].MethodInfo!.Invoke(null, new object[] { e });
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        if (methods[2].Valid) methods[2].MethodInfo!.Invoke(null, new object[] { e });
        
        SwapBuffers();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        if (methods[3].Valid) methods[3].MethodInfo?.Invoke(null, new object[] { e });
    }
}