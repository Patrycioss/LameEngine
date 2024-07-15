using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace LameEngine;

public class SpriteRenderer
{
    private readonly uint vao;
    private readonly ShaderProgram shader;
    private readonly GL gl;

    public class Settings
    {
        public readonly Texture Texture;
        public readonly Matrix4X4<float> ModelMatrix;
        public readonly Matrix4X4<float> ProjectionMatrix;
        public readonly Color Color;

        public Settings(Texture pTexture, Matrix4X4<float> pModelMatrix, Matrix4X4<float> pProjectionMatrix,
            Color pColor)
        {
            Texture = pTexture;
            ModelMatrix = pModelMatrix;
            ProjectionMatrix = pProjectionMatrix;
            Color = pColor;
        }

        public override string ToString()
        {
            return $"[Texture: {Texture}, Color: {Color}, \n{ModelMatrix.MatrixToString("ModelMatrix")}{ProjectionMatrix.MatrixToString("ProjectionMatrix")}]";
        }
    }

    public SpriteRenderer(GL pGL)
    {
        gl = pGL;
        shader = new ShaderProgram(gl, "EngineResources/Shaders/Rectangle.vert",
            "EngineResources/Shaders/Rectangle.frag");

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

    public void Render(Settings pSettings)
    {
        shader.Use();
        pSettings.Texture.Use();
        
        Matrix4X4<float> newModel = pSettings.ModelMatrix;
        Matrix4X4<float> projection = pSettings.ProjectionMatrix;
        
        shader.SetColor("spriteColor", pSettings.Color);
        shader.SetMatrix4X4("modelMatrix", newModel);
        shader.SetMatrix4X4("projectionMatrix", projection);

        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
        gl.UseProgram(0);
    }
}