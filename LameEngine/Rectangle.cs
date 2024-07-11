using Silk.NET.OpenGL;

namespace LameEngine;

public static class Rectangle
{
    private static uint vao;
    private static ShaderProgram shaderProgram;

    private static GL GL = WindowManager.GL;

    public static void Initialize()
    {
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
        };


        uint vbo = GL.GenBuffer();
        vao = GL.GenVertexArray();

        GL.BindVertexArray(vao);
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);

        Span<float> v = new Span<float>(vertices);

        GL.BufferData(
            BufferTargetARB.ArrayBuffer,
            (uint)vertices.Length * sizeof(float),
            v.GetPinnableReference(),
            BufferUsageARB.StaticDraw
        );

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
    }

    public static void Draw(Color pColor)
    {
        shaderProgram.Use();
        shaderProgram.SetColor("spriteColor", pColor);
        InternalDraw(pColor);
    }

    public static void Draw(Texture pTexture)
    {
        shaderProgram.Use();
        pTexture.Use();
        InternalDraw(Color.White);
    }

    public static void Draw(Texture pTexture, Color pColor)
    {
        shaderProgram.Use();
        pTexture.Use();
        InternalDraw(pColor);
    }

    private static void InternalDraw(Color pColor)
    {
        shaderProgram.SetColor("spriteColor", pColor);

        GL.BindVertexArray(vao);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.UseProgram(0);
    }
}