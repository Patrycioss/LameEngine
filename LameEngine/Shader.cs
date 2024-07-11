using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace LameEngine;

public class Shader : IDisposable
{
    private readonly int handle;
    private bool disposedValue = false;
    private readonly Dictionary<string, int> uniformLocations = new Dictionary<string, int>();

    public Shader(string pVertexPath, string pFragmentPath)
    {
        string vertexSource = File.ReadAllText(pVertexPath);
        string fragmentSource = File.ReadAllText(pFragmentPath);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexSource);
        
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentSource);
        
        GL.CompileShader(vertexShader);

        {
            GL.GetShaderInfoLog(vertexShader, out string info);
            Console.WriteLine(info);
        }
        
        GL.CompileShader(fragmentShader);

        {
            GL.GetShaderInfoLog(fragmentShader, out string info);
            Console.WriteLine(info);
        }

        handle = GL.CreateProgram();
        
        GL.AttachShader(handle, vertexShader);
        GL.AttachShader(handle, fragmentShader);
        
        GL.LinkProgram(handle);

        {
            GL.GetProgramInfoLog(handle, out string info);
            Console.WriteLine(info);
        }

        GL.DetachShader(handle,  vertexShader);
        GL.DetachShader(handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }

    public void Use()
    {
        GL.UseProgram(handle);
    }

    public void Dispose()
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(handle);
            disposedValue = true;
        }
        GC.SuppressFinalize(this);
    }
    
    ~Shader()
    {
        if (!disposedValue)
        {
            Console.WriteLine($"GPU Resource leak! Did you forget to call Dispose()?");
        }   
    }
    

    public void SetColor<T>(string pName, Color4<T> pColor) where T : IColorSpace4
    {
        GL.Uniform4f(GetUniformLocation(pName), pColor.X, pColor.Y, pColor.Z, pColor.W);
    }

    private int GetUniformLocation(string pName)
    {
        if (uniformLocations.TryGetValue(pName, out int pValue))
        {
            return pValue;
        }
        
        int value = GL.GetUniformLocation(handle, pName);
        if (value == -1)
        {
            throw new Exception($"Couldn't find uniform with name: {pName}!");
        }

        return value;
    }

}