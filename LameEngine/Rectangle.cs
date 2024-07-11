using OpenTK.Graphics.OpenGL.Compatibility;
using OpenTK.Mathematics;

namespace LameEngine;

public static class Rectangle
{
    private static readonly int VAO;
    private static readonly Shader shader;

    static Rectangle()
    {
        shader = new Shader("Resources/Shaders/Rectangle.vert", "Resources/Shaders/Rectangle.frag");

        float[] vertices =
        {
            //   Pos         Tex
            -0.5f, 0.5f, 0.0f, 1.0f,
            0.5f, -0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, 0.0f, 0.0f,

            -0.5f, 0.5f, 0.0f, 1.0f,
            0.5f, 0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, 1.0f, 0.0f
        };


        int VBO;
        GL.GenBuffer(out VBO);
        GL.GenVertexArray(out VAO);

        GL.BindVertexArray(VAO);


        Span<float> span = vertices;
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            vertices.Length * sizeof(float),
            span.GetPinnableReference(),
            BufferUsage.StaticDraw
        );

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public static void Draw(Color4<Rgba> pColour)
    {
        shader.Use();
        shader.SetColor("spriteColor",pColour);
        InternalDraw(pColour);
    }

    public static void Draw(Texture pTexture)
    {
        shader.Use();
        pTexture.Use();
        InternalDraw(Color4.White);
    }
    
    public static void Draw(Texture pTexture, Color4<Rgba> pColour)
    {
        shader.Use();
        pTexture.Use();
        InternalDraw(pColour);
    }

    private static void InternalDraw(Color4<Rgba> pColour)
    {
        shader.SetColor("spriteColor",pColour);

        GL.BindVertexArray(VAO);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.UseProgram(0);
    }
}