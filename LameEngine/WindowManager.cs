using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using static Silk.NET.Windowing.Window;

namespace LameEngine;

public class WindowManager
{
    public IWindow MainWindow { get; private set; }
    public float DeltaTime { get; private set; }

    private readonly List<IWindow> additionalWindows = new List<IWindow>();
    private readonly GL gl;
    private readonly Engine engine;
    private readonly ShaderProgram shaderProgram;

    private double lastTime;


    public WindowManager(WindowOptions pMainWindowOptions, out GL pGL, out IInputContext pInputContext)
    {
        MainWindow = Create(pMainWindowOptions);
        MainWindow.Initialize();
        MainWindow.Resize += OnMainWindowResize;

        gl = GL.GetApi(MainWindow.GLContext);

        pGL = gl;
        pInputContext = MainWindow.CreateInput();
        
        engine = Engine.I;

        ShaderProgram.Initialize(gl);
        shaderProgram = new ShaderProgram("Resources/Shaders/Screen.vert", "Resources/Shaders/Screen.frag");
    }

    private void OnMainWindowResize(Vector2D<int> pNewSize)
    {
        gl.Viewport(pNewSize);
    }

    public IWindow CreateWindow(WindowOptions pMainWindowOptions)
    {
        IWindow window = Create(pMainWindowOptions);
        additionalWindows.Add(window);
        window.Initialize();
        return window;
    }

    public void CloseWindow(IWindow pWindow)
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

    public void CloseAllAdditionalWindows()
    {
        for (int i = additionalWindows.Count - 1; i >= 0; i--)
        {
            additionalWindows[i].Close();
            additionalWindows[i].Dispose();
            additionalWindows.RemoveAt(i);
        }
    }

    public void Run()
    {
        FrameBuffer frameBuffer = new FrameBuffer(MainWindow.Size);
        MainWindow.Resize += frameBuffer.Resize;


        while (!MainWindow.IsClosing)
        {
            MainWindow.DoEvents();

            DeltaTime = (float)(MainWindow.Time - lastTime);
            lastTime = MainWindow.Time;

            engine.InternalUpdate();

            frameBuffer.Bind();
            engine.InternalRender();

            gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            gl.Clear(ClearBufferMask.ColorBufferBit);

            shaderProgram.Use();
            frameBuffer.Draw();

            MainWindow.SwapBuffers();
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