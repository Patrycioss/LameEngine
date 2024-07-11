using Silk.NET.OpenGL;

namespace LameEngine;

public class ShaderProgram : IDisposable
{
    private readonly uint handle;
    private bool disposedValue = false;
    private readonly Dictionary<string, int> uniformLocations = new Dictionary<string, int>();


    public ShaderProgram(string pVertexPath, string pFragmentPath)
    {
        GL GL = WindowManager.GL;

        string vertexSource = File.ReadAllText(pVertexPath);
        string fragmentSource = File.ReadAllText(pFragmentPath);

        uint vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexSource);

        uint fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentSource);

        GL.CompileShader(vertexShader);

        {
            string info = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(info);
        }

        GL.CompileShader(fragmentShader);

        {
            string info = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(info);
        }

        handle = GL.CreateProgram();

        GL.AttachShader(handle, vertexShader);
        GL.AttachShader(handle, fragmentShader);

        GL.LinkProgram(handle);

        {
            string info = GL.GetProgramInfoLog(handle);
            Console.WriteLine(info);
        }

        GL.DetachShader(handle, vertexShader);
        GL.DetachShader(handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }

    public void Use()
    {
        WindowManager.GL.UseProgram(handle);
    }

    public void Dispose()
    {
        if (!disposedValue)
        {
            WindowManager.GL.DeleteProgram(handle);
            disposedValue = true;
        }

        GC.SuppressFinalize(this);
    }

    ~ShaderProgram()
    {
        if (!disposedValue)
        {
            Console.WriteLine($"GPU Resource leak! Did you forget to call Dispose()?");
        }
    }


    public void SetColor(string pName, Color pColor)
    {
        WindowManager.GL.Uniform4(GetUniformLocation(pName), pColor.R, pColor.G, pColor.B, pColor.A);
    }

    private int GetUniformLocation(string pName)
    {
        if (uniformLocations.TryGetValue(pName, out int pValue))
        {
            return pValue;
        }

        int value = WindowManager.GL.GetUniformLocation(handle, pName);
        if (value == -1)
        {
            throw new Exception($"Couldn't find uniform with name: {pName}!");
        }

        return value;
    }
}