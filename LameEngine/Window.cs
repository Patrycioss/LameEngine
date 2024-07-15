using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using Silk.NET.Windowing;
using SilkWindow = Silk.NET.Windowing.Window;

namespace LameEngine;

public class WindowSettings
{
    public Vector2D<int> Resolution;
    public float NearPlane;
    public float FarPlane;
    public float Zoom;
    public string ScreenVertexPath;
    public string ScreenFragmentPath;
    public Color BackgroundColor;

    public WindowSettings(Vector2D<int> pResolution)
    {
        Resolution = pResolution;
        NearPlane = -1.0f;
        FarPlane = 1.0f;
        Zoom = 1.0f;
        ScreenVertexPath = "EngineResources/Shaders/Screen.vert";
        ScreenFragmentPath = "EngineResources/Shaders/Screen.frag";
        BackgroundColor = Color.Blue;
    }

    public Matrix4X4<float> CalculateProjectionMatrix()
    {
        Matrix4X4<float> orthographic =
            Matrix4X4.CreateOrthographicOffCenter(0, Resolution.X, Resolution.Y, 0, NearPlane, FarPlane);
        Matrix4X4<float> zoomMatrix = Matrix4X4.CreateScale(new Vector3D<float>(Zoom, Zoom, 1));
        return orthographic * zoomMatrix;
    }
}

public class Window
{
    public readonly Guid ID;
    public IWindow Handle { get; private set; }
    public GL GL { get; private set; }
    public IInputContext InputContext { get; private set; }
    public Matrix4X4<float> ProjectionMatrix { get; private set; }
    public WindowSettings Settings;
    public bool HasFocus { get; private set; }

    private readonly FrameBuffer frameBuffer;
    private readonly ShaderProgram screenShader;

    private List<GameObject> gameObjects = new List<GameObject>();

    public Window(WindowOptions pWindowOptions, WindowSettings pWindowSettings)
    {
        Settings = pWindowSettings;
        
        // Default to OpenGL for now.
        pWindowOptions.API = GraphicsAPI.Default;

        ID = Guid.NewGuid();
        RecalculateProjectionMatrix();


        Handle = SilkWindow.Create(pWindowOptions);
        Handle.Initialize();
        Handle.IsVisible = true;
        Handle.FocusChanged += (pChanged) => HasFocus = pChanged;
        

        GL = GL.GetApi(Handle.GLContext);
        InputContext = Handle.CreateInput();

        Handle.MakeCurrent();

        frameBuffer = new FrameBuffer(GL, Handle.Size);
        Handle.Resize += OnWindowResize;

        screenShader = new ShaderProgram(GL, pWindowSettings.ScreenVertexPath, pWindowSettings.ScreenFragmentPath);
    }

    public void Process()
    {
        Handle.MakeCurrent();
        frameBuffer.Bind();
        GL.ClearColor(Settings.BackgroundColor.R, Settings.BackgroundColor.G, Settings.BackgroundColor.B,
        Settings.BackgroundColor.A);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalUpdate();
        }

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalRender();
        }

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        GL.ClearColor(1,0,0,1);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        screenShader.Use();
        frameBuffer.Draw();
        Handle.SwapBuffers();
    }

    public void RecalculateProjectionMatrix()
    {
        ProjectionMatrix = Settings.CalculateProjectionMatrix();
    }

    public void AddObject(GameObject pGameObject)
    {
        gameObjects.Add(pGameObject);
    }

    private void OnWindowResize(Vector2D<int> pNewSize)
    {
        Handle.MakeCurrent();
        frameBuffer.Resize(pNewSize);
        GL.Viewport(pNewSize);
    }

    ~Window()
    {
        Handle.Close();
        Handle.Dispose();
    }
}