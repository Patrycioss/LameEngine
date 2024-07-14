using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace LameEngine;

public class Rectangle
{
    private static uint vao;
    private static ShaderProgram shaderProgram;
    private static GL gl;

    public struct RenderSettings()
    {
        public Matrix4X4<float> ModelMatrix = Matrix4X4<float>.Identity;
        public Color Color = Color.White;
    }
   
    public static void Draw(RenderSettings pRenderSettings)
    {
        shaderProgram.Use();
        InternalDraw(pRenderSettings);
    }

    public static void Draw(RenderSettings pRenderSettings, Texture pTexture)
    {
        shaderProgram.Use();
        pTexture.Use();
        InternalDraw(pRenderSettings);
    }
    
    private static void InternalDraw(RenderSettings pRenderSettings)
    {
        Matrix4X4<float> newModel = pRenderSettings.ModelMatrix;
        Matrix4X4<float> projection = Engine.I.ProjectionMatrix;
        
        shaderProgram.SetColor("spriteColor", pRenderSettings.Color);
        shaderProgram.SetMatrix4X4("modelMatrix", newModel);
        shaderProgram.SetMatrix4X4("projectionMatrix", projection);
        
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
        gl.UseProgram(0);
    }
    
    internal static void Initialize(GL pGL)
    {
        gl = pGL;
        shaderProgram = new ShaderProgram("Resources/Shaders/Rectangle.vert", "Resources/Shaders/Rectangle.frag");

        float[] vertices =
        {
            //   Pos         Tex
            -0.5f, 0.5f, 0.0f, 1.0f,
            0.5f, -0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, 0.0f, 0.0f,
            
            -0.5f, 0.5f, 0.0f, 1.0f,
            0.5f, 0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, 1.0f, 0.0f,
            
            
            // -1.0f, 1.0f, 0.0f, 1.0f,
            // 1.0f, -1.0f, 1.0f, 0.0f,
            // -1.0f, -1.0f, 0.0f, 0.0f,
            //
            // -1.0f, 1.0f, 0.0f, 1.0f,
            // 1.0f, 1.0f, 1.0f, 1.0f,
            // 1.0f, -1.0f, 1.0f, 0.0f,
            
            // -1.0f, 1.0f, 0.0f, 1.0f,
            // 0.0f, 0.0f, 1.0f, 0.0f,
            // -1.0f, 0.0f, 0.0f, 0.0f, 
            //
            // -1.0f, 1.0f, 0.0f, 1.0f,
            // 0.0f, 1.0f, 1.0f, 1.0f,
            // 0.0f, 0.0f, 1.0f, 0.0f,
        };

        uint vbo = gl.GenBuffer();
        vao = gl.GenVertexArray();

        gl.BindVertexArray(vao);
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);

        Span<float> v = new Span<float>(vertices);

        gl.BufferData(
            BufferTargetARB.ArrayBuffer,
            (uint)vertices.Length * sizeof(float),
            v.GetPinnableReference(),
            BufferUsageARB.StaticDraw
        );

        gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        gl.EnableVertexAttribArray(0);

        gl.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        gl.EnableVertexAttribArray(1);

        gl.BindVertexArray(0);
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
    }
}