using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace LameEngine;

public class Engine
{
    public static Engine I { get; private set; }
    
    public Vector2D<int> Resolution { get; private set; }
    public WindowManager WindowManager { get; private set; }
    public IInputContext InputContext { get; private set; }
    public GL GL { get; private set; }

    private GameTemplate? gameTemplate;


    private readonly List<GameObject> gameObjects = new List<GameObject>();

    internal Matrix4X4<float> ProjectionMatrix = Matrix4X4<float>.Identity;
    

    public Engine(WindowOptions pWindowOptions)
    {
        // if (I != null)
        // {
        //     throw new Exception("There can't be more than one active Engine!");
        // }
        
        I = this;


        WindowManager = new WindowManager(pWindowOptions, out GL gl, out IInputContext inputContext);
        GL = gl;
        InputContext = inputContext;
        
        FrameBuffer.Initialize(GL);
        ShaderProgram.Initialize(GL);
        Texture.Initialize(GL);
        Rectangle.Initialize(GL);
        
        SetResolution(pWindowOptions.Size);
    }

    public void Run(GameTemplate pGameTemplate)
    {
        gameTemplate = pGameTemplate;
        Load();
        WindowManager.Run();
    }

    internal void RegisterObject(GameObject pGameObject)
    {
        gameObjects.Add(pGameObject);
    }

    private void Load()
    {
        gameTemplate!.Load();
    }

    internal void InternalUpdate()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalUpdate();
        }

        gameTemplate!.Update();
    }

    internal void InternalRender()
    {
        GL.ClearColor(0.39f, 0.58f, 0.93f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.InternalRender();
        }

        gameTemplate!.Render();
    }

    public void SetResolution(Vector2D<int> pResolution)
    {
        Resolution = pResolution;
        
        // float left = 0;
        // float right = pResolution.X;
        // float bottom = pResolution.Y;
        // float top = 0;
        //
        // float farPlane = 1.0f;
        // float nearPlane = -1.0f;
        //
        // Matrix4X4<float> matrix = new Matrix4X4<float>();
        // matrix.M11 = 2.0f / (right - left);
        // matrix.M22 = 2.0f / (top - bottom);
        // matrix.M33 = -2.0f / (farPlane - nearPlane);
        // matrix.M41 = -(right + left) / (right - left);
        // matrix.M42 = -(top + bottom) / (top - bottom);
        // matrix.M43 = -(farPlane + nearPlane) / (farPlane - nearPlane);
        // matrix.M44 = 1.0f;

        Matrix4X4<float> orthographic =
        Matrix4X4.CreateOrthographicOffCenter(0, pResolution.X, pResolution.Y, 0, -1.0f, 1.0f);
        Matrix4X4<float> zoom = Matrix4X4.CreateScale(new Vector3D<float>(1,-1, 1));
        zoom.LogMatrix("zoom");
        //
        //
        //
        ProjectionMatrix = orthographic * zoom;

        // ProjectionMatrix = matrix;

        // ProjectionMatrix = ;
        ProjectionMatrix.LogMatrix("Projection");
    }
}