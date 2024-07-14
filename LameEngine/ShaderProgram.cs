using System.Numerics;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace LameEngine;

public class ShaderProgram : IDisposable
{
    private readonly uint handle;
    private bool disposedValue = false;
    private readonly Dictionary<string, int> uniformLocations = new Dictionary<string, int>();

    private static GL gl;

    public ShaderProgram(string pVertexPath, string pFragmentPath)
    {
        string vertexSource = File.ReadAllText(pVertexPath);
        string fragmentSource = File.ReadAllText(pFragmentPath);

        uint vertexShader = gl.CreateShader(ShaderType.VertexShader);
        gl.ShaderSource(vertexShader, vertexSource);

        uint fragmentShader = gl.CreateShader(ShaderType.FragmentShader);
        gl.ShaderSource(fragmentShader, fragmentSource);

        gl.CompileShader(vertexShader);

        {
            string info = gl.GetShaderInfoLog(vertexShader);
            Console.WriteLine(info);
        }

        gl.CompileShader(fragmentShader);

        {
            string info = gl.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(info);
        }

        handle = gl.CreateProgram();

        gl.AttachShader(handle, vertexShader);
        gl.AttachShader(handle, fragmentShader);

        gl.LinkProgram(handle);

        {
            string info = gl.GetProgramInfoLog(handle);
            Console.WriteLine(info);
        }

        gl.DetachShader(handle, vertexShader);
        gl.DetachShader(handle, fragmentShader);
        gl.DeleteShader(fragmentShader);
        gl.DeleteShader(vertexShader);
    }

    public void Use()
    {
        gl.UseProgram(handle);
    }

    public void Dispose()
    {
        if (!disposedValue)
        {
            gl.DeleteProgram(handle);
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
        gl.Uniform4(GetUniformLocation(pName), pColor.R, pColor.G, pColor.B, pColor.A);
    }

    public unsafe void SetMatrix4X4(string pName, Matrix4X4<float> pMatrix)
    {
        gl.UniformMatrix4(GetUniformLocation(pName), 1, false, (float*)&pMatrix);
    }

    public unsafe void SetMatrix4X4(string pName, Matrix4x4 pMatrix)
    {
        gl.UniformMatrix4(GetUniformLocation(pName), 1, false, (float*)&pMatrix);
    }

    private int GetUniformLocation(string pName)
    {
        if (uniformLocations.TryGetValue(pName, out int pValue))
        {
            return pValue;
        }

        int value = gl.GetUniformLocation(handle, pName);
        if (value == -1)
        {
            throw new Exception($"Couldn't find uniform with name: {pName}!");
        }

        uniformLocations.Add(pName, value);

        return value;
    }

    internal static void Initialize(GL pGL)
    {
        gl = pGL;
    }
}